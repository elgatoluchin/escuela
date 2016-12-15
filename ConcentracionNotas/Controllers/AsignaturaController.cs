using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcentracionNotas.Models;

namespace ConcentracionNotas.Controllers
{
    public class AsignaturaController : BaseController
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

            ViewBag.ProfesorId =
                new SelectList(
                    (from s in db.Profesor
                     select new { s.ProfesorId, Nombre = s.ProfesorNombre + " " + s.ProfesorApellido }
                        ), "ProfesorId", "Nombre", "-");

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

            ViewBag.ProfesorId =
                new SelectList(
                    (from s in db.Profesor
                     select new { s.ProfesorId, Nombre = s.ProfesorNombre + " " + s.ProfesorApellido }
                        ), "ProfesorId", "Nombre", "-");


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

            ViewBag.ProfesorId =
                new SelectList(
                    (from s in db.Profesor
                     select new { s.ProfesorId, Nombre = s.ProfesorNombre + " " + s.ProfesorApellido }
                        ), "ProfesorId", "Nombre", "-");


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
            try
            {
                Asignatura asignatura = db.Asignatura.Single(a => a.AsignaturaId == id);
                db.Asignatura.DeleteObject(asignatura);
                db.SaveChanges();
            }
            catch (System.Data.UpdateException e)
            {
                var ex = e.GetBaseException() as SqlException;

                if (ex != null)
                {
                    if (ex.Errors.Count > 0)
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 547:
                                Danger("No puede eliminar esta asignatura porque está siendo usada por otra entidad",
                                    true);
                                break;
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}