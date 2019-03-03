using ControlMarcaje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlMarcaje.Controllers
{
    public class MarcajesController : Controller
    {

        ApiMarcajesController apiMarcajes = new ApiMarcajesController();
        ApiEmpleadosController apiEmpleados = new ApiEmpleadosController();
        
        
        public ActionResult Index(int num_empleado)
        {
            IEnumerable<Marcaje> marcajesByEmpleado = apiMarcajes.GetMarcajes().ToList().
                Where(x => x.Empleado1.num_empleado == num_empleado && x.dia_laboral.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()));

            if (marcajesByEmpleado.GetEnumerator().Current == null)
            {
                Marcaje marcaje = new Marcaje();
                marcaje.dia_laboral = DateTime.Now;
                marcaje.empleado = num_empleado;
                apiMarcajes.PostMarcaje(marcaje);

                marcajesByEmpleado = apiMarcajes.GetMarcajes().ToList().
                                Where(x => x.Empleado1.num_empleado == num_empleado && x.dia_laboral.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()));
            }

            return View(marcajesByEmpleado);
        }

        // GET: Marcajes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Marcajes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcajes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Marcajes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Marcajes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Marcajes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Marcajes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
