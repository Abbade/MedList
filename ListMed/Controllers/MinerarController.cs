using ListMed.Mineracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    public class MinerarController : Controller
    {
        // GET: Minerar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listarPagina(string localidade)
        {
            MinerarClinicas m = new MinerarClinicas();
            m.listarClinicas(localidade);
            return View("Index");
        }
    }
}