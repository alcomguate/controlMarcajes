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
        
        
        public ActionResult Index(int? num_empleado)
        {
            if (num_empleado == null)
            {
                return RedirectToAction("Index", "Empleados");
            }
            IEnumerable<Marcaje> marcajesByEmpleado = apiMarcajes.GetMarcajes().ToList().
                Where(x => x.empleado == num_empleado && x.dia_laboral.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()));

            if (marcajesByEmpleado.Count() == 0)
            {
                Marcaje marcaje = new Marcaje();
                marcaje.dia_laboral = DateTime.Now;
                marcaje.empleado = num_empleado;
                apiMarcajes.PostMarcaje(marcaje);

                marcajesByEmpleado = apiMarcajes.GetMarcajes().ToList().
                                Where(x => x.empleado == num_empleado && 
                                x.dia_laboral.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString()));
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
        public ActionResult Create(string num_empleado, string accion)
        {
            try
            {
                int empleado = int.Parse(num_empleado);
                Marcaje marcaje = apiMarcajes.GetMarcajes().ToList().
                    Where(x => x.empleado == empleado && 
                    x.dia_laboral.Value.ToShortDateString().Equals(DateTime.Now.ToShortDateString())).FirstOrDefault();
                switch (accion)
                {
                    case "hora_entrada":
                        TimeSpan hora_entrada = DateTime.Now.TimeOfDay;
                        marcaje.hora_entrada = hora_entrada;
                        break;
                    case "hora_entrada_almuerzo":
                        TimeSpan hora_entrada_almuerzo = DateTime.Now.TimeOfDay;
                        marcaje.hora_entrada_almuerzo = hora_entrada_almuerzo;
                        break;
                    case "hora_salida_almuerzo":
                        TimeSpan hora_salida_almuerzo = DateTime.Now.TimeOfDay;
                        marcaje.hora_salida_almuerzo = hora_salida_almuerzo;
                        break;
                    case "hora_salida":
                        TimeSpan hora_salida = DateTime.Now.TimeOfDay;
                        marcaje.hora_salida = hora_salida;
                        break;
                }
                apiMarcajes.PutMarcaje(marcaje.id, marcaje);
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
