using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ControlMarcaje.Models;

namespace ControlMarcaje.Controllers
{
    public class ApiMarcajesController : ApiController
    {
        private CONTROL_MARCAJEEntities db = new CONTROL_MARCAJEEntities();

        // GET: api/ApiMarcajes
        public IQueryable<Marcaje> GetMarcajes()
        {
            return db.Marcajes;
        }

        // GET: api/ApiMarcajes/5
        [ResponseType(typeof(Marcaje))]
        public IHttpActionResult GetMarcaje(int id)
        {
            Marcaje marcaje = db.Marcajes.Find(id);
            if (marcaje == null)
            {
                return NotFound();
            }

            return Ok(marcaje);
        }

        // PUT: api/ApiMarcajes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarcaje(int id, Marcaje marcaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marcaje.id)
            {
                return BadRequest();
            }

            db.Entry(marcaje).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApiMarcajes
        [ResponseType(typeof(Marcaje))]
        public IHttpActionResult PostMarcaje(Marcaje marcaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marcajes.Add(marcaje);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = marcaje.id }, marcaje);
        }

        // DELETE: api/ApiMarcajes/5
        [ResponseType(typeof(Marcaje))]
        public IHttpActionResult DeleteMarcaje(int id)
        {
            Marcaje marcaje = db.Marcajes.Find(id);
            if (marcaje == null)
            {
                return NotFound();
            }

            db.Marcajes.Remove(marcaje);
            db.SaveChanges();

            return Ok(marcaje);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarcajeExists(int id)
        {
            return db.Marcajes.Count(e => e.id == id) > 0;
        }
    }
}