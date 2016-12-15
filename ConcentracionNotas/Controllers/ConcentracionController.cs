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

            ViewBag.AlumnoId =
                new SelectList(
                    (from o in db.Alumno select new { o.AlumnoId, Nombre = o.AlumnoNombre + " " + o.AlumnoApellido }),
                    "AlumnoId", "Nombre", "-");

            ViewBag.AsignaturaId =
                new SelectList((from o in db.Asignatura select new { o.AsignaturaId, Nombre = o.AsignaturaNombre }),
                    "AsignaturaId", "Nombre", "-");

            return View();
        }

        //
        // POST: /Concentracion/Create

        [HttpPost]
        public ActionResult Create(Concentracion concentracion)
        {
            if (ModelState.IsValid)
            {
                var concent = new Concentracion
                {
                    AsignaturaId = concentracion.AsignaturaId,
                    AlumnoId = concentracion.AlumnoId
                };

                db.Concentracion.AddObject(concent);
                db.SaveChanges();

                for (var i = 0; i < 4; i++)
                {
                    var nota = new Nota
                    {
                        ConcentFolio = concent.ConcentFolio,
                        NotaNumero = Convert.ToInt16(i+1),
                        NotaPonderacion = 25,
                        NotaObtenido = 0
                    };

                    db.Nota.AddObject(nota);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.AlumnoId =
                new SelectList(
                    (from o in db.Alumno select new {o.AlumnoId, Nombre = o.AlumnoNombre + " " + o.AlumnoApellido}),
                    "AlumnoId", "Nombre", "-");

            ViewBag.AsignaturaId =
                new SelectList((from o in db.Asignatura select new {o.AsignaturaId, Nombre = o.AsignaturaNombre}),
                    "AsignaturaId", "Nombre", "-");


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
            return View(concentracion);
        }

        //
        // POST: /Concentracion/Edit/5

        [HttpPost]
        public ActionResult Edit(Concentracion concentracion)
        {
            if (ModelState.IsValid)
            {
                Concentracion modelo = db.Concentracion.Single(c => c.ConcentFolio == concentracion.ConcentFolio);
                modelo.ConcentAsistencia = concentracion.ConcentAsistencia;
                db.ObjectStateManager.ChangeObjectState(modelo, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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