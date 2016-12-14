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
    public class ConcentracionController : Controller
    {
        private ModeloEntities db = new ModeloEntities();

        //
        // GET: /Concentracion/

        public ActionResult Index()
        {
            var concentracion = db.Concentracion.Include("Alumno").Include("Asignatura");
            return View(concentracion.ToList());
        }

        //
        // GET: /Concentracion/Details/5

        public ActionResult Details(int id = 0)
        {
            Concentracion concentracion = db.Concentracion.Single(c => c.ConcentFolio == id);
            if (concentracion == null)
            {
                return HttpNotFound();
            }
            return View(concentracion);
        }

        //
        // GET: /Concentracion/Create

        public ActionResult Create()
        {
            ViewBag.AlumnoId = new SelectList(db.Alumno, "AlumnoId", "AlumnoRutDigito");
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "AsignaturaNombre");
            return View();
        }

        //
        // POST: /Concentracion/Create

        [HttpPost]
        public ActionResult Create(Concentracion concentracion)
        {
            if (ModelState.IsValid)
            {
                db.Concentracion.AddObject(concentracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlumnoId = new SelectList(db.Alumno, "AlumnoId", "AlumnoRutDigito", concentracion.AlumnoId);
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "AsignaturaNombre", concentracion.AsignaturaId);
            return View(concentracion);
        }

        //
        // GET: /Concentracion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Concentracion concentracion = db.Concentracion.Single(c => c.ConcentFolio == id);
            if (concentracion == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlumnoId = new SelectList(db.Alumno, "AlumnoId", "AlumnoRutDigito", concentracion.AlumnoId);
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "AsignaturaNombre", concentracion.AsignaturaId);
            return View(concentracion);
        }

        //
        // POST: /Concentracion/Edit/5

        [HttpPost]
        public ActionResult Edit(Concentracion concentracion)
        {
            if (ModelState.IsValid)
            {
                db.Concentracion.Attach(concentracion);
                db.ObjectStateManager.ChangeObjectState(concentracion, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlumnoId = new SelectList(db.Alumno, "AlumnoId", "AlumnoRutDigito", concentracion.AlumnoId);
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "AsignaturaNombre", concentracion.AsignaturaId);
            return View(concentracion);
        }

        //
        // GET: /Concentracion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Concentracion concentracion = db.Concentracion.Single(c => c.ConcentFolio == id);
            if (concentracion == null)
            {
                return HttpNotFound();
            }
            return View(concentracion);
        }

        //
        // POST: /Concentracion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Concentracion concentracion = db.Concentracion.Single(c => c.ConcentFolio == id);
            db.Concentracion.DeleteObject(concentracion);
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