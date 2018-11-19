using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListMed.DTO;
namespace ListMed.Controllers
{
    public class PainelController : Controller
    {
        private MedListContext db = new MedListContext();
        // GET: Painel
        public ActionResult Index()
        {
         
            return View(db.AmostrasClinicas.Select(a => new ListaAmostra
            {
                Id = a.Id,
                Nome = a.NomeFantasia,
                Site = a.LinkSite,
                Cidade = a.Cidade.Descricao,
                Estado = a.Estado.Descricao
            }).ToList()
            );
        }
        public ActionResult VerificarAmostra(int id)
        {
            var a = db.AmostrasClinicas.Find(id);
            
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
                    TelefonesClinicas = a.TelefonesClinicas
                    

                };
                return View(Amostra);
            }
          
            else
                return HttpNotFound();
        }
    }
}