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
        public ActionResult Index(string returnMsg)
        {

            

            return View(returnMsg);
        }

        [HttpPost]
        public JsonResult ListarLocalidades(string nome)
        {
            var localidades = db.Estados.Where(e => e.Nome.ToUpper().Contains(nome.ToUpper())).OrderBy(c => c.Nome).Select(a => new {
                value = a.Nome + " (Estado)",
                label = a.Nome + " (Estado)"
            }).Take(5).ToList();
            localidades.AddRange(db.Cidades.Where(c => c.Nome.ToUpper().Contains(nome.ToUpper())).OrderBy(a => a.Nome).Select(s => new
            {
                value = s.Nome + " (Cidade)",
                label = s.Nome + " (Cidade)"
            }).Take(5).ToList());
            return Json(localidades);
        }

 
    }
}