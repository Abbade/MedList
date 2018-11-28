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
        public ActionResult CadastrarClinica(AmostraClinicaViewModel dto, int[] servicos, int[] especialidades, string[] cel)
        {
            if (!ModelState.IsValid)
            {
                selectsCadastro();
                return View();
            }
   
            var est = db.Estados.Find(dto.IdEstado);
            var cid = db.Cidades.Find(dto.IdCidade);
            var bair = db.Bairros.Find(dto.IdBairro);
            var identity = User.Identity as ClaimsIdentity;

            int id = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            AmostraClinica a = new AmostraClinica
            {
                EnderecoFormatado = dto.logradouro + ", " + dto.numero + " - " + dto.complemento + " - " + bair.Nome + ", " + cid.Nome + " - " + est.CodigoUf + ", " + dto.cepClinica + ", Brasil",
                NomeFantasia = dto.Nome,
                LinkSite = dto.Site,
                Lt = dto.Latitude,
                Lg = dto.Longitude,
                PrecoConsulta = dto.PrecoConsulta,
                PrecoExame = dto.PrecoExame,
                HoraAbertura = dto.HoraAbertura,
                HoraFechamento = dto.HoraFechamento,
                Pontos = dto.pontos,
                IdUsuario = id,
                IdEstado = dto.IdEstado,
                IdBairro = dto.IdBairro, 
                IdCidade = dto.IdCidade
                
            };

            List<Servico> servs = new List<Servico>();
            foreach(int s in servicos)
            {
                servs.Add(db.Servicos.Find(s));
            }

            List<Especialidade> esps = new List<Especialidade>();
            foreach (int s in especialidades)
            {
                esps.Add(db.Especialidades.Find(s));
            }


            a.Servicos = servs;
            a.Especialidades = esps;
           
            try
            {
                db.AmostrasClinicas.Add(a);
                db.SaveChanges();
                List<TelefonesClinica> tels = new List<TelefonesClinica>();
                foreach (string s in cel)
                {
                    tels.Add(new TelefonesClinica { Numero = s });
                }
                a.TelefonesClinicas = tels;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return View("Index");
        }
        private void selectsCadastro()
        {
            ViewBag.estado = new SelectList(db.Estados, "Id", "Nome");
        }
        [HttpPost]
        public JsonResult ListarCidades(int id)
        {
            string uf = db.Estados.Find(id).Uf;
            var cidades = db.Cidades.Where(c => c.Uf == uf).Select(a => new {
                id = a.Id,
                descricao = a.Nome
            }).ToList();
            return Json(cidades);
        }
        [HttpPost]
        public JsonResult ListarBairros(int id)
        {
            string codCidade = db.Cidades.Find(id).Codigo.ToString();
            var bairros = db.Bairros.Where(b => b.Codigo.Contains(codCidade)).Select(a => new {
                id = a.Id,
                descricao = a.Nome
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