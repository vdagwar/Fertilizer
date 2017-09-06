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
using System.Runtime.Caching;
using System.Web;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/itemMaster")]
    public class ItemMasterController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
      
        [Route("")]
        public IEnumerable<ItemMaster> Get()
        {
            logger.Info("start Item Master: ");
            List<ItemMaster> ass = new List<ItemMaster>();
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
                ass = context.AllItemMaster().ToList();
                logger.Info("End  Item Master: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Item Master " + ex.Message);
                logger.Info("End  Item Master: ");
                return null;
            }
        }

        [Route("")]
        public customeritems Get(int warehouseid)
        {
            return  AngularJSAuthentication.API.Helper.getItemMaster(warehouseid);
        }
        [Route("")]
        public PaggingData Get(int list, int page, string warehouse)
        {
            logger.Info("start ItemMaster: ");
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
                var itemPagedList = context.AllItemMasterForPaging(list, page , warehouse);

                logger.Info("End ItemMaster: ");
                return itemPagedList;
            }
            catch (Exception ex)
            {
                logger.Error("Error in ItemMaster " + ex.Message);
                logger.Info("End  ItemMaster: ");
                return null;
            }
        }
        [Route("supplier")]
        public PaggingData GetSupplier(int list, int page, string SupplierCode)
        {
            AuthContext db = new AuthContext();
            logger.Info("start ItemMaster: ");
            try
            {
                PaggingData obj = new PaggingData();
                obj.total_count = db.itemMasters.Where(x => x.SUPPLIERCODES == SupplierCode).Count();
                obj.ordermaster = db.itemMasters.AsEnumerable().Where(x => x.SUPPLIERCODES == SupplierCode).Skip((page - 1) * list).Take(list).ToList();

                logger.Info("End ItemMaster: ");
                return obj;
            }
            catch (Exception ex)
            {
                logger.Error("Error in ItemMaster " + ex.Message);
                logger.Info("End  ItemMaster: ");
                return null;
            }
        }
        [HttpGet]
        [Route("Search")]
        public IEnumerable<ItemMaster> search(string key, string SupplierCode)
        {
            logger.Info("start Item Master: ");
            List<ItemMaster> ass = new List<ItemMaster>();
            try
            {
                AuthContext db = new AuthContext();
                ass = db.itemMasters.Where(t => t.itemname.Contains(key) && t.Deleted == false && t.SUPPLIERCODES == SupplierCode).ToList();
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Item Master " + ex.Message);
                logger.Info("End  Item Master: ");
                return null;
            }
        }





        [HttpGet]
        [Route("Searchinitemat")]
        public IEnumerable<ItemMaster> searchss(string key)
        {
            logger.Info("start Item Master: ");
            List<ItemMaster> ass = new List<ItemMaster>();
            try
            {
                AuthContext db = new AuthContext();
                ass = db.itemMasters.Where(t => t.itemname.Contains(key) && t.Deleted == false).ToList();
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Item Master " + ex.Message);
                logger.Info("End  Item Master: ");
                return null;
            }
        }




        [HttpGet]
        [Route("Searchinitem")]
        public IEnumerable<ItemMaster> searchs(string key)
        {
            logger.Info("start Item Master: ");
            List<ItemMaster> ass = new List<ItemMaster>();
          
            List<ItemMaster> result = new List<ItemMaster>();
            try
            {
                AuthContext db = new AuthContext();
                ass = db.itemMasters.Where(t => t.itemname.Contains(key) && t.Deleted == false && t.active == true).ToList();

                List<string> PurchaseSku = new List<string>();

                foreach (var item in ass)
                {
                    var items = ass.Where(t => t.PurchaseSku == item.PurchaseSku).FirstOrDefault();

                    if (items != null && !PurchaseSku.Any(x => x == items.PurchaseSku))
                    {
                        result.Add(items);
                        PurchaseSku.Add(items.PurchaseSku);
                    }
                }



                return result;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Item Master " + ex.Message);
                logger.Info("End  Item Master: ");
                return null;
            }
        }


        [HttpGet]
        [Route("freeItem")]
        public IEnumerable<ItemMaster> freeItem()
        {
            logger.Info("start Item Master: ");
            AuthContext db = new AuthContext();
            List<ItemMaster> ass = new List<ItemMaster>();
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
                ass = db.itemMasters.Where(f=>f.Deleted == false && f.free == true).ToList();
                logger.Info("End  Item Master: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Item Master " + ex.Message);
                logger.Info("End  Item Master: ");
                return null;
            }
        }
        [Authorize]
        [ResponseType(typeof(ItemMaster))]
        [Route("")]
        public IEnumerable<ItemMaster> Get(string cityid, string categoryid, string subcategoryid, string subsubcategoryid)
        {
            logger.Info("start ItemMaster: ");
            List<ItemMaster> ass = new List<ItemMaster>();
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
                ass = context.filteredItemMaster(cityid, categoryid, subcategoryid, subsubcategoryid).ToList();
                logger.Info("End ItemMaster: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in ItemMaster " + ex.Message);
                logger.Info("End  ItemMaster: ");
                return null;
            }
        }

        [Route("")]
        public List<ItemMaster> Get(string type, int id ,int Wid)
        {
            List<ItemMaster> item = new List<ItemMaster>();
            AuthContext db = new AuthContext();
            if (type == "Supplier")
            {
                try
                {
                    List<ItemMaster> it = db.itemMasters.Where(c=> c.SupplierId == id && c.warehouse_id == Wid && c.active == true).ToList();

                    List<string> PurchaseSku = new List<string>();

                    foreach (ItemMaster i in it)
                    {
                        var data = item.Where(c => c.PurchaseSku == i.PurchaseSku).FirstOrDefault();

                        if (data == null && !PurchaseSku.Any(x => x == i.PurchaseSku)) {
                            item.Add(i);
                            PurchaseSku.Add(i.PurchaseSku);
                        }
                    }
                }
                catch (Exception ex)
                {
                    item = null;
                }
            }
            return item;
        }
        [Route("")]
        public ItemMaster getitembyid(string id)
        {
            int idd = Convert.ToInt32(id);
            ItemMaster mk = new ItemMaster();
            mk = context.itembyid(idd).FirstOrDefault();
            return mk;
        }

        [Route("")]
        public List<ItemMaster> getitembyitemname(string itemname, string x)
        {
            List<ItemMaster> itemList = new List<ItemMaster>();
            var mk = context.itembystring(itemname).ToList();
            if (mk.Count != 0)
            {
                foreach (var itmm in mk)
                {
                    itmm.itemname = itmm.SellingUnitName;
                    itemList.Add(itmm);
                }
            }
            else
            {
                itemList = null;
            }
            return itemList;
        }

        [Route("")]
        public List<ItemMaster> post(string type, itemselected id)
        {
            List<ItemMaster> items = new List<ItemMaster>();
            if (type == "ids")
            {
                try
                {
                    foreach (var i in id.ItemId)
                    {
                        ItemMaster mk = new ItemMaster();
                        int idd = Convert.ToInt32(i);
                        mk = context.itembyid(idd).FirstOrDefault();
                        items.Add(mk);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return items;
        }

        [Authorize]
        [ResponseType(typeof(ItemMaster))]
        [Route("")]
        [AcceptVerbs("POST")]
        //[Route("api/ItemMaster")]
        public ItemMaster add(ItemMaster item)
        {
            logger.Info("start addItem Master: ");
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
                context.AddItemMaster(item);
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                AngularJSAuthentication.API.Helper.refreshItemMaster(item.warehouse_id, item.Categoryid);
                logger.Info("End  addItem Master: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add Item Master " + ex.Message);
                return null;
            }
        }      

        [Authorize]
        [ResponseType(typeof(ItemMaster))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public ItemMaster Put(ItemMaster item)
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
                ItemMaster itm = context.PutItemMaster(item);
                AngularJSAuthentication.API.Helper.refreshItemMaster(itm.warehouse_id, itm.Categoryid);
                return itm;
            }
            catch
            {
                return null;
            }
        }

        [ResponseType(typeof(ItemMaster))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start delete Item Master: ");
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
                context.DeleteItemMaster(id);
                logger.Info("End  delete Item Master: ");
            }
            catch (Exception ex)
            {
                logger.Error("Error in delete Item Master " + ex.Message);
            }
        }

        [Route("export")]
        [HttpGet]
        public dynamic export(int warehouseid)
        {
            AuthContext db = new AuthContext();
            try
            {
                var list = (from i in db.itemMasters
                            where i.Deleted == false /*&&  i.active == true*/ && i.warehouse_id == warehouseid
                            join j in db.Categorys on i.Categoryid equals j.Categoryid
                            join k in db.SubsubCategorys on i.SubsubCategoryid equals k.SubsubCategoryid
                            select new
                            {
                                CityName = i.CityName,
                                Cityid = i.Cityid,
                                CategoryName = i.CategoryName,
                                CategoryCode = j.Code,
                                SubcategoryName = i.SubcategoryName,
                                SubsubcategoryName = i.SubsubcategoryName,
                                BrandCode = k.Code,
                                itemname = i.itemname,
                                itemcode = i.itemcode,
                                Number = i.Number,
                                SellingSku = i.SellingSku,
                                price = i.price,
                                PurchasePrice = i.PurchasePrice,
                                UnitPrice = i.UnitPrice,
                                MinOrderQty = i.MinOrderQty,
                                SellingUnitName = i.SellingUnitName,
                                PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                StoringItemName = i.StoringItemName,
                                PurchaseSku = i.PurchaseSku,
                                PurchaseUnitName = i.PurchaseUnitName,
                                SupplierName = i.SupplierName,
                                SUPPLIERCODES = i.SUPPLIERCODES,
                                BaseCategoryName = i.BaseCategoryName,
                                TGrpName = i.TGrpName,
                                TotalTaxPercentage = i.TotalTaxPercentage,
                                WarehouseName = i.WarehouseName,
                                HindiName = i.HindiName,
                                SizePerUnit = i.SizePerUnit,
                                Barcode = i.Barcode,
                                Active = i.active,
                                Deleted = i.Deleted,
                                Margin = i.Margin,
                                PromoPoint = i.promoPoint,
                                HSNCode = i.HSNCode
                            }).ToList();
                logger.Info("excel exported: ");
                return list;
            }
            catch (Exception ex)
            {
                logger.Error("Gote exception:: " + ex.Message);
                return null;
            }
        }

        [Route("oldprice")]
        [HttpGet]
        public dynamic oldprice(int? ItemId)
        {
            AuthContext odd = new AuthContext();
            try
            {
                var data = odd.ItemMasterHistoryDb.Where(x => x.Deleted == false && x.ItemId == ItemId).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}