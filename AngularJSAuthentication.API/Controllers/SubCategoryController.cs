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


namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/SubCategory")]
    public class SubCategoryController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<SubCategory> Get()
        {
            logger.Info("start SubCategory: ");
            List<SubCategory> ass = new List<SubCategory>();
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
                ass = context.AllSubCategory(compid).ToList();
                logger.Info("End  SubCategory: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in SubCategory " + ex.Message);
                logger.Info("End  SubCategory: ");
                return null;
            }
        }

        [Authorize]
        [Route("")]
        public IEnumerable<SubCategory> Get(string id)
        {
            logger.Info("start SubCategory: ");
            List<SubCategory> ass = new List<SubCategory>();
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
                int idd = Int32.Parse(id);
                ass = context.AllSubCategoryy(idd).ToList();
                logger.Info("End  SubCategory: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in SubCategory " + ex.Message);
                logger.Info("End  SubCategory: ");
                return null;
            }
        }

       
        [Authorize]
        [Route("")]
        public IEnumerable<WarehouseSubCategory> Get(string recordtype, int whid)
        {
            if (recordtype == "warehouse")
            {
                logger.Info("start Category: ");
                List<SubCategory> subCategory = new List<SubCategory>();
                List<Warehouse> Warehouse = new List<Warehouse>();
                List<WarehouseSubCategory> warehouseSubCategory = new List<WarehouseSubCategory>();
                logger.Info("start SubCategory: ");

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
                    subCategory = context.AllSubCategory(compid).ToList();
                    Warehouse = context.AllWarehouse(compid).ToList();
                    var cat = (from c in subCategory where c.Warehouseid.Equals(whid) select c).ToList();
                    var war = (from c in Warehouse where c.Warehouseid.Equals(whid) select c).SingleOrDefault();
                    for (int i = 0; i < cat.Count; i++)
                    {
                        WarehouseSubCategory wc = new WarehouseSubCategory();
                        wc.SubCategoryId = cat[i].SubCategoryId;
                        wc.SubcategoryName = cat[i].SubcategoryName;
                        wc.Warehouseid = cat[i].Warehouseid;
                        wc.Stateid = war.Stateid;
                        wc.State = war.StateName;
                        wc.Cityid = war.Cityid;
                        wc.City = war.CityName;
                        warehouseSubCategory.Add(wc);
                    }
                    logger.Info("End   Sub Category: ");
                    return warehouseSubCategory;
                 }
                 catch (Exception ex)
                   {
                      logger.Error("Error in SubCategory " + ex.Message);
                      logger.Info("End  SubCategory: ");
                      return null;
                   }
            }
            return null;
        }


        [ResponseType(typeof(SubCategory))]
        [Route("")]
        [AcceptVerbs("POST")]
        public SubCategory add(SubCategory item)
        {
            logger.Info("start add SubCategory: ");
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
                context.AddSubCategory(item);
                logger.Info("End  add SubCategory: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in add SubCategory " + ex.Message);
                logger.Info("End  add SubCategory: ");
                return null;
            }
        }

        [ResponseType(typeof(SubCategory))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public SubCategory Put(SubCategory item)
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
                return context.PutSubCategory(item);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(SubCategory))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start delete SubCategory: ");
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
                context.DeleteSubCategory(id);
                logger.Info("End  delete  SubCategory: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in delete SubCategory " + ex.Message);


            }
        }
    }
}



