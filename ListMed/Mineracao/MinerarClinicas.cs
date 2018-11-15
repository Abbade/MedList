using ListMed.DTO;
using ListMed.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ListMed.Mineracao
{
    public class MinerarClinicas
    {
        private MedListContext db = new MedListContext();
        HttpClient client = new HttpClient();

        public MinerarClinicas()
        {
            client.BaseAddress = new Uri("https://maps.googleapis.com");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void listarClinicas(string Localidade)
        {
            Localidade = HttpUtility.UrlPathEncode(Localidade);
            string retorno = "";
            retornoMineracao clinicas = new retornoMineracao();
            HttpResponseMessage response = client.GetAsync("/maps/api/place/textsearch/json?input=clinica%20popular%20rio%20de%20janeiro&inputtype=textquery&fields=photos,formatted_address,name,rating,opening_hours,geometry,place_id,price_level,permanently_closed&key=AIzaSyBbjKpIM4wD3dj3W5VqGuCYMH6hdoGhXP8").Result;

            if (response.IsSuccessStatusCode)
            {

                retorno = response.Content.ReadAsStringAsync().Result;
                clinicas = JsonConvert.DeserializeObject<retornoMineracao>(retorno);
                foreach(var clinica in clinicas.results)
                {
                    Clinica c = new Clinica();
                    string tel1 = "", tel2 = "";
                    var cliAux = db.Clinicas.FirstOrDefault(u => u.NomeFantasia == clinica.name);
                    if (cliAux == null)
                    {
                        c.NomeFantasia = clinica.name;
                        c.PlaceID = clinica.place_id;
                        c.Lt = clinica.geometry.location.lat.ToString();
                        c.Lg = clinica.geometry.location.lng.ToString();
                        c.avaliacao = clinica.rating;
                        c.EnderecoFormatado = clinica.formatted_address.Replace("Brazil", "Brasil"); ;
                        c.IdEstado = 1;
                        HttpResponseMessage response1 = client.GetAsync("/maps/api/place/details/json?placeid=" + clinica.place_id + "&fields=formatted_phone_number,address_components,website,photo&key=AIzaSyBbjKpIM4wD3dj3W5VqGuCYMH6hdoGhXP8").Result;
                        string retorno1 = "";
                        if (response1.IsSuccessStatusCode)
                        {
                            retorno1 = response1.Content.ReadAsStringAsync().Result;
                            var aclinica = JsonConvert.DeserializeObject<retornoPlaceID>(retorno1);
                            var cid = aclinica.result.address_components.Where(a => a.types.Any(s => s.Contains("administrative_area_level_2"))).FirstOrDefault();
                            string cida = cid != null ? cid.short_name : null;
                            if (cida != null)
                            {
                                var cidade = db.Cidades.Where(d => d.Descricao.ToUpper() == cida.ToUpper()).FirstOrDefault();
                                if (cidade != null)
                                    c.IdCidade = cidade.Id;
                            }
                            var bair = aclinica.result.address_components.Where(a => a.types.Any(s => s.Contains("locality"))).FirstOrDefault();
                            string bairri = bair != null ? bair.short_name : null;
                            if (bairri != null)
                            {
                                var bairro = db.Bairros.Where(d => d.Descricao.ToUpper() == bairri.ToUpper()).FirstOrDefault();
                                if (bairro != null)
                                    c.IdBairro = bairro.Id;
                            }
                            c.LinkSite = aclinica.result.website;
                           tel1= aclinica.result.formatted_phone_number;
                            tel2 = aclinica.result.international_phone_number;

                        }

                        db.Clinicas.Add(c);
                        try
                        {
                            if (c.IdBairro != null && c.IdCidade != null && c.IdBairro > 0 && c.IdCidade > 0)
                            {
                                db.SaveChanges();
                                if(!string.IsNullOrEmpty(tel1))
                                    db.TelefonesClinicas.Add(new TelefonesClinica { Numero = tel1, IdClinica = c.Id });
                                if (!string.IsNullOrEmpty(tel2))
                                    db.TelefonesClinicas.Add(new TelefonesClinica { Numero = tel2, IdClinica = c.Id });
                                db.SaveChanges();
                                if (clinica.photos != null && clinica.photos.Count > 0)
                                {
                                    foreach (var foto in clinica.photos)
                                    {
                                        Foto f = new Foto();
                                        f.altura = foto.height;
                                        f.largura = foto.width;
                                        f.URL = foto.html_attributions.FirstOrDefault();
                                        f.IdClinica = c.Id;
                                        db.Fotos.Add(f);
                                        db.SaveChanges();
                                        
                                    }
                                }
                            }

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }


                }
                string proxPag = clinicas.next_page_token;
                for(int i = 0; i < 40; i++)
                {
                    HttpResponseMessage prox = new HttpResponseMessage();
                    prox = client.GetAsync("/maps/api/place/nearbysearch/json?pagetoken="+ proxPag + "&key=AIzaSyBbjKpIM4wD3dj3W5VqGuCYMH6hdoGhXP8").Result;
                    clinicas = JsonConvert.DeserializeObject<retornoMineracao>(retorno);
                    foreach (var clinica in clinicas.results)
                    {
                        string telcli1 = "", telcli2 = "";
                        Clinica c = new Clinica();
                        var cliAux = db.Clinicas.FirstOrDefault(u => u.NomeFantasia == clinica.name);
                        if (cliAux == null)
                        {
                            c.NomeFantasia = clinica.name;
                            c.PlaceID = clinica.place_id;
                            c.Lt = clinica.geometry.location.lat.ToString();
                            c.Lg = clinica.geometry.location.lng.ToString();
                            c.avaliacao = clinica.rating;
                            c.EnderecoFormatado = clinica.formatted_address.Replace("Brazil", "Brasil");
                            c.IdEstado = 1;
                            HttpResponseMessage response1 = client.GetAsync("/maps/api/place/details/json?placeid=" + clinica.place_id + "&fields=formatted_phone_number,address_components,website,photo&key=AIzaSyBbjKpIM4wD3dj3W5VqGuCYMH6hdoGhXP8").Result;
                            string retorno1 = "";
                            if (response1.IsSuccessStatusCode)
                            {
                                retorno1 = response1.Content.ReadAsStringAsync().Result;
                                var aclinica = JsonConvert.DeserializeObject<retornoPlaceID>(retorno1);
                                var cid = aclinica.result.address_components.Where(a => a.types.Any(s => s.Contains("administrative_area_level_2"))).FirstOrDefault();
                                string cida = cid != null ? cid.short_name : null;
                                if (cida != null)
                                {
                                    var cidade = db.Cidades.Where(d => d.Descricao.ToUpper() == cida.ToUpper()).FirstOrDefault();
                                    if (cidade != null)
                                        c.IdCidade = cidade.Id;
                                }
                                var bair = aclinica.result.address_components.Where(a => a.types.Any(s => s.Contains("locality"))).FirstOrDefault();
                                string bairri = bair != null ? bair.short_name : null;
                                if (bairri != null)
                                {
                                    var bairro = db.Bairros.Where(d => d.Descricao.ToUpper() == bairri.ToUpper()).FirstOrDefault();
                                    if (bairro != null)
                                        c.IdBairro = bairro.Id;
                                }
                                c.LinkSite = aclinica.result.website;
                                telcli1 = aclinica.result.formatted_phone_number;
                                telcli2 = aclinica.result.international_phone_number;

                            }

                            db.Clinicas.Add(c);
                            try
                            {
                                if (c.IdBairro != null && c.IdCidade != null && c.IdBairro > 0 && c.IdCidade > 0)
                                {
                                    db.SaveChanges();
                                    if (!string.IsNullOrEmpty(telcli1))
                                        db.TelefonesClinicas.Add(new TelefonesClinica { Numero = telcli1, IdClinica = c.Id });
                                    if (!string.IsNullOrEmpty(telcli2))
                                        db.TelefonesClinicas.Add(new TelefonesClinica { Numero = telcli2, IdClinica = c.Id });
                                    db.SaveChanges();
                                    if (clinica.photos != null && clinica.photos.Count > 0)
                                    {
                                        foreach (var foto in clinica.photos)
                                        {
                                            Foto f = new Foto();
                                            f.altura = foto.height;
                                            f.largura = foto.width;
                                            f.URL = foto.html_attributions.FirstOrDefault();
                                            f.IdClinica = c.Id;
                                            db.Fotos.Add(f);
                                            db.SaveChanges();
                                        }
                                    }
                                }

                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }


                    }
                    proxPag = clinicas.next_page_token;
                }
            }

        }
    }
}