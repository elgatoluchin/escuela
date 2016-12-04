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
    public class AsignaturaController : Controller
    {
        private ModeloEntities db = new ModeloEntities();

        //
        // GET: /Asignatura/

        public ActionResult Index()
        {
            var asignatura = db.Asignatura.Include("Profesor");
            return View(asignatura.ToList());
        }

        //
        // GET: /Asignatura/Details/5

        public ActionResult Details(int id = 0)
        {
            Asignatura asignatura = db.Asignatura.Single(a => a.AsignaturaId == id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            return View(asignatura);
        }

        //
        // GET: /Asignatura/Create

        public ActionResult Create()
        {
            //ViewBag.ProfesorId = new SelectList(db.Profesor, "ProfesorId", "ProfesorNombre");

            ViewBag.ProfesorId =
                new SelectList(
                    (from s in db.Profesor
                        select new {s.ProfesorId, Nombre = s.ProfesorNombre + " " + s.ProfesorApellido}
                        ), "ProfesorId", "Nombre", "-");

            return View();
        }

        //
        // POST: /Asignatura/Create

        [HttpPost]
        public ActionResult Create(Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                db.Asignatura.AddObject(asignatura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfesorId = new SelectList(db.Profesor, "ProfesorId", "ProfesorRutDigito", asignatura.ProfesorId);
            return View(asignatura);
        }

        //
        // GET: /Asignatura/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Asignatura asignatura = db.Asignatura.Single(a => a.AsignaturaId == id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfesorId = new SelectList(db.Profesor, "ProfesorId", "ProfesorRutDigito", asignatura.ProfesorId);
            return View(asignatura);
        }

        //
        // POST: /Asignatura/Edit/5

        [HttpPost]
        public ActionResult Edit(Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                db.Asignatura.Attach(asignatura);
                db.ObjectStateManager.ChangeObjectState(asignatura, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfesorId = new SelectList(db.Profesor, "ProfesorId", "ProfesorRutDigito", asignatura.ProfesorId);
            return View(asignatura);
        }

        //
        // GET: /Asignatura/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Asignatura asignatura = db.Asignatura.Single(a => a.AsignaturaId == id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            return View(asignatura);
        }

        //
        // POST: /Asignatura/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Asignatura asignatura = db.Asignatura.Single(a => a.AsignaturaId == id);
            db.Asignatura.DeleteObject(asignatura);
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