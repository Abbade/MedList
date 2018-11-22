using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListMed.DTO;
using ListMed.Geral;

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
                Cidade = a.Cidade.Descricao,
                Estado = a.Estado.Descricao,
                NomeUsuario = a.Usuario.nick
            }).ToList()
            );
        }
        public ActionResult VerificarAmostra(int id)
        {
   
            var a = db.AmostrasClinicas.Find(id);
            ViewBag.estado = new SelectList(db.Estados, "Id", "Descricao", a.IdEstado);
            ViewBag.cidade = new SelectList(db.Cidades, "Id", "Descricao", a.IdCidade);
            ViewBag.bairro = new SelectList(db.Bairros, "Id", "Descricao", a.IdBairro);
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
        public ActionResult ConfirmarAmostra(AmostraClinicaViewModel dto, int[] servicos, int[] especialidades, string[] cel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.estado = new SelectList(db.Estados, "Id", "Descricao");
                return View("VerificarAmostra", "Painel", new { id = dto.Id  });
            }

            return View("Index");
        }
    }
}