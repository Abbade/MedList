using ListMed.DTO;
using ListMed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ListMed.Controllers
{
    public class InicioController : Controller
    {
        private MedListContext db = new MedListContext();
        HttpClient client = new HttpClient();

        public InicioController()
        {
            //client.BaseAddress = new Uri("https://maps.googleapis.com");
            
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Inicio
        public ActionResult Index()
        {
            //List<ClinicaDTO> clinicas = new List<ClinicaDTO>();
            //HttpResponseMessage response = client.GetAsync("/maps/api/place/textsearch/json?input=clinica%20popular%20rio%20de%20janeiro&inputtype=textquery&fields=photos,formatted_address,name,rating,opening_hours,geometry,place_id,price_level,permanently_closed&key=AIzaSyBbjKpIM4wD3dj3W5VqGuCYMH6hdoGhXP8").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    string teste = response.Content.ReadAsStringAsync().Result;
            //}
            return View();
        }

        [HttpPost]
        public JsonResult ListarLocalidades(string nome)
        {
            var localidades = db.Localidades.Where(l => (l.Tipo == "C" || l.Tipo == "E")  && l.Descricao.ToUpper().Contains(nome.ToUpper())).Select(a => new
            {
                label = a.Descricao,
                value = a.Descricao
            }).ToList();
            return Json(localidades);
        }

        [HttpPost]
        public JsonResult AddEstado(List<estados> estados)
        {
            foreach(var estado in estados)
            {
                db.Localidades.Add(new Localidade { Descricao = estado.desc, UF = estado.uf, Tipo = "E" });
            }
            db.SaveChanges();
            return Json("OK");
        }
    }
}