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
    public class ItemCopyController : ApiController
    {
        private AuthContext db = new AuthContext();

        // GET: api/ItemCopy
        public IQueryable<ItemMaster> GetitemMasters()
        {
            return db.itemMasters;
        }

        // GET: api/ItemCopy/5
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

        // POST: api/ItemCopy/5
        [ResponseType(typeof(void))]
        public List<MoveWarehouse> PostItemMaster(List<MoveWarehouse> item)
        {
            int Warehid = item[0].Warehouseid;

           
            try
            {
               
                //item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }

                db.AddItemMove(item, Warehid);
                
                return item;

            }
            catch (Exception ex)
            {
               

                return null;
            }



        }

      

        // DELETE: api/ItemCopy/5
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