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
    [Authorize]
    public class PerfilController : Controller
    {
        private MedListContext db = new MedListContext();

        public ActionResult AlterarSenha()
        {
            return View();
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
    }
}