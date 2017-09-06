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
using AngularJSAuthentication.API;
using AngularJSAuthentication.Model;

namespace AngularJSAuthentication.API.Controllers
{
    public class WarehouseCatApiController : ApiController
    {
        private AuthContext db = new AuthContext();

        // GET: api/WarehouseCatApi
        public IQueryable<WarehouseCategory> GetDbWarehouseCategory()
        {
            var items = from i in db.DbWarehouseCategory.Include("ItemMaster") select i;
            return items;
            //return db.DbWarehouseCategory;
        }

        // GET: api/WarehouseCatApi/5
        [ResponseType(typeof(WarehouseCategory))]
        public IHttpActionResult GetWarehouseCategory(int id)
        {
            WarehouseCategory warehouseCategory = db.DbWarehouseCategory.Find(id);
            if (warehouseCategory == null)
            {
                return NotFound();
            }

            return Ok(warehouseCategory);
        }

        // PUT: api/WarehouseCatApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWarehouseCategory(int id, WarehouseCategory warehouseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warehouseCategory.WhCategoryid)
            {
                return BadRequest();
            }

            db.Entry(warehouseCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseCategoryExists(id))
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

        // POST: api/WarehouseCatApi
        [ResponseType(typeof(WarehouseCategory))]
        public IHttpActionResult PostWarehouseCategory(WarehouseCategory warehouseCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DbWarehouseCategory.Add(warehouseCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = warehouseCategory.WhCategoryid }, warehouseCategory);
        }

        // DELETE: api/WarehouseCatApi/5
        [ResponseType(typeof(WarehouseCategory))]
        public IHttpActionResult DeleteWarehouseCategory(int id)
        {
            WarehouseCategory warehouseCategory = db.DbWarehouseCategory.Find(id);
            if (warehouseCategory == null)
            {
                return NotFound();
            }

            db.DbWarehouseCategory.Remove(warehouseCategory);
            db.SaveChanges();

            return Ok(warehouseCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WarehouseCategoryExists(int id)
        {
            return db.DbWarehouseCategory.Count(e => e.WhCategoryid == id) > 0;
        }
    }
}