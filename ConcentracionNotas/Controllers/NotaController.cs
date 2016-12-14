using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcentracionNotas.Models;

namespace ConcentracionNotas.Controllers
{
    public class NotaController : Controller
    {
        private ModeloEntities db = new ModeloEntities();

        //
        // GET: /Nota/

        public ActionResult Index()
        {
            var nota = db.Nota.Include("Concentracion");
            return View(nota.ToList());
        }

        //
        // GET: /Nota/Details/5

        public ActionResult Details(int id = 0)
        {
            Nota nota = db.Nota.Single(n => n.NotaId == id);
            if (nota == null)
            {
                return HttpNotFound();
            }
            return View(nota);
        }

        //
        // GET: /Nota/Create

        public ActionResult Create()
        {
            ViewBag.ConcentFolio = new SelectList(db.Concentracion, "ConcentFolio", "ConcentFolio");
            return View();
        }

        //
        // POST: /Nota/Create

        [HttpPost]
        public ActionResult Create(Nota nota)
        {
            if (ModelState.IsValid)
            {
                db.Nota.AddObject(nota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConcentFolio = new SelectList(db.Concentracion, "ConcentFolio", "ConcentFolio", nota.ConcentFolio);
            return View(nota);
        }

        //
        // GET: /Nota/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Nota nota = db.Nota.Single(n => n.NotaId == id);
            if (nota == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConcentFolio = new SelectList(db.Concentracion, "ConcentFolio", "ConcentFolio", nota.ConcentFolio);
            return View(nota);
        }

        //
        // POST: /Nota/Edit/5

        [HttpPost]
        public ActionResult Edit(Nota nota)
        {
            if (ModelState.IsValid)
            {
                db.Nota.Attach(nota);
                db.ObjectStateManager.ChangeObjectState(nota, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConcentFolio = new SelectList(db.Concentracion, "ConcentFolio", "ConcentFolio", nota.ConcentFolio);
            return View(nota);
        }

        //
        // GET: /Nota/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Nota nota = db.Nota.Single(n => n.NotaId == id);
            if (nota == null)
            {
                return HttpNotFound();
            }
            return View(nota);
        }

        //
        // POST: /Nota/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Nota nota = db.Nota.Single(n => n.NotaId == id);
            db.Nota.DeleteObject(nota);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}