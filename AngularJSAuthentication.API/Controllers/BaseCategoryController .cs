using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using System.Data.Entity;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/BaseCategory")]
    public class BaseCategoryController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        AuthContext db = new AuthContext();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Authorize]
        [Route("")]
        public IEnumerable<BaseCategory> Get()
        {
            logger.Info("start Category: ");
            List<BaseCategory> ass = new List<BaseCategory>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                // Access claims
                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                ass = db.BaseCategoryDb.Where(x=>x.Deleted == false).ToList();
                logger.Info("End  BaseCategory: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in BaseCategory " + ex.Message);
                logger.Info("End  BaseCategory: ");
                return null;
            }
        }
        
        [ResponseType(typeof(BaseCategory))]
        [Route("")]
        [AcceptVerbs("POST")]
        public BaseCategory add(BaseCategory item)
        {
            logger.Info("start addCategory: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                // Access claims
                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                item.CreatedDate = indianTime;
                item.UpdatedDate = indianTime;
                item.IsActive = true;
                item.Deleted = false;
                db.BaseCategoryDb.Add(item);
                int id = db.SaveChanges();

                //AngularJSAuthentication.API.Helper.refreshCategory();
                logger.Info("End  addCategory: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addCategory " + ex.Message);
                logger.Info("End  addCategory: ");
                return null;
            }
        }

        [ResponseType(typeof(BaseCategory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public BaseCategory Put(BaseCategory item)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

                if (item != null) {
                    item.UpdatedDate = indianTime;
                    item.LogoUrl = item.LogoUrl;
                    db.BaseCategoryDb.Attach(item);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                } else {
                    return null;
                }

                AngularJSAuthentication.API.Helper.refreshCategory();
                return item;
            }
            catch
            {
                return null;
            }
        }

        [ResponseType(typeof(BaseCategory))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start del Category: ");
            try
            {

                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid = 0;
                foreach (Claim claim in identity.Claims)
                {
                    if (claim.Type == "compid")
                    {
                        compid = int.Parse(claim.Value);
                    }
                    if (claim.Type == "userid")
                    {
                        userid = int.Parse(claim.Value);
                    }
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                BaseCategory category = db.BaseCategoryDb.Where(x => x.BaseCategoryId == id && x.Deleted == false).FirstOrDefault();
                category.Deleted = true;
                category.IsActive = false;
                category.UpdatedDate = indianTime;
                db.BaseCategoryDb.Attach(category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                AngularJSAuthentication.API.Helper.refreshCategory();
                logger.Info("End  delete Category: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in del Category " + ex.Message);


            }
        }
    }
}



