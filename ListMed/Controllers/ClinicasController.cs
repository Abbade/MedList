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
            ViewBag.local = local;
            ViewBag.servicos = new SelectList(db.Servicos, "Id", "Descricao");
            List<Clinica> clinicas = new List<Clinica>();
            if(local == " (Cidade)")
                clinicas = db.Clinicas.Where(c => c.Cidade.Descricao.ToUpper().Contains(localidade.ToUpper())).ToList();
            else
                clinicas = db.Clinicas.Where(c => c.Estado.Descricao.ToUpper().Contains(localidade.ToUpper())).ToList();
            return View(clinicas);
        }


 
        public ActionResult Detalhes(int id, string returnMsg)
        {
            ViewBag.returnMsg = returnMsg;
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
                    model = db.Clinicas.Where(c => c.Cidade.Descricao.ToUpper().Contains(filtros.localidade.ToUpper()) && (c.PrecoConsulta <= filtros.preco || c.PrecoExame <= filtros.preco || (c.PrecoConsulta == null && c.PrecoExame == null)) ).ToList();
                else
                    model = db.Clinicas.Where(c => c.Estado.Descricao.ToUpper().Contains(filtros.localidade.ToUpper()) && (c.PrecoConsulta <= filtros.preco || c.PrecoExame <= filtros.preco || (c.PrecoConsulta == null && c.PrecoExame == null ))).ToList();
                if(filtros.servico > 0)
                {
                    model = model.Where(a => a.Servicos.Any(s => s.Id == filtros.servico)).ToList();
                }
                if(filtros.especialidades != null && filtros.especialidades.Count > 0)
                {
                    model = model.Where(m => m.Especialidades.Any(a => filtros.especialidades.Contains(a.Id))).ToList();
                }

            }



            return PartialView("_Clinicas", model);
        }
        [HttpPost]
        public ActionResult AvaliarClinica(int id, string desc, int estrelas)
        {
            var usuario = UsuarioLogado();
            Avaliacao av = new Avaliacao();
            av.IdUsuario = usuario.Id;
            av.IdClinica = id;
            av.comentario = desc;
            av.nota = estrelas;
            av.DataHora = DateTime.Now;

            db.Avaliacoes.Add(av);
            db.SaveChanges();
            recalcularAvaliacao(av.IdClinica);
            return RedirectToAction("Detalhes", new { id = id , returnMsg = "Clinica avaliada com sucesso!"});
        }
        [HttpPost]
        public ActionResult ReavaliarClinica(int id, string desc, int estrelas)
        {
            var av = db.Avaliacoes.Find(id);
            av.comentario = desc;
            av.nota = estrelas;
            av.DataHora = DateTime.Now;
            db.Entry(av).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            recalcularAvaliacao(av.IdClinica);



            return RedirectToAction("Detalhes", new { id = av.IdClinica, returnMsg = "Clinica avaliada com sucesso!" });
        }
        public void recalcularAvaliacao(int id)
        {
            var clinica = db.Clinicas.Find(id);
            int valorAvalicoes = clinica.Avaliacoes.Sum(a => a.nota != null ? (int)a.nota : 0);
            int totalAvaliacaoClinica = clinica.Avaliacoes.Count(a => a.nota != null);
            double avaliacaoNova = (double) valorAvalicoes / totalAvaliacaoClinica;
            clinica.avaliacao = avaliacaoNova;
            db.Entry(clinica).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
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
        [HttpPost]
        public JsonResult FavoritarClinica(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                int idUsuario = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var usuario = db.Usuarios.Find(idUsuario);
                usuario.Clinicas.Add(db.Clinicas.Find(id));
                db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public JsonResult DesFavoritarClinica(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                int idUsuario = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                var usuario = db.Usuarios.Find(idUsuario);
                usuario.Clinicas.Remove(db.Clinicas.Find(id));
                db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        public Usuario UsuarioLogado()
        {
            var identity = User.Identity as ClaimsIdentity;
            int idUsuario = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            var usuario = db.Usuarios.Find(idUsuario);
            return usuario;
        }
       
    }
}