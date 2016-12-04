using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcentracionNotas.Models;
using Inacap.Infraestructura.Utilidad;

namespace ConcentracionNotas.Controllers
{
    public class ProfesorController : Controller
    {
        private ModeloEntities db = new ModeloEntities();

        //
        // GET: /Profesor/

        public ActionResult Index()
        {
            return View(db.Profesor.ToList());
        }

        //
        // GET: /Profesor/Details/5

        public ActionResult Details(int id = 0)
        {
            Profesor profesor = db.Profesor.Single(p => p.ProfesorId == id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        //
        // GET: /Profesor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Profesor/Create

        [HttpPost]
        public ActionResult Create(ProfesorModelo profesor)
        {
            if (ModelState.IsValid)
            {
                var modelo = new Profesor
                {
                    ProfesorNombre = profesor.ProfesorNombre,
                    ProfesorApellido = profesor.ProfesorApellido
                };

                var run = (new RolUnicoVerificador(new RolUnicoNacional() {Rut = profesor.RolUnico}));

                if (run.EsValido())
                {
                    modelo.ProfesorRut = run.ObtenerRolUnico().Numero;
                    modelo.ProfesorRutDigito = run.ObtenerRolUnico().DigitoVerificador;

                    db.Profesor.AddObject(modelo);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("RolUnico", run.ObtenerErrores()[0]);
            }

            return View(profesor);
        }

        //
        // GET: /Profesor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Profesor profesor = db.Profesor.Single(p => p.ProfesorId == id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        //
        // POST: /Profesor/Edit/5

        [HttpPost]
        public ActionResult Edit(Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                db.Profesor.Attach(profesor);
                db.ObjectStateManager.ChangeObjectState(profesor, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profesor);
        }

        //
        // GET: /Profesor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Profesor profesor = db.Profesor.Single(p => p.ProfesorId == id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        //
        // POST: /Profesor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesor profesor = db.Profesor.Single(p => p.ProfesorId == id);
            db.Profesor.DeleteObject(profesor);
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