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
            client.BaseAddress = new Uri("https://maps.googleapis.com");
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Inicio
        public ActionResult Index()
        {
            //List<ClinicaDTO> clinicas = new List<ClinicaDTO>();
            //HttpResponseMessage response = client.GetAsync("/maps/api/place/findplacefromtext/json?input=clinica%20popular%20rio%20de%20janeiro&inputtype=textquery&fields=photos,formatted_address,name,rating,opening_hours,geometry,place_id,price_level,permanently_closed&key=AIzaSyBbjKpIM4wD3dj3W5VqGuCYMH6hdoGhXP8").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    string teste = response.Content.ReadAsStringAsync().Result;
            //}
            return View();
        }
    }
}