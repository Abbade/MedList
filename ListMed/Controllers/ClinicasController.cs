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
            int inicio = localidade.IndexOf(" (");
            int fim = localidade.IndexOf(")");
            string local = "";
            if(inicio > 0 && fim > 0)
            {
                 local = localidade.Substring(inicio);
                localidade = localidade.Substring(0, inicio);
            }

            ViewBag.servicos = new SelectList(db.Servicos, "Id", "Descricao");
            List<Clinica> clinicas = new List<Clinica>();
            if(local == " (Cidade)")
                clinicas = db.Clinicas.Where(c => c.Cidade.Descricao.ToUpper().Contains(localidade.ToUpper())).ToList();
            else
                clinicas = db.Clinicas.Where(c => c.Estado.Descricao.ToUpper().Contains(localidade.ToUpper())).ToList();
            return View(clinicas);
        }


        public ActionResult Recupera(int id)
        {
            
            return View(db.Clinicas.Find(id));
        }

        [HttpPost]
        public ActionResult listarClinicas(FiltroClinica filtros)
        {
            List<Clinica> model = new List<Clinica>();

            if(filtros.localidade != null)
            {
                int inicio = filtros.localidade.IndexOf(" (");
                int fim = filtros.localidade.IndexOf(")");
                string local = "";
                if (inicio > 0 && fim > 0)
                {
                    local = filtros.localidade.Substring(inicio);
                    filtros.localidade = filtros.localidade.Substring(0, inicio);
                }
              
                if (local == " (Cidade)")
                    model = db.Clinicas.Where(c => c.Cidade.Descricao.ToUpper().Contains(filtros.localidade.ToUpper())).ToList();
                else
                    model = db.Clinicas.Where(c => c.Estado.Descricao.ToUpper().Contains(filtros.localidade.ToUpper())).ToList();

            }



            return PartialView("_Clinicas", model);
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