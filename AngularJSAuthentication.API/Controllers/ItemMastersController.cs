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
    public class ItemMastersController : ApiController
    {
        private AuthContext db = new AuthContext();
        // GET: api/ItemMasters
        public IQueryable<ItemMaster> GetitemMasters()
        {
            return db.itemMasters;
        }
        // GET: api/ItemMasters/5
        [ResponseType(typeof(ItemMaster))]
        public IHttpActionResult GetItemMaster(int id)
        {
            ItemMaster itemMaster = db.itemMasters.Find(id);
            if (itemMaster == null)
            {
                return NotFound();
            }
            return Ok(itemMaster);
        }
        // PUT: api/ItemMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItemMaster(int id, ItemMaster itemMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != itemMaster.ItemId)
            {
                return BadRequest();
            }
            db.Entry(itemMaster).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMasterExists(id))
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
        // POST: api/ItemMasters
        [ResponseType(typeof(ItemMaster))]
        public IHttpActionResult PostItemMaster(ItemMaster itemMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.itemMasters.Add(itemMaster);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = itemMaster.ItemId }, itemMaster);
        }
        // DELETE: api/ItemMasters/5
        [ResponseType(typeof(ItemMaster))]
        public IHttpActionResult DeleteItemMaster(int id)
        {
            ItemMaster itemMaster = db.itemMasters.Find(id);
            if (itemMaster == null)
            {
                return NotFound();
            }
            db.itemMasters.Remove(itemMaster);
            db.SaveChanges();
            return Ok(itemMaster);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ItemMasterExists(int id)
        {
            return db.itemMasters.Count(e => e.ItemId == id) > 0;
        }
    }
}