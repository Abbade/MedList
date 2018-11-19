using ListMed.DTO;
using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    public class AutenticacaoController : Controller
    {
        private MedListContext db = new MedListContext();
        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuario dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            if(db.Usuarios.Count(u => u.email.ToUpper() == dto.Email.ToUpper()) > 0)
            {
                ModelState.AddModelError("Email", "Esse e-mail ja está em uso!");
                return View(dto);
            }
            Usuario usuario = new Usuario
            {
                nick = dto.Nome,
                email = dto.Email,
                senha = Hash.GerarHash(dto.Senha),

            };
            db.Usuarios.Add(usuario);
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index", "Inicio");
        }

        public ActionResult Login(string ReturnUrl)
        {
            var dto = new LoginUsuario
            {
                UrlRetorno = ReturnUrl
            };
            return View(dto);
        }
        [HttpPost]
        public ActionResult Login(LoginUsuario dto)
        {
            bool foto = false;
            if (!ModelState.IsValid)
                return View(dto);
            var usuario = db.Usuarios.FirstOrDefault(u => u.email.ToUpper() == dto.Email.ToUpper());
            if (usuario == null)
            {
                ModelState.AddModelError("Email", "E-mail incorreto");
                return View(dto);
            }
            if (usuario.senha != Hash.GerarHash(dto.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(dto);
            }
            if (usuario.Foto != null)
                foto = true;
            var identity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.Name, usuario.nick),
                new Claim(ClaimTypes.Email, usuario.email),
                new Claim("TemFoto", foto.ToString()),
                new Claim("Id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
            }, "ApplicationCookie");


            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(dto.UrlRetorno) ||
                Url.IsLocalUrl(dto.UrlRetorno))
                return Redirect(dto.UrlRetorno);
            else
                return RedirectToAction("Index", "Inicio");
        }
        public FileContentResult FotoPerfil(int id)
        {
            var usuario = db.Usuarios.Find(id);
            if (usuario != null && usuario.Foto != null)
                return File(usuario.Foto, "image", "perfil_" + usuario.Id);
            else
                return null;
        }
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Inicio");
        }
    }
}