using ListMed.DTO;
using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    public class InicioController : Controller
    {
        private MedListContext db = new MedListContext();
        HttpClient client = new HttpClient();

        public InicioController()
        {

        }
        // GET: Inicio
        public ActionResult Index()
        {


            //db.SaveChanges();

            return View();
        }

        [HttpPost]
        public JsonResult ListarLocalidades(string nome)
        {
            var localidades = db.Estados.Where(e => e.Descricao.ToUpper().Contains(nome.ToUpper())).OrderBy(c => c.Descricao).Select(a => new {
                value = a.Descricao + " (Estado)",
                label = a.Descricao + " (Estado)"
            }).Take(5).ToList();
            localidades.AddRange(db.Cidades.Where(c => c.Descricao.ToUpper().Contains(nome.ToUpper())).OrderBy(a => a.Descricao).Select(s => new
            {
                value = s.Descricao + " (Cidade)",
                label = s.Descricao + " (Cidade)"
            }).Take(5).ToList());
            return Json(localidades);
        }

 
    }
}