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
    public class SubsubCategoriesJsonApiController : ApiController
    {
        private AuthContext db = new AuthContext();

        // GET: api/SubsubCategoriesJsonApi
        public IQueryable<SubsubCategory> GetSubsubCategorys()
        {
            var subsubCate = from s in db.SubsubCategorys.Include("itemMaster") select s ;
            return subsubCate;
            //return db.SubsubCategorys;
        }

        // GET: api/SubsubCategoriesJsonApi/5
        [ResponseType(typeof(SubsubCategory))]
        public IHttpActionResult GetSubsubCategory(int id)
        {
            SubsubCategory subsubCategory = db.SubsubCategorys.Find(id);
            if (subsubCategory == null)
            {
                return NotFound();
            }

            return Ok(subsubCategory);
        }

        // PUT: api/SubsubCategoriesJsonApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubsubCategory(int id, SubsubCategory subsubCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subsubCategory.SubsubCategoryid)
            {
                return BadRequest();
            }

            db.Entry(subsubCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubsubCategoryExists(id))
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

        // POST: api/SubsubCategoriesJsonApi
        [ResponseType(typeof(SubsubCategory))]
        public IHttpActionResult PostSubsubCategory(SubsubCategory subsubCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SubsubCategorys.Add(subsubCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = subsubCategory.SubsubCategoryid }, subsubCategory);
        }

        // DELETE: api/SubsubCategoriesJsonApi/5
        [ResponseType(typeof(SubsubCategory))]
        public IHttpActionResult DeleteSubsubCategory(int id)
        {
            SubsubCategory subsubCategory = db.SubsubCategorys.Find(id);
            if (subsubCategory == null)
            {
                return NotFound();
            }

            db.SubsubCategorys.Remove(subsubCategory);
            db.SaveChanges();

            return Ok(subsubCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubsubCategoryExists(int id)
        {
            return db.SubsubCategorys.Count(e => e.SubsubCategoryid == id) > 0;
        }
    }
}