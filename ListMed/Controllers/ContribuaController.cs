using ListMed.DTO;
using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    [Authorize]
    public class ContribuaController : Controller
    {
        private MedListContext db = new MedListContext();

    
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarClinica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarClinica(AmostraClinicaViewModel dto)
        {
            if (!ModelState.IsValid)
                return View();
            return View();
        }
    }
}