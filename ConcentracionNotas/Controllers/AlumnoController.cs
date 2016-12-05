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
    /// <summary>
    /// hola
    /// </summary>
    public class AlumnoController : Controller
    {
        private ModeloEntities db = new ModeloEntities();

        //
        // GET: /Alumno/prueba

        public ActionResult Index()
        {
            return View(db.Alumno.ToList());
        }

        //
        // GET: /Alumno/Details/5

        public ActionResult Details(int id = 0)
        {
            Alumno alumno = db.Alumno.Single(a => a.AlumnoId == id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        //
        // GET: /Alumno/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Alumno/Create

        [HttpPost]
        public ActionResult Create(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Alumno.AddObject(alumno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alumno);
        }

        //
        // GET: /Alumno/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Alumno alumno = db.Alumno.Single(a => a.AlumnoId == id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        //
        // POST: /Alumno/Edit/5

        [HttpPost]
        public ActionResult Edit(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Alumno.Attach(alumno);
                db.ObjectStateManager.ChangeObjectState(alumno, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alumno);
        }

        //
        // GET: /Alumno/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Alumno alumno = db.Alumno.Single(a => a.AlumnoId == id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        //
        // POST: /Alumno/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Alumno alumno = db.Alumno.Single(a => a.AlumnoId == id);
            db.Alumno.DeleteObject(alumno);
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