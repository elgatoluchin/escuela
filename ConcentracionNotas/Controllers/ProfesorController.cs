using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcentracionNotas.Models;
using Inacap.Infraestructura.Utilidad;

namespace ConcentracionNotas.Controllers
{
    public class ProfesorController : BaseController
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
            try
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
                        var numb = run.ObtenerRolUnico().Numero;
                        var prof = db.Profesor.SingleOrDefault(o => o.ProfesorRut == numb);

                        if (prof != null)
                        {
                            Danger("El profesor ya existe", true);
                        }
                        else
                        {
                            modelo.ProfesorRut = run.ObtenerRolUnico().Numero;
                            modelo.ProfesorRutDigito = run.ObtenerRolUnico().DigitoVerificador;

                            db.Profesor.AddObject(modelo);
                            db.SaveChanges();

                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Danger(ex.Message, true);
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
            try
            {
                Profesor profesor = db.Profesor.Single(p => p.ProfesorId == id);
                db.Profesor.DeleteObject(profesor);
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
                                Danger("No puede eliminar este profesor porque está siendo usada por otra entidad",true);
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