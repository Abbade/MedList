using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    public class ClinicasController : Controller
    {
        private MedListContext db = new MedListContext();

        public ActionResult Index(string localidade)
        {
            var clinicas = db.Clinicas.Where(c => c.Localidades.Any(a => a.Descricao.ToUpper().Contains(localidade.ToUpper()))).ToList();
            return View(clinicas);
        }

    }
}