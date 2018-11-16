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
            selectsCadastro();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarClinica(AmostraClinicaViewModel dto, int[] servicos, int[] especialidades)
        {
     
            if (!ModelState.IsValid)
            {
                selectsCadastro();
                return View();
            }
      
            return View();
        }
        private void selectsCadastro()
        {
            ViewBag.estado = new SelectList(db.Estados, "Id", "Descricao");
        }
        [HttpPost]
        public JsonResult ListarCidades(int id)
        {
            var cidades = db.Cidades.Where(c => c.IdEstado == id).Select(a => new {
                id = a.Id,
                descricao = a.Descricao
            }).ToList();
            return Json(cidades);
        }
        [HttpPost]
        public JsonResult ListarBairros(int id)
        {
            var bairros = db.Bairros.Where(b => b.IdCidade == id).Select(a => new {
                id = a.Id,
                descricao = a.Descricao
            }).ToList();
            return Json(bairros);
        }
        [HttpPost]
        public JsonResult ListarServicos(string nome, int[] escolhidas)
        {
            List<Autocomplete> servicos = new List<Autocomplete>();
            if (!string.IsNullOrEmpty(nome))
                servicos = db.Servicos.Where(a => a.Descricao.ToUpper().Contains(nome.ToUpper())).Select(s => new Autocomplete
                {
                    value = s.Id,
                    label = s.Descricao
                }).Take(10).ToList();
            else
                servicos = db.Servicos.Select(s => new Autocomplete
                {
                    value = s.Id,
                    label = s.Descricao
                }).Take(10).ToList();
            if (escolhidas != null)
                foreach (var cont in escolhidas)
                    servicos = servicos.Where(a => a.value != cont).ToList();
            return Json(servicos);
        }
    }
}