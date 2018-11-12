using ListMed.DTO;
using ListMed.Geral;
using ListMed.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private MedListContext db = new MedListContext();

        public ActionResult AlterarSenha()
        {
            return View();
        }
        public ActionResult MeuPerfil()
        {
            var usu = RetornaUsuario();
            AlterarPerfil p = new AlterarPerfil()
            {
                Id = usu.Id,
                nome = usu.nick,
                email = usu.email,
                temFoto = usu.Foto != null ? true : false
            };
            return View(p);
        }
        public ActionResult MinhasClinicas()
        {
            var usu = RetornaUsuario();
            ViewBag.servicos = new SelectList(db.Servicos, "Id", "Descricao");
            return View(usu.Clinicas);
        }

        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenha dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identity = User.Identity as ClaimsIdentity;

            int id = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var usuario = db.Usuarios.Find(id);

            if (Hash.GerarHash(dto.SenhaAtual) != usuario.senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }


            usuario.senha = Hash.GerarHash(dto.NovaSenha);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Inicio");

        }



        [HttpPost]
        public ActionResult AlterarPerfil(AlterarPerfil p)
        {
            var usu = RetornaUsuario();
            usu.nick = p.nome;
            usu.Foto = Anexo.ArqParaByte(p.foto);
            db.Entry(usu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            identity.RemoveClaim(identity.Claims.FirstOrDefault(c => c.Type == "TemFoto"));
            identity.AddClaim(new Claim("TemFoto", "true"));
            var authenticationManager = Request.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties() { IsPersistent = true });
            return RedirectToAction("MeuPerfil");
        }
        public FileContentResult FotoPerfil()
        {
            var usuario = RetornaUsuario();
            if (usuario != null)
                return File(usuario.Foto, "image", "perfil_" + usuario.Id);
            else
                return null;
        }
        public  Usuario RetornaUsuario()
        {
            var identity = User.Identity as ClaimsIdentity;

            int id = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var usuario = db.Usuarios.Find(id);
            return usuario;
        }
    }
}