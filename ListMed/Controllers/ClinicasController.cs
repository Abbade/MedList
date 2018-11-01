using ListMed.DTO;
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
            ViewBag.servicos = new SelectList(db.Servicos, "Id", "Descricao");
            var clinicas = db.Clinicas.Where(c => c.Localidades.Any(a => a.Descricao.ToUpper().Contains(localidade.ToUpper()))).ToList();
            return View(clinicas);
        }
        public JsonResult listarClinicas(FiltroClinica filtros)
        {
            return Json("Ok");
        }
        public JsonResult listarEspecialidades(string nome, int [] escolhidas)
        {
            List<Autocomplete> especialidades = new List<Autocomplete>();
            if (!string.IsNullOrEmpty(nome))
                especialidades = db.Especialidades.Where(a => a.descricao.ToUpper().Contains(nome.ToUpper())).Select(c => new Autocomplete
                {
                    value = c.Id,
                    label = c.descricao
                }).Take(10).ToList();
            else
                especialidades = db.Especialidades.Select(c => new Autocomplete
                {
                    value = c.Id,
                    label = c.descricao
                }).Take(10).ToList();
            if (escolhidas != null)
            {
                foreach (var cont in escolhidas)
                {
                    especialidades = especialidades.Where(a => a.value != cont).ToList();
                }
            }
            return Json(especialidades);
        }

    }
}