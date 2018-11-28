using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListMed.DTO;
using ListMed.Geral;
using System.Security.Claims;

namespace ListMed.Controllers
{
    [AutorizacaoTipo(new[] { TipoUsuario.Adminstrador })]
    public class PainelController : Controller
    {
        private MedListContext db = new MedListContext();
        // GET: Painel
        public ActionResult Index()
        {
         
            return View(db.AmostrasClinicas.Where(s => s.Ativo == true).Select(a => new ListaAmostra
            {
                Id = a.Id,
                Nome = a.NomeFantasia,
                Site = a.LinkSite,
                Cidade = a.Cidade.Nome,
                Estado = a.Estado.Nome,
                NomeUsuario = a.Usuario.nick
            }).ToList()
            );
        }
        public ActionResult VerificarAmostra(int id)
        {
   
            var a = db.AmostrasClinicas.Find(id);
            var estado = db.Estados.Find(a.IdEstado).Uf;
            var cidadinha = db.Cidades.Find(a.IdCidade).Codigo;
            ViewBag.state = new SelectList(db.Estados, "Id", "Nome", a.IdEstado);
            ViewBag.cidade = new SelectList(db.Cidades.Where(c => c.Uf == estado), "Id", "Nome", a.IdCidade);
            ViewBag.bairro = new SelectList(db.Bairros.Where(b => b.Codigo.Contains(cidadinha.ToString())), "Id", "Nome", a.IdBairro);
            if (a != null)
            {
                var Amostra = new AmostraClinicaViewModel
                {
                    Id = a.Id,
                    Nome = a.NomeFantasia,
                    Site = a.LinkSite,
                    Latitude = a.Lt,
                    Longitude = a.Lg,
                    PrecoConsulta = a.PrecoConsulta,
                    PrecoExame = a.PrecoExame,
                    HoraAbertura = a.HoraAbertura,
                    HoraFechamento = a.HoraFechamento,
                    EnderecoFormatado = a.EnderecoFormatado,
                    pontos = a.Pontos,
                    TelefonesClinicas = a.TelefonesClinicas,
                    EspecialidadesEdit = a.Especialidades,
                    ServicosEdit = a.Servicos
                    

                };
                return View(Amostra);
            }
          
            else
                return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarAmostra(AmostraClinicaViewModel dto, int[] servicos, int[] especialidades, string[] cel, int EstadoId, int cidadeId, int bairroId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.estado = new SelectList(db.Estados, "Id", "Descricao");
                return View("VerificarAmostra", "Painel", new { id = dto.Id  });
            }
            Clinica a = new Clinica
            {
                EnderecoFormatado = dto.EnderecoFormatado,
                NomeFantasia = dto.Nome,
                LinkSite = dto.Site,
                Lt = dto.Latitude,
                Lg = dto.Longitude,
                PrecoConsulta = dto.PrecoConsulta,
                PrecoExame = dto.PrecoExame,
                HoraAbertura = dto.HoraAbertura,
                HoraFechamento = dto.HoraFechamento,
                IdEstado = EstadoId,
                IdBairro = bairroId,
                IdCidade = cidadeId
            };
            List<Servico> servs = new List<Servico>();
            foreach (int s in servicos)
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
                db.Clinicas.Add(a);
                db.SaveChanges();
                var tels = db.TelefonesClinicas.Where(t => t.IdAmostraClinica == dto.Id).ToList();
                foreach(var tel in tels)
                {
                    tel.IdClinica = a.Id;
         
                }
                
                db.SaveChanges();
                var identity = User.Identity as ClaimsIdentity;

                int id = Convert.ToInt32(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

                var usuario = db.Usuarios.Find(id);
                if (usuario.Pontos == null)
                {
                    usuario.Pontos = dto.pontos;
                }
                else
                {
                    usuario.Pontos += dto.pontos;
                }
                var amostra = db.AmostrasClinicas.Find(dto.Id).Ativo = false;
                db.SaveChanges();
        
                
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }
    }
}