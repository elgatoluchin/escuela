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

        public ActionResult Index(int id = 0)
        {
            var nota = db.Nota.Include("Concentracion").Where(o => o.ConcentFolio == id);
            ViewBag.Concentracion = db.Concentracion.Single(o => o.ConcentFolio == id);
            
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

                Concentracion concent = db.Concentracion.Single(o => o.ConcentFolio == nota.ConcentFolio);

                var notas = db.Nota.Include("Concentracion").Where(o => o.ConcentFolio == nota.ConcentFolio);

                decimal total = 0;
                int validas = 0;
                foreach (var nta in notas.ToList())
                {
                    validas += (null == nta.NotaObtenido || 0 == nta.NotaObtenido) ? 0 : 1;
                    total += nta.NotaObtenido ?? 0;
                }

                decimal promedio = (total > 0) ? total / 4 : 0;

                concent.ConcentPromedio = promedio;

                if (4 == validas)
                {
                    concent.ConcentSituacion = Convert.ToInt16((promedio < 4.0M) ? 2 : 1);
                }
                else
                {
                    concent.ConcentSituacion = 0;
                }

                db.ObjectStateManager.ChangeObjectState(concent, EntityState.Modified);
                db.SaveChanges();

                return RedirectToAction("Index", new {id = nota.ConcentFolio});
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