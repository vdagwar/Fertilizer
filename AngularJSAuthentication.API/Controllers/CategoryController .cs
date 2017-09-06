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
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<Category> Get()
        {
            logger.Info("start Category: ");
            List<Category> ass = new List<Category>();
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
                ass = context.AllCategory(compid).ToList();
                logger.Info("End  Category: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Category " + ex.Message);
                logger.Info("End  Category: ");
                return null;
            }
        }

        [Authorize]
        [Route("")]
        public IEnumerable<WarehouseCategory> Get(string recordtype, int whid)
        {
            if (recordtype == "warehouse")
            {
                logger.Info("start Category: ");
                List<Category> Category = new List<Category>();
                List<Warehouse> Warehouse = new List<Warehouse>();
                List<WarehouseCategory> WarehouseCategory = new List<WarehouseCategory>();
                List<WarehouseCategory> wareH = new List<WarehouseCategory>();
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
                    Category = context.AllCategory(compid).ToList();
                    Warehouse = context.AllWarehouse(compid).ToList();
                    wareH = context.AllWarehouseCategory(compid).ToList();
                    //var cat = (from c in Category where c.Warehouseid.Equals(whid) select c).ToList();
                    var cat = Category;
                    var war = (from c in Warehouse where c.Warehouseid.Equals(whid) select c).SingleOrDefault();
                    //var cat = (from c in Category where c.Warehouseid.Equals(whid) select c).ToList();
                    for (int i = 0; i < cat.Count; i++)
                    {
                        //var wcat = (from c in wareH where c.Warehouseid == whid && c.Categoryid == cat[i].Categoryid select c).SingleOrDefault();
                        List<WarehouseCategory> wcat = (from c in wareH where c.Warehouseid == whid && c.Deleted==false select c).ToList();
                        
                        WarehouseCategory wc = new WarehouseCategory();
                        wc.Categoryid = cat[i].Categoryid;
                        wc.CategoryName = cat[i].CategoryName;
                        wc.Warehouseid = whid;
                        wc.Stateid = war.Stateid;
                        wc.State = war.StateName;
                        wc.Cityid = war.Cityid;
                        wc.City = war.CityName;
                        foreach (var c in wcat)
                        {
                            if (c.Categoryid.Equals(cat[i].Categoryid))
                            {
                                wc.WhCategoryid = c.WhCategoryid;
                                wc.IsVisible = true;
                                wc.SortOrder = c.SortOrder;
                            }
                        }

                        WarehouseCategory.Add(wc);
                        

                    }
                    logger.Info("End  Category: ");
                    return WarehouseCategory;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in Category " + ex.Message);
                    logger.Info("End  Category: ");
                    return null;
                }
            }
            return null;
        }

        [Authorize]
        [Route("")]
        public IEnumerable<WarehouseCategory> Get(string recordtype, int whid, int whcatid)
        {
            if (recordtype == "warehouse")
            {
                logger.Info("start Category: ");
                List<Category> Category = new List<Category>();
                List<Warehouse> Warehouse = new List<Warehouse>();
                List<WarehouseCategory> WarehouseCategory = new List<WarehouseCategory>();
                List<WarehouseCategory> wareH = new List<WarehouseCategory>();
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
                    Category = context.AllCategory(compid).ToList();
                    Warehouse = context.AllWarehouse(compid).ToList();
                    wareH = context.AllWarehouseCategory(compid).ToList();
                   
                    var cat = Category;
                    var war = (from c in Warehouse where c.Warehouseid.Equals(whid) select c).SingleOrDefault();
                   
                    List<WarehouseCategory> wcat = (from c in wareH where c.Warehouseid == whid select c).ToList();
                  foreach(var i in wcat)
                    {
                        //var wcat = (from c in wareH where c.Warehouseid == whid && c.Categoryid == cat[i].Categoryid select c).SingleOrDefault();
                        //List<WarehouseCategory> wcat = (from c in wareH where c.Warehouseid == whid select c).ToList();
                       if(i.IsVisible)
                       {
                           WarehouseCategory wc = new WarehouseCategory();
                           wc.WhCategoryid = whcatid;
                           wc.Categoryid = i.Categoryid;
                           wc.CategoryName = i.CategoryName;
                           wc.Warehouseid = whid;
                           wc.Stateid = war.Stateid;
                           wc.State = war.StateName;
                           wc.Cityid = war.Cityid;
                           wc.City = war.CityName;
                           wc.IsVisible = true;
                           wc.SortOrder = i.SortOrder;
                 
                           WarehouseCategory.Add(wc);
                       }

                    }
                    logger.Info("End  Category: ");
                    return WarehouseCategory;
                }
                catch (Exception ex)
                {
                    logger.Error("Error in Category " + ex.Message);
                    logger.Info("End  Category: ");
                    return null;
                }
            }
            return null;
        }



        [ResponseType(typeof(Category))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Category add(Category item)
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
                context.AddCategory(item);
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

        [ResponseType(typeof(Category))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public Category Put(Category item)
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
                return context.PutCategory(item);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(Category))]
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
                context.DeleteCategory(id);
                logger.Info("End  delete Category: ");
            }
            catch (Exception ex)
            {
                logger.Error("Error in del Category " + ex.Message);
            }
        }
    }
}



