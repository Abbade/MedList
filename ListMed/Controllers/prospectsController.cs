using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ListMed.Models;

namespace ListMed.Controllers
{
    public class prospectsController : Controller
    {
        private MedListContext db = new MedListContext();

        // GET: prospects
        public ActionResult Index()
        {
            return View(db.Clinicas.ToList());
        }

        // GET: prospects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = db.Clinicas.Find(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // GET: prospects/Create
        public ActionResult Create()
        {
            ViewBag.Estados = new SelectList(db.Localidades.Where(a => a.Tipo.Equals("E")), "Id", "Descricao");
            ViewBag.Cidades = new SelectList(db.Localidades.Where(a => a.Tipo.Equals("C")), "Id", "Descricao");
            ViewBag.Bairros = new SelectList(db.Localidades.Where(a => a.Tipo.Equals("B")), "Id", "Descricao");
            return View();
        }

        // POST: prospects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlaceID,NomeFantasia,TituloSite,Snippet,LinkSite,Lt,Lg,EnderecoFormatado,avaliacao,Preco,HoraAbertura,HoraFechamento,Telefone1,Telefone2")] Clinica clinica, int estado = 0, int bairro = 0, int cidade = 0)
        {
            if (ModelState.IsValid)
            {
                if(estado > 0)
                {
                  
                    var local = db.Localidades.Find(estado);
                    if (local != null)
                        clinica.Localidades.Add(local);
                    
                }
                if (cidade > 0)
                {
                    
                    var local = db.Localidades.Find(cidade);
                    if (local != null)
                        clinica.Localidades.Add(local);
                    
                }
                if (bairro> 0)
                {
                    
                    var local = db.Localidades.Find(bairro);
                    if (local != null)
                        clinica.Localidades.Add(local);
                    
                }
                db.Clinicas.Add(clinica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clinica);
        }

        // GET: prospects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = db.Clinicas.Find(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: prospects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlaceID,NomeFantasia,TituloSite,Snippet,LinkSite,Lt,Lg,EnderecoFormatado,avaliacao,Preco,HoraAbertura,HoraFechamento,Telefone1,Telefone2")] Clinica clinica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clinica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clinica);
        }

        // GET: prospects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = db.Clinicas.Find(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: prospects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clinica clinica = db.Clinicas.Find(id);
            db.Clinicas.Remove(clinica);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
