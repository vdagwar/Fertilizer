using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NLog;
using AngularJSAuthentication.API.Controllers;
using System.Runtime.Caching;
using AngularJSAuthentication.Model;
using GenricEcommers.Models;
using AngularJSAuthentication.Model.NotMapped;
using AngularJSAuthentication.API.ControllerV1;
using static AngularJSAuthentication.API.Controllers.pointConversionController;

namespace AngularJSAuthentication.API
{
    public class AuthContext : IdentityDbContext<IdentityUser>, iAuthContext
    {
        //nlogger
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        public AuthContext() : base("AuthContext")
        {

        }
   
        public DbSet<CheckCurrency> CheckCurrencyDB { get; set; }
        public DbSet<CurrencyHistory> CurrencyHistoryDB { get; set; }
        public DbSet<CurrencyStock> CurrencyStockDB { get; set; }
        public DbSet<DBoyCurrency> DBoyCurrencyDB { get; set; }
        public DbSet<CurrencyBankSettle> CurrencyBankSettleDB { get; set; }

        public DbSet<DamageStock> DamageStockDB { get; set; }
      public DbSet<DamageOrderMaster> DamageOrderMasterDB { get; set; }
        public DbSet<DamageOrderDetails> DamageOrderDetailsDB { get; set; }
        
        public DbSet<Area> AreaDb { get; set; }
      
        public DbSet<ReqService> ReqServiceDB { get; set; }
        
        public DbSet<IR_Confirm> IR_ConfirmDb { get; set; }
        public DbSet<UnitEconomic> UnitEconomicDb { get; set; }
        public DbSet<InvoiceReceive> InvoiceReceiveDb { get; set; } 
        public DbSet<InvoiceImage> InvoiceImageDb { get; set; }
        public DbSet<FreeItem> FreeItemDb { get; set; }
        public DbSet<SKFreeItem> SKFreeItemDb { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturnDb { get; set; }
        public DbSet<Target> TargetDb { get; set; }
        public DbSet<appVersion> appVersionDb { get; set; }
        public DbSet<AvgInventory> AvgInventoryDb { get; set; }
        public DbSet<ShortSetttle> ShortSetttleDb { get; set; }
        public DbSet<ItemMasterHistory> ItemMasterHistoryDb { get; set; }
        public DbSet<CurrentStockHistory> CurrentStockHistoryDb { get; set; }
        public DbSet<Offer> OfferDb { get; set; }
        public DbSet<Wallet> WalletDb { get; set; }        
        public DbSet<RewardPoint> RewardPointDb { get; set; }
        public DbSet<RPConversion> RPConversionDb { get; set; }
        public DbSet<MilestonePoint> MilestonePointDb { get; set; }
        public DbSet<RetailerShare> RetailerShareDb { get; set; }        
        public DbSet<CashConversion> CashConversionDb { get; set; }
        public DbSet<promoPurConv> promoPurConvDb { get; set; }
        public DbSet<supplierPoint> supplierPointDb { get; set; }
        public DbSet<RewardItems> RewardItemsDb { get; set; }
        public DbSet<DreamOrder> DreamOrderDb { get; set; }
        public DbSet<DreamItem> DreamItemDb { get; set; }
        public DbSet<ActionTask> ActionTaskDb { get; set; }
        public DbSet<CustomerIssue> CustomerIssuedb { get; set; }
        public DbSet<SalesPersonBeat> SalesPersonBeatDb { get; set; }
        public DbSet<DeliveryCharge> DeliveryChargeDb { get; set; }
        public DbSet<DailyEssential> DailyEssentialDb { get; set; }
        public DbSet<GpsCoordinate> GpsCoordinateDb { get; set; }
        public DbSet<DailyItemEdit> DailyItemCancelDb { get; set; }
        public DbSet<OrderNotes> OrderNotesDb { get; set; }
        public DbSet<ItemMaster> itemMasters { get; set; }
        public DbSet<BaseCategory> BaseCategoryDb { get; set; }
        public DbSet<DeliveryIssuance> DeliveryIssuanceDb { get; set; }
        public DbSet<IssuanceDetails> IssuanceDetailsDb { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<SubCategory> SubCategorys { get; set; }
        public DbSet<SubsubCategory> SubsubCategorys { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseCategory> DbWarehouseCategory { get; set; }
        public DbSet<WarehouseSubCategory> DbWarehouseSubCategory { get; set; }
        public DbSet<WarehouseSubsubCategory> DbWarehousesubsubcats { get; set; }
        public DbSet<WarehouseSupplier> DbWarehouseSupplier { get; set; }
        public DbSet<OrderMaster> DbOrderMaster { get; set; }
        public DbSet<OrderDetails> DbOrderDetails { get; set; }
      
        public DbSet<FinalOrderDispatchedMaster> FinalOrderDispatchedMasterDb { get; set;}
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<TravelRequest> TravelRequests { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Role> UserRole { get; set; }
        public DbSet<Assets> Assetss { get; set; }
        public DbSet<AssetsCategory> AssetsCategorys { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<AllInvoice> invoices { get; set; }
        public DbSet<InvoiceRow> InvoiceRows { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<CustomerCategory> CustomerCategorys { get; set; }
        public DbSet<ItemBrand> DbItemBrand { get; set; }
        public DbSet<FinancialYear> DbFinacialYear { get; set; }
        public DbSet<CustomerRegistration> CustomerRegistrations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierCategory> SupplierCategory { get; set; }
        public DbSet<TaxGroupDetails> DbTaxGroupDetails { get; set; }
        public DbSet<TaxGroup> DbTaxGroup { get; set; }
        public DbSet<TaxMaster> DbTaxMaster { get; set; }
        public DbSet<DemandMaster> dbDemandMasters { get; set; }
        public DbSet<DemandDetails> dbDemandDetails { get; set; }
        public DbSet<ItemPramotions> itempramotions { get; set; }
        public DbSet<BillPramotion> BillPramotions { get; set; }
        public DbSet<PurchaseOrder> DbPurchaseOrder { get; set; }
        public DbSet<PurchaseOrderMaster> DPurchaseOrderMaster { get; set; }
        public DbSet<PurchaseOrderDetail> DPurchaseOrderDeatil { get; set; }
        public DbSet<CurrentStock> DbCurrentStock { get; set; }
        public DbSet<Message> DbMessage { get; set; }
        public DbSet<Favorites> Favoritess { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<PurchaseOrderMasterRecived> PurchaseOrderMasterRecivedes { get; set; }
        public DbSet<PurchaseOrderDetailRecived> PurchaseOrderRecivedDetails { get; set; }
        public DbSet<OrderDispatchedDetails> OrderDispatchedDetailss { get; set; }
        public DbSet<OrderDispatchedMaster> OrderDispatchedMasters { get; set; }
        public DbSet<Slider> SliderDb { get; set; }
        public DbSet<ReturnOrderDispatchedDetails> ReturnOrderDispatchedDetailsDb { get; set; }
        public DbSet<FinalOrderDispatchedDetails> FinalOrderDispatchedDetailsDb { get; set; }
        public DbSet<Notification> NotificationDb { get; set; }
        public DbSet<NotificationByDeviceId> NotificationByDeviceIdDb { get; set; }
        public DbSet<DeviceNotification> DeviceNotificationDb { get; set; }
        public DbSet<ApplicationIdNotification> ApplicationIdNotificationDb { get; set; }
        public DbSet<GroupNotification> GroupNotificationDb { get; set; }
        public DbSet<Coupon> CouponDb { get; set; }
        public DbSet<Division> DivisionDb { get; set; }
        public DbSet<News> NewsDb { get; set; }
        public DbSet<Vehicle> VehicleDb { get; set; }
        public DbSet<RedispatchWarehouse> RedispatchWarehouseDb { get; set; }
        public DbSet<EditPriceHistory> EditPriceHistoryDb { get; set; }

        //======new=================================================================================================================================//
        #region Damage order placed
        
        public DamageOrderMaster AddDamageOrder(DamageOrder sc)
        {
            double data = 0;
            //List<IDetail> cart = new List<IDetail>();
           // cart = sc.itemDetails.Where(a => a.qty > 0).Select(a => a).ToList<IDetail>();
            double finaltotal = 0;
            double finalTaxAmount = 0;
            double finalGrossAmount = 0;
            double finalTotalTaxAmount = 0;

            Customer cust = Customers.Where(c => c.Active == true && c.CustomerId == sc.CustomerId).SingleOrDefault();
           
            Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == cust.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
            if (cust != null)
            {
                cust.ShippingAddress = sc.ShippingAddress;
                cust.CompanyId = 2;
                cust.ordercount = cust.ordercount + 1;
                cust.MonthlyTurnOver = cust.MonthlyTurnOver + sc.TotalAmount;
                Customers.Attach(cust);
                this.Entry(cust).State = EntityState.Modified;
                this.SaveChanges();
            }
            DamageOrderMaster objOrderMaster = new DamageOrderMaster();
            try
            {
                DamageOrderMaster order = new DamageOrderMaster();

                People p = Peoples.Where(x => x.PeopleID == cust.ExecutiveId && x.Deleted == false).SingleOrDefault();
                objOrderMaster.CompanyId = 1;
                objOrderMaster.TotalAmount = sc.TotalAmount;

                objOrderMaster.CustomerCategoryId = 2;
                objOrderMaster.Status = "Pending";
                objOrderMaster.CustomerName =sc.CustomerName;
                objOrderMaster.Customerphonenum = cust.Mobile;
                objOrderMaster.ShopName = cust.ShopName;
                objOrderMaster.Skcode = cust.Skcode;
                objOrderMaster.Tin_No = cust.RefNo;
                objOrderMaster.CustomerType = cust.CustomerType;
                objOrderMaster.Warehouseid = warehouse.Warehouseid;
                objOrderMaster.WarehouseName = warehouse.WarehouseName;
                objOrderMaster.CustomerId = cust.CustomerId;
                objOrderMaster.CityId = warehouse.Cityid;              
                objOrderMaster.ClusterId = Convert.ToInt16(cust.ClusterId);
                var clstr = Clusters.Where(x => x.ClusterId == cust.ClusterId).SingleOrDefault();
                if (clstr != null)
                {
                    objOrderMaster.ClusterName = clstr.ClusterName;
                }
                
             
                objOrderMaster.BillingAddress = sc.ShippingAddress;
                objOrderMaster.ShippingAddress = sc.ShippingAddress;

                objOrderMaster.active = true;
                objOrderMaster.CreatedDate = indianTime;
                if (indianTime.Hour > 16)
                {
                    objOrderMaster.Deliverydate = indianTime.AddDays(2);
                }
                else
                {
                    objOrderMaster.Deliverydate = indianTime.AddDays(1);
                }
                objOrderMaster.UpdatedDate = indianTime;
                objOrderMaster.Deleted = false;

                List<DamageOrderDetails> collection = new List<DamageOrderDetails>();
                objOrderMaster.DamageorderDetails = collection;

                //foreach (var i in cart.Select(x => x))
                {
                    if (sc.qty != 0 && sc.qty > 0)
                    {
                        ItemMaster items = itemMasters.Where(x => x.ItemId == sc.ItemId).Select(x => x).FirstOrDefault();
                        DamageStock dt = DamageStockDB.Where(c => c.Deleted == false && c.DamageStockId == sc.DamageStockId).SingleOrDefault();
                        if (dt != null)
                        {
                            dt.DamageInventory = dt.DamageInventory-sc.qty;
                            dt.UpdatedDate = indianTime;
                            DamageStockDB.Attach(dt);
                            this.Entry(dt).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                        DamageOrderDetails od = new DamageOrderDetails();
                        od.CustomerId = cust.CustomerId;
                        od.CustomerName = cust.Name;
                        od.City = warehouse.CityName;
                        od.CityId = warehouse.Cityid;
                        od.Mobile = cust.Mobile;
                        od.OrderDate = indianTime;
                        od.Status = "Pending";
                        od.CompanyId = Convert.ToInt32(cust.CompanyId);
                        od.Warehouseid = warehouse.Warehouseid;
                        od.WarehouseName = warehouse.WarehouseName;
                        od.NetPurchasePrice = items.NetPurchasePrice + ((items.NetPurchasePrice * items.TotalTaxPercentage) / 100);
                        od.ItemId = items.ItemId;
                        od.Itempic = items.LogoUrl;
                        od.itemname = items.SellingUnitName;
                        od.itemcode = items.itemcode;
                        od.itemNumber = items.Number;
                        od.Barcode = items.itemcode;
                        od.UnitPrice = sc.UnitPrice;
                        od.DefaultUnitPrice = dt.UnitPrice;
                        od.price = items.price;
                        od.MinOrderQty = items.MinOrderQty;
                        int MOQ = items.MinOrderQty;

                        od.MinOrderQtyPrice = MOQ * items.UnitPrice;

                        od.qty = Convert.ToInt32(sc.qty);

                        int qty = 0;
                        qty = Convert.ToInt32(od.qty);

                        od.SizePerUnit = items.SizePerUnit;
                        od.TaxPercentage = items.TotalTaxPercentage;

                        //........CALCULATION FOR NEW SHOPKIRANA.............................
                        od.Noqty = qty; // for total qty (no of items)

                        // STEP 1  (UNIT PRICE * QTY)     - SHOW PROPERTY                  
                        od.TotalAmt = System.Math.Round(od.UnitPrice * qty, 2);

                        // STEP 2 (AMOUT WITHOU TEX AND WITHOUT DISCOUNT ) - SHOW PROPERTY
                        od.AmtWithoutTaxDisc = ((100 * od.UnitPrice * qty) / (1 + od.TaxPercentage / 100)) / 100;

                        // STEP 3 (AMOUNT WITHOUT TAX AFTER DISCOUNT) - UNSHOW PROPERTY
                        od.AmtWithoutAfterTaxDisc = (100 * od.AmtWithoutTaxDisc) / (100 + items.PramotionalDiscount);

                        //STEP 4 (TAX AMOUNT) - UNSHOW PROPERTY
                        od.TaxAmmount = (od.AmtWithoutAfterTaxDisc * od.TaxPercentage) / 100;

                        //STEP 5(TOTAL TAX AMOUNT) - UNSHOW PROPERTY
                        od.TotalAmountAfterTaxDisc = od.AmtWithoutAfterTaxDisc + od.TaxAmmount;


                        //...............Calculate Discount.............................
                        od.DiscountPercentage = items.PramotionalDiscount;
                        od.DiscountAmmount = (od.NetAmmount * items.PramotionalDiscount) / 100;
                        double DiscountAmmount = od.DiscountAmmount;
                        double NetAmtAfterDis = (od.NetAmmount - DiscountAmmount);
                        od.NetAmtAfterDis = (od.NetAmmount - DiscountAmmount);

                        double TaxAmmount = od.TaxAmmount;

                        od.Purchaseprice = items.price;
                        //od.VATTax = items.VATTax;
                        od.CreatedDate = indianTime;
                        od.UpdatedDate = indianTime;
                        od.Deleted = false;
                        objOrderMaster.DamageorderDetails.Add(od);
                        finaltotal = finaltotal + od.TotalAmt;
                        finalTaxAmount = finalTaxAmount + od.TaxAmmount;
                        finalGrossAmount = finalGrossAmount + od.TotalAmountAfterTaxDisc;
                        finalTotalTaxAmount = finalTotalTaxAmount + od.TotalAmountAfterTaxDisc;

                    }
                }

              
                objOrderMaster.TaxAmount = System.Math.Round(finalTaxAmount, 2);
             
                objOrderMaster.DiscountAmount = finalTotalTaxAmount - finaltotal;
                objOrderMaster.GrossAmount = Convert.ToInt32(finalGrossAmount);

                List<DamageOrderMaster> ord = DamageOrderMasterDB.ToList();
                if (ord.Count > 0)
                {
                    order = ord.Last();
                }
                else
                {
                    order.DamageOrderId = 0;
                }
                objOrderMaster.invoice_no = "Od_" + Convert.ToString(order.DamageOrderId + 1);

                DamageOrderMasterDB.Add(objOrderMaster);
                int id = this.SaveChanges();

                try
                {

                    string invoice = objOrderMaster.invoice_no.ToString();
                   
                }
                catch (Exception ex)
                {
                    logger.Error("Error in Get single GetcusomerWallets " + ex.Message);
                }
                return objOrderMaster;

            }//end order master catch
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }


        #endregion

        #region for damage stock transfer from current stock
        public DamageStock Adddemand(DamageStock obj)
        {
            try
            {

                
                if (obj != null)
                {
                    CurrentStock itemm = DbCurrentStock.Where(x => x.ItemNumber == obj.ItemNumber && x.Warehouseid == obj.Warehouseid).SingleOrDefault();

                    if (itemm.CurrentInventory >= obj.DamageInventory)
                    {
                        DamageStock dst = DamageStockDB.Where(x => x.ItemNumber == obj.ItemNumber && x.Warehouseid == obj.Warehouseid).SingleOrDefault();
                        if (dst == null)
                        {
                           DamageStock objst = new DamageStock();
                            objst.Warehouseid = obj.Warehouseid;
                            objst.WarehouseName = obj.WarehouseName;
                            objst.ItemId = obj.ItemId;
                            objst.ItemNumber = obj.ItemNumber;
                            objst.ItemName = obj.ItemName;
                            objst.DamageInventory = obj.DamageInventory;
                            double netUnitPrice = Math.Round(obj.UnitPrice, 2);
                            objst.UnitPrice = netUnitPrice;
                            objst.ReasonToTransfer = obj.ReasonToTransfer;
                            objst.CreatedDate = indianTime;
                            DamageStockDB.Add(objst);
                            int id = this.SaveChanges();
                            if (id != 0)
                            {

                                CurrentStockHistory Oss = new CurrentStockHistory();
                                if (itemm != null)
                                {
                                    Oss.StockId = itemm.StockId;
                                    Oss.ItemNumber = itemm.ItemNumber;
                                    Oss.ItemName = itemm.ItemName;
                                    Oss.CurrentInventory = itemm.CurrentInventory;
                                    Oss.DamageInventoryOut = Convert.ToInt32(obj.DamageInventory);
                                    Oss.TotalInventory = Convert.ToInt32(itemm.CurrentInventory - obj.DamageInventory);
                                    Oss.WarehouseName = itemm.WarehouseName;
                                    Oss.CreationDate = indianTime;
                                   CurrentStockHistoryDb.Add(Oss);
                                    int idd = this.SaveChanges();
                                }
                                itemm.CurrentInventory = itemm.CurrentInventory - obj.DamageInventory;
                                itemm.UpdatedDate = DateTime.Now;
                                DbCurrentStock.Attach(itemm);
                                this.Entry(itemm).State = EntityState.Modified;
                                this.SaveChanges();
                            }
                        }
                        else {

                            dst.DamageInventory = dst.DamageInventory + obj.DamageInventory;
                            double netUnitPrice = Math.Round(obj.UnitPrice, 2);
                            dst.UnitPrice = netUnitPrice;
                            dst.ReasonToTransfer = obj.ReasonToTransfer;
                            dst.UpdatedDate = DateTime.Now;
                            DamageStockDB.Attach(dst);
                            this.Entry(dst).State = EntityState.Modified;
                            this.SaveChanges();

                            if (dst != null) {
                                itemm.CurrentInventory = itemm.CurrentInventory - obj.DamageInventory;
                                itemm.UpdatedDate = DateTime.Now;
                                DbCurrentStock.Attach(itemm);
                                this.Entry(itemm).State = EntityState.Modified;
                                this.SaveChanges();

                            }
                            
                        }
                    }
                }
                else
                {
                    return null;

                }
                return obj;
            }

            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return obj;
            }
        }

        #endregion

        #region for damage order 
        public PaggingDataitem AddDamageStock(int pid, int warehouseid)
        {
            List<ItemMaster> newdata = new List<ItemMaster>();

            if (itemMasters.AsEnumerable().Count() > 0)
            {

                    newdata = itemMasters.Where(x => x.Deleted == false && x.ItemId == pid && x.warehouse_id == warehouseid).OrderByDescending(x => x.ItemId).ToList();

            }
            else
            {
                var orders = itemMasters.OrderByDescending(x => x.Deleted == false && x.ItemId == pid && x.warehouse_id == warehouseid).AsEnumerable();
            }
            PaggingDataitem obj = new PaggingDataitem();
            obj.total_count = itemMasters.Count();
            obj.otmaster = newdata;
            return obj;
        }

        #endregion



        #region for multi select in ordersettle data

        public string DBIsettle(List<FinalOrderDispatchedMaster> obj)
        {
            try
            {
                foreach (var final in obj)
                {
                    // for ordermaster master status change
                    OrderMaster om = DbOrderMaster.Where(x => x.OrderId == final.OrderId && x.Deleted == false).FirstOrDefault();
                    om.Status = "sattled";
                    om.DiscountAmount = final.DiscountAmount;
                    om.ShortAmount = final.ShortAmount;
                    om.ShortAmount = final.ShortAmount;
                    om.UpdatedDate = indianTime;
                    DbOrderMaster.Attach(om);
                    Entry(om).State = EntityState.Modified;
                    SaveChanges();

                    // order dispatched master status change
                    OrderDispatchedMaster ox = OrderDispatchedMasters.Where(x => x.OrderId == final.OrderId && x.Deleted == false).FirstOrDefault();
                    ox.Status = "sattled";
                    ox.DiscountAmount = final.DiscountAmount;
                    ox.UpdatedDate = indianTime;
                    OrderDispatchedMasters.Attach(ox);
                    Entry(ox).State = EntityState.Modified;
                    SaveChanges();
                    try
                    {
                        if (final.ShortAmount > 0)
                        {
                            ShortSetttle ob = new ShortSetttle();
                            ob.Status = "sattled";
                            ob.OrderId = final.OrderId;
                            ob.CustomerId = final.CustomerId;
                            ob.CustomerName = final.CustomerName;
                            ob.DboyName = final.DboyName;
                            ob.DboyMobileNo = final.DboyMobileNo;
                            ob.Warehouseid = final.Warehouseid;
                            ob.WarehouseName = final.WarehouseName;
                            ob.ShortAmount = final.ShortAmount;
                             ob.ShortReason = final.ShortReason;
                            ob.DiscountAmount = final.DiscountAmount;
                            ob.GrossAmount = final.GrossAmount;
                            ob.CreatedDate = indianTime;
                            ShortSetttleDb.Add(ob);
                            int id = this.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    if (final != null)
                    {

                        final.UpdatedDate = indianTime;
                        final.Status = "sattled";
                       FinalOrderDispatchedMasterDb.Attach(final);
                        Entry(final).State = EntityState.Added;
                        SaveChanges();
                    }
                    else
                    {
                        logger.Error("This final is not Found int add ");
                        return null;
                    }
                    
                }
               
            }

            catch (Exception ex)
            {
                logger.Error("Error in final Order Dispatch master " + ex.Message);
            }
            return null;

        }

        #endregion


        public IList<EditPriceHistory> filteredEditPriceHistory(DateTime? start, DateTime? end, string cityid, string categoryid, string subcategoryid, string subsubcategoryid)
        {
            int CityID = Convert.ToInt32(cityid.Trim());
            int CategoryID = Convert.ToInt32(categoryid.Trim());
            int SubCategoryID = Convert.ToInt32(subcategoryid.Trim());
            int SubSubCategoryID = Convert.ToInt32(subsubcategoryid.Trim());

            if (cityid == "undefined" || cityid == null || cityid == "0")
            {
                if (categoryid == "undefined" || categoryid == null || categoryid == "0")
                {
                    var filteredlist = (from od in EditPriceHistoryDb where od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false && od.CreatedDate > start && od.CreatedDate < end select od).ToList();
                    return filteredlist;
                }
                else if (start != null)
                {
                    var filteredlist = (from od in EditPriceHistoryDb where od.Categoryid == CategoryID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false && od.CreatedDate > start && od.CreatedDate < end select od).ToList();
                    return filteredlist;
                }

                else
                {
                    var filteredlist = (from od in EditPriceHistoryDb where od.Categoryid == CategoryID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false && od.CreatedDate > start && od.CreatedDate < end select od).ToList();
                    return filteredlist;
                }
            }
            else
            {
                if (categoryid == "undefined" || categoryid == null || categoryid == "0")
                {
                    var filteredlist = (from od in EditPriceHistoryDb where od.Cityid == CityID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false && od.CreatedDate > start && od.CreatedDate < end select od).ToList();
                    return filteredlist;
                }
                else if (start != null)
                {
                    var filteredlist = (from od in EditPriceHistoryDb where od.Categoryid == CategoryID && od.Cityid == CityID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false && od.CreatedDate > start && od.CreatedDate < end select od).ToList();
                    return filteredlist;
                }

                else
                {
                    var filteredlist = (from od in EditPriceHistoryDb where od.Categoryid == CategoryID && od.Cityid == CityID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false && od.CreatedDate > start && od.CreatedDate < end select od).ToList();
                    return filteredlist;
                }
            }
        }
        public List<Area> AddBulkArea(List<Area> CustCollection)
        {
            logger.Info("start addbulk customer");
            try
            {
                foreach (var o in CustCollection)
                {
                    List<Area> cust = AreaDb.Where(c => c.AreaName.Equals(o.AreaName) && c.Deleted == false).ToList();

                    Area objitemMaster = new Area();
                    if (cust.Count == 0)
                    {
                        o.Active = true;
                        o.CreatedDate = indianTime;
                        o.UpdatedDate = indianTime;
                        AreaDb.Add(o);
                        int id = this.SaveChanges();

                    }
                    else
                    {
                        logger.Info("Mobile number already exists");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Info("error in adding Sales Executive collection");
            }
            return null;
        }

        public Area AddArea(Area area)
        {

            var ar = AreaDb.Where(x => x.AreaName == area.AreaName).FirstOrDefault();
            var city = Cities.Where(x => x.Cityid == area.CityId).FirstOrDefault();
        
            if (ar == null)
            {
                area.CityName = city.CityName;
                area.CityId = city.Cityid;
                area.CreatedDate = indianTime;
                area.UpdatedDate = indianTime;
                AreaDb.Add(area);
                int id = this.SaveChanges();
                return area;
            }
            return null;
        }

        public Area Putarea(Area area)
        {
            var ar = AreaDb.Where(x => x.areaId == area.areaId).FirstOrDefault();
            var city = Cities.Where(x => x.Cityid == area.CityId).FirstOrDefault();
         
            if (ar != null)
            {
                ar.AreaName = area.AreaName;
                ar.AreaCode = area.AreaCode;
                ar.CityId = city.Cityid;
                ar.CityName = city.CityName;
                ar.UpdatedDate = indianTime;
                AreaDb.Attach(ar);
                this.Entry(ar).State = EntityState.Modified;
                int id = this.SaveChanges();
                return area;
            }
            return null;
        }


        public bool DeleteArea(int id)
        {
            try
            {
                Area ara = AreaDb.Where(x => x.areaId == id).FirstOrDefault();
                ara.Deleted = true;
                ara.Active = false;
                AreaDb.Attach(ara);
                this.Entry(ara).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public PaggingData AllShortagePaging(int list, int page, string DBoyNo, DateTime? datefrom, DateTime? dateto)
        {
            List<ShortSetttle> newdata = new List<ShortSetttle>();

            if (DBoyNo == "all" && datefrom == null)
            {
                newdata = ShortSetttleDb.Where(x => x.Deleted == false && x.ShortAmount != 0).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
            }
            else if (DBoyNo == "all" && datefrom != null && dateto != null)
            {
                newdata = ShortSetttleDb.Where(x => x.Deleted == false && x.ShortAmount != 0 && (x.CreatedDate > datefrom && x.CreatedDate < dateto)).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
            }
            else if (DBoyNo != "all" && datefrom != null && dateto != null)
            {
                newdata = ShortSetttleDb.Where(x => x.CreatedDate > datefrom && x.CreatedDate < dateto && x.DboyMobileNo == DBoyNo && x.Deleted == false && x.ShortAmount != 0).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
            }
            else if (DBoyNo != "all" && datefrom == null && dateto == null)
            {
                newdata = ShortSetttleDb.Where(x => x.DboyMobileNo == DBoyNo && x.Deleted == false && x.ShortAmount != 0).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
            }

            PaggingData obj = new PaggingData();
            obj.total_count = ShortSetttleDb.Count();
            obj.ordermaster = newdata;
            return obj;
        }

        #region Offers

        public Offer AddOffer(Offer Offer)
        {
            try
            {
                Offer objoff = new Offer();
                objoff.OfferCategory = Offer.OfferCategory;
                objoff.OfferType = Offer.OfferType;
                objoff.OfferName = Offer.OfferName;
                objoff.Description = Offer.Description;
                objoff.Amount = Offer.Amount;
                objoff.MinAmount = Offer.MinAmount;
                objoff.MaxAmount = Offer.MaxAmount;
                objoff.StartTime = Offer.StartTime;
                objoff.EndTime = Offer.EndTime;
                objoff.StartDate = Offer.StartDate;
                objoff.EndDate = Offer.EndDate;
                objoff.CreatedDate = indianTime;
                OfferDb.Add(objoff);
                int id = this.SaveChanges();
                return Offer;
            }
            catch (Exception ex)
            {

                return Offer;
            }
        }

        public IEnumerable<Offer> GetAllOffer()
        {

            if (OfferDb.AsEnumerable().Count() > 0)
            {
                List<Offer> Offers = new List<Offer>();
                Offers = OfferDb.ToList();
                Offers = OfferDb.Where(c => c.IsDeleted == false).ToList();
                return Offers.AsEnumerable();
            }
            else
            {
                List<Offer> Offers = new List<Offer>();
                return Offers.AsEnumerable();
            }
        }


        public Offer GetOfferbyId(int id)
        {

            Offer offer = OfferDb.Where(c => c.OfferId == id && c.IsDeleted == false).SingleOrDefault();
            if (offer != null)
            {
                return offer;
            }
            else
            {
                offer = new Offer();
            }
            return offer;
        }

        public Offer PutOffer(Offer obj)
        {

            logger.Info("put Offer: ");
            try
            {
                Offer offer = OfferDb.Where(x => x.OfferId == obj.OfferId).FirstOrDefault();
                if (offer != null)
                {
                    offer.OfferCategory = obj.OfferCategory;
                    offer.OfferType = obj.OfferType;
                    offer.OfferName = obj.OfferName;
                    offer.Description = obj.Description;
                    offer.Description = obj.Description;
                    offer.Amount = obj.Amount;
                    offer.MinAmount = obj.MinAmount;
                    offer.MaxAmount = obj.MaxAmount;
                    offer.StartTime = obj.StartTime;
                    offer.EndTime = obj.EndTime;
                    offer.StartDate = obj.StartDate;
                    offer.EndDate = obj.EndDate;
                    offer.IsDeleted = false;
                    offer.UpdateDate = indianTime;
                    OfferDb.Attach(offer);
                    this.Entry(offer).State = EntityState.Modified;
                    this.SaveChanges();
                    return obj;
                }
                else
                {
                    logger.Error("This offer is not Found in put " + obj.Description);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Offer " + ex.Message);
            }
            return null;
        }

        public bool DeleteOffer(int id)
        {

            try
            {
                Offer offer = OfferDb.Where(x => x.OfferId == id).SingleOrDefault();
                offer.IsDeleted = true;
                offer.UpdateDate = indianTime;
                OfferDb.Attach(offer);
                this.Entry(offer).State = EntityState.Modified;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
        #region Wallet
        public Wallet GetWalletbyCustomerid(int CustomerId)//get orders to deliver
        {
            try
            {
                var wallet = WalletDb.Where(c => c.CustomerId == CustomerId).SingleOrDefault();
                if (wallet == null) {
                    Wallet w = new Wallet();
                    var cust = Customers.Where(c => c.CustomerId == CustomerId).SingleOrDefault();
                    if (cust != null)
                    {
                        w.ShopName = cust.ShopName;
                        w.Skcode = cust.Skcode;
                    }
                    w.CustomerId = CustomerId;
                    w.TotalAmount = 0;
                    w.CreatedDate = indianTime;
                    w.UpdatedDate = indianTime;
                    w.Deleted = false;
                    WalletDb.Add(w);
                    this.SaveChanges();
                    return w;
                }
                return wallet;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        public Wallet postWalletbyCustomerid(Wallet wallet)
        {
            try
            {
                var walt = WalletDb.Where(c => c.CustomerId == wallet.CustomerId).SingleOrDefault();
                if (walt != null)
                {                   
                    walt.CustomerId = wallet.CustomerId;
                    if (wallet.CreditAmount != 0)
                    {
                        walt.TotalAmount += wallet.CreditAmount;
                        walt.UpdatedDate = indianTime;
                    }
                    if (wallet.DebitAmount > 0)
                    {
                        walt.TotalAmount -= wallet.DebitAmount;
                        walt.TransactionDate = indianTime;
                    }
                    WalletDb.Attach(walt);
                    this.Entry(walt).State = EntityState.Modified;
                    this.SaveChanges();
                    return walt;
                }
                else
                {
                    if (wallet.CreditAmount > 0)
                    {
                        wallet.TotalAmount = wallet.CreditAmount;
                        wallet.UpdatedDate = indianTime;
                    }
                    wallet.CreatedDate = indianTime;
                    wallet.UpdatedDate = indianTime;
                    wallet.Deleted = false;
                    WalletDb.Add(wallet);
                    this.SaveChanges();
                    return wallet;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        public RewardPoint GetRewardbyCustomerid(int CustomerId)//get orders to deliver
        {
            try
            {
                var point = RewardPointDb.Where(c => c.CustomerId == CustomerId).SingleOrDefault();
                if (point == null)
                {
                    RewardPoint w = new RewardPoint();
                    var cust = Customers.Where(c => c.CustomerId == CustomerId).SingleOrDefault();
                    if (cust != null)
                    {
                        w.ShopName = cust.ShopName;
                        w.Skcode = cust.Skcode;
                    }
                    w.CustomerId = CustomerId;
                    w.TotalPoint = 0;
                    w.EarningPoint = 0;
                    w.MilestonePoint = 0;
                    w.UsedPoint = 0;
                    w.CreatedDate = indianTime;
                    w.UpdatedDate = indianTime;
                    w.Deleted = false;
                    RewardPointDb.Add(w);
                    this.SaveChanges();
                    return w; 
                }                
                return point;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        #endregion
        #region issuance
        public string deliveryIssuance(DeliveryIssuance obj) {
            try {

                var DBoyorders = DeliveryIssuanceDb.Where(x => x.DeliveryIssuanceId == obj.DeliveryIssuanceId && x.IsActive == true).SingleOrDefault();
                if (DBoyorders != null)
                {
                    DBoyorders.Acceptance = obj.Acceptance;
                    if (obj.Acceptance)
                    {
                        DBoyorders.Status = "Accepted";
                    }
                    else {
                        DBoyorders.Status = "Rejected";
                    }
                    DBoyorders.RejectReason = obj.RejectReason;
                    DBoyorders.IsActive = false;
                    DBoyorders.UpdatedDate = indianTime;
                    DeliveryIssuanceDb.Attach(DBoyorders);
                    this.Entry(DBoyorders).State = EntityState.Modified;
                    this.SaveChanges();
                }
                if (obj.Acceptance)
                {
                    string[] ids = obj.OrderdispatchIds.Split(',');
                    foreach (var od in ids)
                    {
                        var oid = Convert.ToInt16(od);
                        var orderdipatchmaster = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == oid).SingleOrDefault();
                        orderdipatchmaster.Status = "Shipped";
                        orderdipatchmaster.UpdatedDate = indianTime;
                        OrderDispatchedMasters.Attach(orderdipatchmaster);
                        this.Entry(orderdipatchmaster).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                    string[] odds = obj.OrderIds.Split(',');
                    foreach (var od in odds)
                    {
                        int oid = Convert.ToInt32(od);
                        var orderMaster = DbOrderMaster.Where(x => x.OrderId == oid).SingleOrDefault();
                        orderMaster.Status = "Shipped";
                        orderMaster.UpdatedDate = indianTime;
                        DbOrderMaster.Attach(orderMaster);
                        this.Entry(orderMaster).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                }
                else {
                    string[] ids = obj.OrderdispatchIds.Split(',');
                    foreach (var od in ids)
                    {
                        var oid = Convert.ToInt16(od);
                        var orderdipatchmaster = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == oid).SingleOrDefault();
                        orderdipatchmaster.Status = "Ready to Dispatch";
                        orderdipatchmaster.ReDispatchCount += 1;
                        orderdipatchmaster.UpdatedDate = indianTime;
                        OrderDispatchedMasters.Attach(orderdipatchmaster);
                        this.Entry(orderdipatchmaster).State = EntityState.Modified;
                        this.SaveChanges();
                        
                        var orderMaster = DbOrderMaster.Where(x => x.OrderId == orderdipatchmaster.OrderId).SingleOrDefault();
                        orderMaster.Status = "Ready to Dispatch";
                        orderMaster.ReDispatchCount += 1;
                        orderMaster.UpdatedDate = indianTime;
                        DbOrderMaster.Attach(orderMaster);
                        this.Entry(orderMaster).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                }

                return "true";
            } catch (Exception ex) {
                return ex.Message;
            }
            
        }

        public string DBIssueWailt(List<OrderDispatchedMaster> obj)
        {
            try
            {
                    foreach (var od in obj)
                    {
                        var orderdipatchmaster = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == od.OrderDispatchedMasterId).SingleOrDefault();
                        orderdipatchmaster.Status = "Issued";
                        orderdipatchmaster.UpdatedDate = indianTime;
                        OrderDispatchedMasters.Attach(orderdipatchmaster);
                        this.Entry(orderdipatchmaster).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                foreach (var od in obj)
                {
                    var OrderMaster = DbOrderMaster.Where(x => x.OrderId == od.OrderId).SingleOrDefault();
                    OrderMaster.Status = "Issued";
                    OrderMaster.UpdatedDate = indianTime;
                    DbOrderMaster.Attach(OrderMaster);
                    this.Entry(OrderMaster).State = EntityState.Modified;
                    this.SaveChanges();
                }

                //List<IssuanceDetails> uniquelist = new List<IssuanceDetails>();
                //foreach (var selectunique in obj.details)
                //{
                //    var check = uniquelist.Find(x => x.OrderDispatchedMasterId == selectunique.OrderDispatchedMasterId);
                //    if (check == null)
                //    {
                //        uniquelist.Add(selectunique);
                //    }
                //}

                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion
        #region  Delivery Boys
        public List<People> AllDBoy()
        {
            return Peoples.Where(p => p.Deleted == false && p.Department == "Delivery Boy").ToList();
        }

        public People AddDboys(People obj)
        {
           
            People objCity = new People();
            City citys = Cities.Where(x => x.Cityid == obj.Cityid && x.Deleted == false).FirstOrDefault();
            Warehouse w = Warehouses.Where(x => x.Warehouseid == obj.Warehouseid).SingleOrDefault();
            Vehicle vh = VehicleDb.Where(x => x.VehicleId == obj.VehicleId).SingleOrDefault();
            List<People> v = Peoples.Where(x => x.Mobile == obj.Mobile && x.Department == "Delivery Boy" && x.Deleted == false).ToList();
            if (v.Count == 0)
            {
                if (citys != null)
                {
                    obj.Cityid = citys.Cityid;
                    obj.city = citys.CityName;
                    obj.state = citys.StateName;
                }
                if (w != null)
                {
                    obj.Warehouseid = w.Warehouseid;
                }
                if (vh != null)
                {
                    obj.VehicleId = vh.VehicleId;
                    obj.VehicleName = vh.VehicleName;
                    obj.VehicleNumber = vh.VehicleNumber;
                    obj.VehicleCapacity = vh.Capacity;
                }
                obj.DisplayName = obj.PeopleFirstName + " " + obj.PeopleLastName;
                obj.Department = "Delivery Boy";
                obj.CreatedDate = indianTime;
                obj.UpdatedDate = indianTime;
                Peoples.Add(obj);
                int id = this.SaveChanges();
                return obj;
            }
            else
            {
               return null;
            }


        }
        public People PutDboys(People obj)
        {

            City citys = Cities.Where(x => x.Cityid == obj.Cityid && x.Deleted == false).FirstOrDefault();
            Warehouse w = Warehouses.Where(x => x.Warehouseid == obj.Warehouseid).SingleOrDefault();
            Vehicle vh = VehicleDb.Where(x => x.VehicleId== obj.VehicleId).SingleOrDefault();
            People v = Peoples.Where(x => x.PeopleID == obj.PeopleID && x.Department == "Delivery Boy" && x.Deleted == false).SingleOrDefault();
            List<People> vc = Peoples.Where(x => x.Mobile == obj.Mobile && x.Department == "Delivery Boy" && x.Deleted == false).ToList();
            if (vc.Count ==1 && vc[0].PeopleID != obj.PeopleID ) return null;
            if (v != null)
            {
                if (citys != null)
                {
                    v.city = citys.CityName;
                    v.state = citys.StateName;
                    v.Cityid = citys.Cityid;
                    v.Stateid = citys.Stateid;
                }
                v.UpdatedDate = indianTime;
                v.DisplayName = obj.PeopleFirstName + " " + obj.PeopleLastName;
                v.PeopleFirstName = obj.PeopleFirstName;
                v.PeopleLastName = obj.PeopleLastName;
                v.Department = obj.Department;
                
                v.city = citys.CityName;
                v.state = citys.StateName;
                v.Active = obj.Active;
                v.Department = "Delivery Boy";
                
                if (w != null)
                {
                    v.Warehouseid = w.Warehouseid;
                }
                if (vh != null)
                {
                    v.VehicleId = vh.VehicleId;
                    v.VehicleName = vh.VehicleName;
                    v.VehicleNumber = vh.VehicleNumber;
                    v.VehicleCapacity = vh.Capacity;
                }
                
                Peoples.Attach(v);
                this.Entry(v).State = EntityState.Modified;
                this.SaveChanges();
                return v;
            }
            else
            {
                return obj;
            }

        }
        public bool DeleteDboys(int id)
        {
            try
            {
                People citys = Peoples.Where(x => x.PeopleID == id).FirstOrDefault();
                citys.Active = false;
                citys.Deleted = true;
                Peoples.Attach(citys);
                this.Entry(citys).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region  Vehicles
        public List<Vehicle> AllVehicles()
        {
            return VehicleDb.Where(p =>p.isDeleted == false).ToList();
        }
       
        public Vehicle AddVehicle(Vehicle city)
        {
            var vs = VehicleDb.Where(c => c.VehicleNumber == city.VehicleNumber).SingleOrDefault();
            Vehicle objCity = new Vehicle();

            if (vs==null)
            {
                city.CreatedDate = indianTime;
                city.UpdatedDate = indianTime;
                if (city.Cityid > 0) {
                    City ct = Cities.Where(x => x.Cityid == city.Cityid).SingleOrDefault();
                    if (ct != null) {
                        city.City = ct.CityName;
                    } 
                }
                if (city.Warehouseid > 0)
                {
                    Warehouse w = Warehouses.Where(x => x.Warehouseid == city.Warehouseid).SingleOrDefault();
                    if (w != null)
                    {
                        city.WarehouseName = w.WarehouseName;
                    }
                }
                VehicleDb.Add(city);
                int id = this.SaveChanges();
                return city;
            }
            else
            {

                return objCity;
            }


        }
        public Vehicle PutVehicle(Vehicle obj)
        {

            City citys = Cities.Where(x => x.Cityid == obj.Cityid && x.Deleted == false).FirstOrDefault();
            Warehouse w = Warehouses.Where(x => x.Warehouseid == obj.Warehouseid).SingleOrDefault();
            Vehicle v = VehicleDb.Where(x => x.VehicleId == obj.VehicleId).SingleOrDefault();
            if (v != null)
            {
                v.UpdatedDate = indianTime;
                v.VehicleName = obj.VehicleName;
                v.VehicleNumber = obj.VehicleNumber;
                v.OwnerAddress = obj.OwnerAddress;
                v.OwnerName = obj.OwnerName;
                if (citys != null)
                {
                    v.Cityid = citys.Cityid;
                    v.City = citys.CityName;
                }
                if (w != null)
                {
                    v.Warehouseid = w.Warehouseid;
                    v.WarehouseName = w.WarehouseName;
                }
                
                VehicleDb.Attach(v);
                this.Entry(v).State = EntityState.Modified;
                this.SaveChanges();
                return v;
            }
            else
            {
                return obj;
            }

        }
        public bool DeleteVehicle(int id)
        {
            try
            {
                Vehicle citys = VehicleDb.Where(x => x.VehicleId == id).FirstOrDefault();
                citys.isActive = false;
                citys.isDeleted= true;
                VehicleDb.Attach(citys);
                this.Entry(citys).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region Change delivery boy
        public List<OrderDispatchedMaster> changeDBoy(List<OrderDispatchedMaster> objlist ,string mob) {
            try {
                var DBoy = Peoples.Where(x => x.Mobile == mob && x.Deleted == false).FirstOrDefault();
                foreach (var obj in objlist)
                {
                    var od = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == obj.OrderDispatchedMasterId).SingleOrDefault();
                    if (od != null)
                    {
                        if (DBoy != null)
                        {
                            od.DboyMobileNo = DBoy.Mobile;
                            od.DboyName = DBoy.DisplayName;
                            od.UpdatedDate = indianTime;
                            OrderDispatchedMasters.Attach(od);
                            this.Entry(od).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
                return objlist;
            } catch (Exception ex) {
                logger.Error(ex.Message);
                return null;
            }
        }

        #endregion
        #region GPS tracking and History of DBoy
        public GpsCoordinate Addgps(GpsCoordinate obj)
        {
            try
            {
                GpsCoordinate objgps = new GpsCoordinate();
                objgps.DeliveryBoyId = obj.DeliveryBoyId;
                objgps.lat = obj.lat;
                objgps.lg = obj.lg;
                objgps.CreatedDate = indianTime;
                GpsCoordinateDb.Add(objgps);
                int id = this.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return obj;
            }
        }
        public List<OrderDispatchedMaster> getallOrderofboy(string mob) {
            try {
                return OrderDispatchedMasters.Where(x => x.DboyMobileNo == mob).ToList();
            } catch (Exception ex) { logger.Error(ex.Message); return null; }

        }

        public List<OrderDispatchedMasterDTO> getRedispatchordersbyboy(string mob) {

            try {
                //var list = RedispatchWarehouseDb.Where(x => x.Status == "Delivery Redispatch" && x.DboyMobileNo == mob).ToList();

                var list1 = (from i in RedispatchWarehouseDb
                             where (i.Status == "Delivery Redispatch") && i.DboyMobileNo == mob
                             join a in OrderDispatchedMasters on i.OrderDispatchedMasterId equals a.OrderDispatchedMasterId
                             join j in Customers on a.CustomerId equals j.CustomerId
                             select new OrderDispatchedMasterDTO
                             {
                                 active = a.active,
                                 BillingAddress = a.BillingAddress,
                                 CityId = a.CityId,
                                 comments = a.comments,
                                 CompanyId = a.CompanyId,
                                 CreatedDate = i.CreatedDate,
                                 CustomerId = a.CustomerId,
                                 CustomerName = j.Name,
                                 ShopName = j.ShopName,
                                 Skcode = j.Skcode,
                                 Customerphonenum = a.Customerphonenum,
                                 DboyMobileNo = a.DboyMobileNo,
                                 DboyName = a.DboyName,
                                 Deleted = a.Deleted,
                                 Deliverydate = a.Deliverydate,
                                 DiscountAmount = a.DiscountAmount,
                                 DivisionId = a.DivisionId,
                                 GrossAmount = a.GrossAmount,
                                 invoice_no = a.invoice_no,
                                 orderDetails = a.orderDetails,
                                 OrderDispatchedMasterId = a.OrderDispatchedMasterId,
                                 OrderId = a.OrderId,
                                 ReDispatchCount = a.ReDispatchCount,
                                 SalesPerson = a.SalesPerson,
                                 SalesPersonId = a.SalesPersonId,
                                 ShippingAddress = a.ShippingAddress,
                                 Status = a.Status,
                                 TaxAmount = a.TaxAmount,
                                 TotalAmount = a.TotalAmount,
                                 UpdatedDate = a.UpdatedDate,
                                 Warehouseid = a.Warehouseid,
                                 WarehouseName = a.WarehouseName
                             }).ToList();

                return list1;
            }
            catch (Exception ex) {
                logger.Error(ex.Message);
                return null;
            }
        }
        #endregion
        #region SalesPersonBeat add data from app beat
        public SalesPersonBeat Addsalesbeat(SalesPersonBeat obj)
        {
            try
            {

                People sname = Peoples.Where(f => f.PeopleID == obj.SalesPersonId).SingleOrDefault();
                Customer cname = Customers.Where(f => f.Skcode == obj.Skcode).SingleOrDefault();

                SalesPersonBeat objsals = new SalesPersonBeat();
                objsals.SalesPersonId = obj.SalesPersonId;
                objsals.Skcode = obj.Skcode;
                objsals.status = obj.status;
                objsals.Comment = obj.Comment;
                objsals.SalespersonName = sname.DisplayName;
                objsals.ShopName = cname.ShopName;
                objsals.CreatedDate = indianTime;
                SalesPersonBeatDb.Add(objsals);
                int id = this.SaveChanges();
                return obj;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return obj;
            }
        }
        #endregion
        #region Delivery App calls

        public List<OrderHistory> getDBoyOrdersHistory(string mob, DateTime? start, DateTime? end, int dboyId)//get orders History
        {
            try
            {
                List<OrderHistory> OrderHistoryList = new List<OrderHistory>();

                var Issulist = DeliveryIssuanceDb.Where(x => x.CreatedDate > start && x.CreatedDate <= end && x.PeopleID == dboyId).ToList();
                foreach (var o in Issulist)
                {
                    if (o.Status == "Accepted")
                    {
                        var orderhistory = new OrderHistory();
                        List<OrderDispatchedMaster> OrdersObj = new List<OrderDispatchedMaster>();
                        orderhistory.deliveryIssuance = o;
                        orderhistory.totalcash = 0;
                        orderhistory.Canceled = 0;
                        orderhistory.Redispatched = 0;
                        orderhistory.Delivered = 0;
                        string[] ids = o.OrderdispatchIds.Split(',');
                        foreach (var od in ids)
                        {
                            var oid = Convert.ToInt16(od);
                            var orderdipatchmaster = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == oid).SingleOrDefault();
                           
                            if (orderdipatchmaster != null)
                            {
                                //List<OrderDispatchedMasterDTO> om = new List<OrderDispatchedMasterDTO>();
                                if ((orderdipatchmaster.DboyMobileNo == mob) && (orderdipatchmaster.Status == "Delivery Canceled" || orderdipatchmaster.Status == "Order Canceled"))
                                {
                                    orderhistory.Canceled = orderhistory.Canceled + 1;
                                }
                                else if ((orderdipatchmaster.DboyMobileNo == mob) && (orderdipatchmaster.Status == "Delivered" || orderdipatchmaster.Status == "sattled"))
                                {
                                    orderhistory.Delivered = orderhistory.Delivered + 1;
                                }
                                if ((orderdipatchmaster.DboyMobileNo == mob) && (orderdipatchmaster.Status == "Delivery Redispatch" || orderdipatchmaster.ReDispatchCount > 0))
                                {
                                    orderhistory.Redispatched = orderhistory.Redispatched + 1;
                                }
                                orderhistory.totalcash = orderhistory.totalcash + orderdipatchmaster.CashAmount;
                                OrdersObj.Add(orderdipatchmaster);
                            }
                        }
                        orderhistory.Orders = OrdersObj;
                        OrderHistoryList.Add(orderhistory);
                    }
                }



                return OrderHistoryList;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        public List<OrderDispatchedMasterDTO> getAcceptedOrders(string mob)//get orders to deliver
        {
            try
            {
                // var list = OrderDispatchedMasters.Where(a => a.Status == "Shipped" && a.DboyMobileNo == mob).ToList();
                var list1 = (from a in OrderDispatchedMasters
                             where (a.Status == "Shipped") && a.DboyMobileNo == mob
                             join i in Customers on a.CustomerId equals i.CustomerId
                             join clstr in Clusters on i.ClusterId equals clstr.ClusterId
                             select new OrderDispatchedMasterDTO
                             {
                                 lat = i.lat,
                                 lg = i.lg,
                                 ClusterId = clstr.ClusterId,
                                 ClusterName = clstr.ClusterName,
                                 active = a.active,
                                 BillingAddress = a.BillingAddress,
                                 CityId = a.CityId,
                                 comments = a.comments,
                                 CompanyId = a.CompanyId,
                                 CreatedDate = a.CreatedDate,
                                 CustomerId = a.CustomerId,
                                 CustomerName = a.CustomerName,
                                 ShopName = i.ShopName,
                                 Skcode = i.Skcode,
                                 Customerphonenum = a.Customerphonenum,
                                 DboyMobileNo = a.DboyMobileNo,
                                 DboyName = a.DboyName,
                                 Deleted = a.Deleted,
                                 Deliverydate = a.Deliverydate,
                                 DiscountAmount = a.DiscountAmount,
                                 DivisionId = a.DivisionId,
                                 GrossAmount = a.GrossAmount,
                                 invoice_no = a.invoice_no,
                                 //OrderDate = a.OrderDate,
                                 orderDetails = a.orderDetails,
                                 OrderDispatchedMasterId = a.OrderDispatchedMasterId,
                                 OrderId = a.OrderId,
                                 //RecivedAmount = a.RecivedAmount,
                                 ReDispatchCount = a.ReDispatchCount,
                                 SalesPerson = a.SalesPerson,
                                 SalesPersonId = a.SalesPersonId,
                                 ShippingAddress = a.ShippingAddress,
                                 Status = a.Status,
                                 TaxAmount = a.TaxAmount,
                                 TotalAmount = a.TotalAmount,
                                 UpdatedDate = a.UpdatedDate,
                                 Warehouseid = a.Warehouseid,
                                 WarehouseName = a.WarehouseName
                             }).ToList();

                return list1;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        //Order delivered or canceled or Redispatched
        public OrderDispatchedMaster orderdeliveredreturn(OrderDispatchedMaster obj)
        {
            try
            {
                if (obj.Status == "Delivered")
                {
                    var ODM = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == obj.OrderDispatchedMasterId).SingleOrDefault();
                    if (ODM != null)
                    {
                        ODM.Status = obj.Status;
                        ODM.comments = obj.comments;
                        ODM.CheckNo = obj.CheckNo;
                        ODM.CheckAmount = obj.CheckAmount;
                        ODM.ElectronicAmount = obj.ElectronicAmount;
                        ODM.ElectronicPaymentNo = obj.ElectronicPaymentNo;
                        ODM.RecivedAmount = obj.RecivedAmount;
                        ODM.Signimg = obj.Signimg;
                        ODM.CashAmount = obj.CashAmount;
                        ODM.UpdatedDate = indianTime;
                        OrderDispatchedMasters.Attach(ODM);
                        this.Entry(ODM).State = EntityState.Modified;
                        this.SaveChanges();
                        foreach (var detail in ODM.orderDetails)
                        {
                            detail.Status = obj.Status;
                            OrderDispatchedDetailss.Attach(detail);
                            this.Entry(detail).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                        if (ODM.ReDispatchCount > 0)
                        {
                            try
                            {
                                var RO = RedispatchWarehouseDb.Where(x => x.OrderId == obj.OrderId && x.DboyMobileNo == obj.DboyMobileNo).SingleOrDefault();
                                if (RO != null)
                                {
                                    RO.Status = obj.Status;
                                    RO.comments = obj.comments;
                                    RO.UpdatedDate = indianTime;
                                    RedispatchWarehouseDb.Attach(RO);
                                    this.Entry(RO).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }

                        }
                        var ODMaster = DbOrderMaster.Where(x => x.OrderId == obj.OrderId).SingleOrDefault();
                        if (ODMaster != null)
                        {
                            ODMaster.Status = "Delivered";
                            ODMaster.comments = obj.comments;
                            ODMaster.UpdatedDate = indianTime;
                            DbOrderMaster.Attach(ODMaster);
                            this.Entry(ODMaster).State = EntityState.Modified;
                            this.SaveChanges();
                            foreach (var detail in ODMaster.orderDetails)
                            {
                                detail.Status = obj.Status;
                                detail.UpdatedDate = indianTime;
                                DbOrderDetails.Attach(detail);
                                this.Entry(detail).State = EntityState.Modified;
                                this.SaveChanges();
                            }
                            Wallet wlt = WalletDb.Where(c => c.CustomerId == ODMaster.CustomerId).SingleOrDefault();
                            if (wlt != null)
                            {
                                if (ODMaster.RewardPoint > 0)
                                {
                                    wlt.TotalAmount += ODMaster.RewardPoint;
                                    wlt.TransactionDate = indianTime;

                                    WalletDb.Attach(wlt);
                                    Entry(wlt).State = EntityState.Modified;
                                    SaveChanges();
                                    var rpoint = RewardPointDb.Where(c => c.CustomerId == ODMaster.CustomerId).SingleOrDefault();
                                    if (rpoint != null)
                                    {
                                        rpoint.EarningPoint -= ODMaster.RewardPoint;
                                        if (rpoint.EarningPoint < 0)
                                            rpoint.EarningPoint = 0;
                                        rpoint.UpdatedDate = indianTime;
                                        RewardPointDb.Attach(rpoint);
                                        Entry(rpoint).State = EntityState.Modified;
                                        SaveChanges();
                                    }
                                }
                            }
                        }

                        return obj;
                    }
                }
                else if (obj.Status == "Delivery Canceled")
                {
                    try
                    {
                        var ODM = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == obj.OrderDispatchedMasterId).SingleOrDefault();
                        if (ODM != null)
                        {
                            ODM.Status = obj.Status;
                            ODM.comments = obj.comments;
                            ODM.Signimg = obj.Signimg;
                            ODM.UpdatedDate = indianTime;
                            OrderDispatchedMasters.Attach(ODM);
                            this.Entry(ODM).State = EntityState.Modified;
                            this.SaveChanges();
                            foreach (var detail in ODM.orderDetails)
                            {
                                detail.Status = obj.Status;
                                detail.UpdatedDate = indianTime;
                                OrderDispatchedDetailss.Attach(detail);
                                this.Entry(detail).State = EntityState.Modified;
                                this.SaveChanges();
                            }
                            var OMaster = DbOrderMaster.Where(x => x.OrderId == obj.OrderId).SingleOrDefault();
                            if (OMaster != null)
                            {
                                OMaster.Status = obj.Status;
                                OMaster.comments = obj.comments;
                                OMaster.UpdatedDate = indianTime;
                                DbOrderMaster.Attach(OMaster);
                                this.Entry(OMaster).State = EntityState.Modified;
                                this.SaveChanges();
                                foreach (var detail in OMaster.orderDetails)
                                {
                                    detail.Status = obj.Status;
                                    detail.UpdatedDate = indianTime;
                                    DbOrderDetails.Attach(detail);
                                    this.Entry(detail).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                            }
                            try
                            {
                                var RO = RedispatchWarehouseDb.Where(x => x.OrderId == obj.OrderId && x.DboyMobileNo == obj.DboyMobileNo).SingleOrDefault();
                                if (RO != null)
                                {
                                    RO.Status = obj.Status;
                                    RO.comments = obj.comments;
                                    RO.UpdatedDate = indianTime;
                                    RedispatchWarehouseDb.Attach(RO);
                                    this.Entry(RO).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                            }
                            catch (Exception ex) { logger.Error(ex.Message); }
                        }
                        return obj;

                    }
                    catch (Exception ex) { logger.Error(ex.Message); }
                }
                else if (obj.Status == "Delivery Redispatch")
                {
                    var ODM = OrderDispatchedMasters.Where(x => x.OrderDispatchedMasterId == obj.OrderDispatchedMasterId).SingleOrDefault();
                    if (ODM != null)
                    {
                        ODM.Status = "Delivery Redispatch";
                        ODM.ReDispatchCount = ODM.ReDispatchCount + 1;
                        ODM.Signimg = obj.Signimg;
                        ODM.comments = obj.comments;
                        ODM.UpdatedDate = indianTime;
                        OrderDispatchedMasters.Attach(ODM);
                        this.Entry(ODM).State = EntityState.Modified;
                        this.SaveChanges();
                        foreach (var detail in ODM.orderDetails)
                        {
                            detail.Status = obj.Status;
                            OrderDispatchedDetailss.Attach(detail);
                            this.Entry(detail).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                        var OMaster = DbOrderMaster.Where(x => x.OrderId == obj.OrderId).SingleOrDefault();
                        if (OMaster != null)
                        {
                            OMaster.Status = obj.Status;
                            OMaster.comments = obj.comments;
                            OMaster.UpdatedDate = indianTime;
                            OMaster.ReDispatchCount = obj.ReDispatchCount + 1;
                            DbOrderMaster.Attach(OMaster);
                            this.Entry(OMaster).State = EntityState.Modified;
                            this.SaveChanges();
                            foreach (var detail in OMaster.orderDetails)
                            {
                                detail.Status = obj.Status;
                                DbOrderDetails.Attach(detail);
                                this.Entry(detail).State = EntityState.Modified;
                                this.SaveChanges();
                            }
                        }
                        if (obj.ReDispatchCount == 0)
                        {
                            RedispatchWarehouse RO = new RedispatchWarehouse();
                            RO.active = true;
                            RO.comments = obj.comments;
                            RO.CompanyId = ODM.CompanyId;
                            RO.CreatedDate = indianTime;
                            RO.UpdatedDate = indianTime;
                            RO.DboyMobileNo = obj.DboyMobileNo;
                            RO.DboyName = obj.DboyName;
                            RO.Deleted = false;
                            RO.OrderDispatchedMasterId = obj.OrderDispatchedMasterId;
                            RO.OrderId = obj.OrderId;
                            RO.Warehouseid = obj.Warehouseid;
                            RO.ReDispatchCount = obj.ReDispatchCount + 1;
                            RO.Status = obj.Status;
                            RedispatchWarehouseDb.Add(RO);
                            this.SaveChanges();
                        }
                        else
                        {
                            try
                            {
                                var RO = RedispatchWarehouseDb.Where(x => x.OrderId == obj.OrderId && x.DboyMobileNo == obj.DboyMobileNo).SingleOrDefault();
                                if (RO != null)
                                {
                                    RO.Status = obj.Status;
                                    RO.comments = obj.comments;
                                    RO.ReDispatchCount = obj.ReDispatchCount + 1;
                                    RO.UpdatedDate = indianTime;
                                    RedispatchWarehouseDb.Attach(RO);
                                    this.Entry(RO).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                                else
                                {
                                    RO = new RedispatchWarehouse();
                                    RO.active = true;
                                    RO.comments = obj.comments;
                                    RO.CompanyId = ODM.CompanyId;
                                    RO.CreatedDate = indianTime;
                                    RO.UpdatedDate = indianTime;
                                    RO.DboyMobileNo = obj.DboyMobileNo;
                                    RO.DboyName = obj.DboyName;
                                    RO.Deleted = false;
                                    RO.OrderDispatchedMasterId = obj.OrderDispatchedMasterId;
                                    RO.OrderId = obj.OrderId;
                                    RO.Warehouseid = obj.Warehouseid;
                                    RO.ReDispatchCount = obj.ReDispatchCount + 1;
                                    RO.Status = obj.Status;
                                    RedispatchWarehouseDb.Add(RO);
                                    this.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }

                    }
                    return obj;
                }

                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        public List<ReturnOrderDispatchedDetails> createreturnobj(OrderDispatchedMaster obj)
        {
            List<ReturnOrderDispatchedDetails> Returnlist = new List<ReturnOrderDispatchedDetails>();
            foreach (var RD in obj.orderDetails)
            {  try
                {
                    ReturnOrderDispatchedDetails ROD = new ReturnOrderDispatchedDetails();
                    ROD.Barcode = RD.Barcode;
                    ROD.CategoryName = RD.CategoryName;
                    ROD.City = RD.City;
                    ROD.CompanyId = RD.CompanyId;
                    ROD.CreatedDate = RD.CreatedDate;
                    ROD.CustomerId = RD.CustomerId;
                    ROD.CustomerName = RD.CustomerName;
                    ROD.Deleted = RD.Deleted;
                    ROD.DiscountAmmount = RD.DiscountAmmount;
                    ROD.DiscountPercentage = RD.DiscountPercentage;
                    ROD.isDeleted = RD.isDeleted;
                    ROD.itemcode = RD.itemcode;
                    ROD.ItemId = RD.ItemId;
                    ROD.itemname = RD.itemname;
                    ROD.Itempic = RD.Itempic;
                    ROD.MinOrderQty = RD.MinOrderQty;
                    ROD.MinOrderQtyPrice = RD.MinOrderQtyPrice;
                    ROD.Mobile = RD.Mobile;
                    ROD.NetAmmount = RD.NetAmmount;
                    ROD.NetAmtAfterDis = RD.NetAmtAfterDis;
                    ROD.OrderDate = RD.OrderDate;
                    ROD.OrderDetailsId = RD.OrderDetailsId;
                    ROD.OrderDispatchedDetailsId = RD.OrderDispatchedDetailsId;
                    ROD.OrderDispatchedMasterId = RD.OrderDispatchedMasterId;
                    ROD.OrderId = RD.OrderId;
                    ROD.price = RD.price;
                    ROD.Purchaseprice = RD.Purchaseprice;
                    ROD.qty = RD.qty;
                    ROD.Status = RD.Status;
                    ROD.TaxAmmount = RD.TaxAmmount;
                    ROD.TaxPercentage = RD.TaxPercentage;
                    ROD.TotalAmt = RD.TotalAmt;
                    ROD.UpdatedDate = RD.UpdatedDate;
                    ROD.UnitPrice = RD.UnitPrice;
                    ROD.Warehouseid = RD.Warehouseid;
                    ROD.WarehouseName = RD.WarehouseName;
                    Returnlist.Add(ROD);
                }
                catch (Exception ex)
                { logger.Error(ex.Message); return null; }
            }
            return Returnlist;
        }

        public List<ReturnOrderDispatchedDetails> ReturndeliveryOrders(List<ReturnOrderDispatchedDetails> po)
        {
            int Oid = po[0].OrderId;
            List<OrderDispatchedDetails> dispatchedobj = new List<OrderDispatchedDetails>();
            dispatchedobj = OrderDispatchedDetailss.Where(x => x.OrderId == Oid).ToList();
            List<FinalOrderDispatchedDetails> Finalobj = new List<FinalOrderDispatchedDetails>();
            try
            {
                for (var i = 0; i < dispatchedobj.Count; i++)
                {
                    for (var k = 0; k < po.Count; k++)
                    {
                        if (po[k].isDeleted != true && dispatchedobj[i].ItemId == po[k].ItemId)
                        {
                            dispatchedobj[i].qty = dispatchedobj[i].qty - po[k].qty;
                            var itemIDmaster = dispatchedobj[i].ItemId;
                            ItemMaster items = itemMasters.Where(x => x.ItemId == itemIDmaster).Select(x => x).FirstOrDefault();
                            FinalOrderDispatchedDetails newfinal = new FinalOrderDispatchedDetails();
                            newfinal.OrderDispatchedDetailsId = dispatchedobj[i].OrderDispatchedDetailsId;
                            newfinal.OrderDetailsId = dispatchedobj[i].OrderDetailsId;
                            newfinal.OrderId = dispatchedobj[i].OrderId;
                            newfinal.OrderDispatchedMasterId = dispatchedobj[i].OrderDispatchedMasterId;
                            newfinal.CustomerId = dispatchedobj[i].CustomerId;
                            newfinal.CustomerName = dispatchedobj[i].CustomerName;
                            newfinal.City = dispatchedobj[i].City;
                            newfinal.Mobile = dispatchedobj[i].Mobile;
                            newfinal.OrderDate = dispatchedobj[i].OrderDate;
                            newfinal.CompanyId = dispatchedobj[i].CompanyId;
                            newfinal.CityId = dispatchedobj[i].CityId;
                            newfinal.Warehouseid = dispatchedobj[i].Warehouseid;
                            newfinal.WarehouseName = dispatchedobj[i].WarehouseName;
                            newfinal.CategoryName = dispatchedobj[i].CategoryName;

                            newfinal.ItemId = dispatchedobj[i].ItemId;
                            newfinal.Itempic = dispatchedobj[i].Itempic;
                            newfinal.itemname = dispatchedobj[i].itemname;
                            newfinal.itemcode = dispatchedobj[i].itemcode;
                            newfinal.Barcode = dispatchedobj[i].Barcode;
                            newfinal.UnitPrice = dispatchedobj[i].UnitPrice;
                            newfinal.Purchaseprice = dispatchedobj[i].Purchaseprice;
                            newfinal.MinOrderQty = dispatchedobj[i].MinOrderQty;
                            newfinal.MinOrderQtyPrice = dispatchedobj[i].MinOrderQtyPrice;
                            newfinal.qty = dispatchedobj[i].qty;
                            newfinal.price = dispatchedobj[i].price;
                            newfinal.MinOrderQty = dispatchedobj[i].MinOrderQty;
                            int MOQ = dispatchedobj[i].MinOrderQty;
                            newfinal.MinOrderQtyPrice = MOQ * dispatchedobj[i].UnitPrice;
                            newfinal.qty = Convert.ToInt32(dispatchedobj[i].qty);

                            int qty = 0;
                            qty = Convert.ToInt32(newfinal.qty);

                            newfinal.TaxPercentage = items.TotalTaxPercentage;
                            //........CALCULATION FOR NEW SHOPKIRANA.............................
                            newfinal.Noqty = qty; // for total qty (no of items)

                            // STEP 1  (UNIT PRICE * QTY)     - SHOW PROPERTY                  
                            newfinal.TotalAmt = System.Math.Round(newfinal.UnitPrice * qty, 2);

                            // STEP 2 (AMOUT WITHOU TEX AND WITHOUT DISCOUNT ) - SHOW PROPERTY
                            newfinal.AmtWithoutTaxDisc = ((100 * newfinal.UnitPrice * qty) / (1 + newfinal.TaxPercentage / 100)) / 100;

                            // STEP 3 (AMOUNT WITHOUT TAX AFTER DISCOUNT) - UNSHOW PROPERTY
                            newfinal.AmtWithoutAfterTaxDisc = (100 * newfinal.AmtWithoutTaxDisc) / (100 + items.PramotionalDiscount);

                            //STEP 4 (TAX AMOUNT) - UNSHOW PROPERTY
                            newfinal.TaxAmmount = (newfinal.AmtWithoutAfterTaxDisc * newfinal.TaxPercentage) / 100;

                            //STEP 5(TOTAL TAX AMOUNT) - UNSHOW PROPERTY
                            newfinal.TotalAmountAfterTaxDisc = newfinal.AmtWithoutAfterTaxDisc + newfinal.TaxAmmount;

                            //...............Calculate Discount.............................
                            newfinal.DiscountPercentage = items.PramotionalDiscount;
                            newfinal.DiscountAmmount = 0;
                            newfinal.NetAmtAfterDis = 0;
                            //...................................................................
                            newfinal.Purchaseprice = items.price;
                            //newfinal.VATTax = items.VATTax;
                            newfinal.CreatedDate = Convert.ToDateTime(dispatchedobj[i].CreatedDate);
                            newfinal.UpdatedDate = Convert.ToDateTime(dispatchedobj[i].CreatedDate);
                            newfinal.Deleted = false;

                            Finalobj.Add(newfinal);
                        }
                    }
                }
                foreach (FinalOrderDispatchedDetails x1 in Finalobj)
                {
                    
                    this.FinalOrderDispatchedDetailsDb.Add(x1);
                    int id = this.SaveChanges();
                }
                foreach (ReturnOrderDispatchedDetails pc in po)
                {
                    OrderMaster om = DbOrderMaster.Where(x => x.OrderId == pc.OrderId && x.Deleted == false).FirstOrDefault();
                    om.Status = "Order Canceled";
                    this.DbOrderMaster.Attach(om);
                    this.Entry(om).State = EntityState.Modified;
                    this.SaveChanges();

                    OrderDispatchedMaster odm = OrderDispatchedMasters.Where(x => x.OrderId == pc.OrderId && x.Deleted == false).FirstOrDefault();
                    odm.Status = "Order Canceled";
                    this.OrderDispatchedMasters.Attach(odm);
                    this.Entry(odm).State = EntityState.Modified;
                    this.SaveChanges();

                    //update stock
                    ItemMaster master = itemMasters.Where(c => c.ItemId == pc.ItemId).SingleOrDefault();
                    CurrentStock itemm = DbCurrentStock.Where(x => x.ItemNumber == master.Number && x.Warehouseid == pc.Warehouseid).SingleOrDefault();

                    if (itemm != null)
                    {
                        itemm.CurrentInventory = Convert.ToInt32(itemm.CurrentInventory + (master.MinOrderQty * pc.qty));
                        UpdateCurrentStock(itemm);
                    }
                    var ord = DbOrderDetails.Where(r => r.OrderDetailsId == pc.OrderDetailsId).SingleOrDefault();
                    ord.Status = "Order Canceled";
                    DbOrderDetails.Attach(ord);
                    this.Entry(ord).State = EntityState.Modified;
                    this.SaveChanges();

                    AddReturnOrderDispatchedDetails(pc);
                }
                try
                {
                    OrderMaster om = DbOrderMaster.Where(x => x.OrderId == po[0].OrderId && x.Deleted == false).FirstOrDefault();
                    var rpoint = RewardPointDb.Where(c => c.CustomerId == om.CustomerId).FirstOrDefault();
                    if (rpoint != null)
                    {
                        if (om.RewardPoint > 0)
                        {
                            rpoint.EarningPoint -= om.RewardPoint;
                            if (rpoint.EarningPoint < 0)
                                rpoint.EarningPoint = 0;
                            rpoint.UpdatedDate = indianTime;
                            rpoint.TransactionDate = indianTime;
                            RewardPointDb.Attach(rpoint);
                            this.Entry(rpoint).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) { }
                return po;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        #endregion
        #region Clusters
        public IEnumerable<Cluster> AllCluster(int compid)
        {
            if (Clusters.AsEnumerable().Count() > 0)
            {
                return Clusters.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<Cluster> Cluster = new List<Cluster>();
                return Cluster.AsEnumerable();
            }
        }
        public Cluster Addcluster(Cluster cluster)
        {
            if (cluster.Warehouseid > 0)
            {
                var wh = Warehouses.Where(x => x.Warehouseid == cluster.Warehouseid).SingleOrDefault();
                if (wh != null)
                {
                    cluster.WarehouseName = wh.WarehouseName;
                }
            }

            cluster.CreatedDate = indianTime;
            cluster.UpdatedDate = indianTime;
            Clusters.Add(cluster);
            int id = this.SaveChanges();
            return cluster;
        }
        public Cluster UpdateCluster(Cluster cluster)
        {
            try
            {
                Cluster clust = Clusters.Where(x => x.ClusterId == cluster.ClusterId && x.Deleted == false).FirstOrDefault();
                if (clust != null)
                {
                    if (cluster.Warehouseid > 0)
                    {
                        var wh = Warehouses.Where(x => x.Warehouseid == cluster.Warehouseid).SingleOrDefault();
                        if (wh != null)
                        {
                            cluster.WarehouseName = wh.WarehouseName;
                        }
                    }

                    clust.ClusterName = cluster.ClusterName;
                    clust.Warehouseid = cluster.Warehouseid;
                    clust.WarehouseName = cluster.WarehouseName;
                    clust.Address = cluster.Address;
                    clust.Phone = cluster.Phone;
                    clust.Active = cluster.Active;
                    clust.CreatedDate = cluster.CreatedDate;
                    clust.UpdatedDate = indianTime;
                    Clusters.Attach(clust);
                    this.Entry(clust).State = EntityState.Modified;
                    int id = this.SaveChanges();
                    return clust;
                }
                else
                {
                    return clust;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteCluster(int id)
        {
            try
            {
                Cluster cl = Clusters.Where(x => x.ClusterId == id).FirstOrDefault();
                cl.Deleted = true;
                cl.Active = false;
                Clusters.Attach(cl);
                this.Entry(cl).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Cluster getClusterbyid(int id)
        {
            return Clusters.Where(p => p.ClusterId == id && p.Deleted == false).SingleOrDefault();
        }
        #endregion
        #region Reports 

        //order report Month wise
        public IList<OrderDetails> OrderMonthReport(DateTime? datefrom, DateTime? dateto)
        {
            if (dateto != null && datefrom != null)
            {
                var result = DbOrderDetails.Where(x => x.CreatedDate > datefrom && x.CreatedDate < dateto).ToList();
                return result;
            }
            else {
                return null;
            }
        }
        
        //customer static data using month to current date

        //public IList<Customer> CustReport(DateTime? datefrom, DateTime? dateto)
        //{
        //    if (dateto != null && datefrom != null)
        //    {
        //        var CustReportResult = Customers.Where(x => x.CreatedDate > datefrom && x.CreatedDate < dateto && x.CustOrderCount >= 2).ToList();
        //        return CustReportResult;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /*customer report in graph by harry*/
        public IList<Customer> filteredCustomerReport(DateTime? datefrom, DateTime? dateto)
        {

            if (dateto != null && datefrom != null)
            {

                var result = Customers.Where(x => x.CreatedDate > datefrom && x.CreatedDate < dateto).ToList();
                // int nResults = result.Count();
                return result;
            }
            else
            {
                return null;
            }
        }

        //filtered report for order
        public IList<OrderMaster> filteredOrderMasters11(DateTime datefrom, DateTime dateto)
        {
            if (dateto != null && datefrom != null)

            {
                var result = DbOrderMaster.Where(x => x.CreatedDate > datefrom && x.CreatedDate < dateto).ToList();
                return result;
            }
            else
            {
                return null;
            }
        }
        
        public IEnumerable<OrderMaster> AllOrderMasters
        {
            get { return DbOrderMaster.Where(x => x.Deleted == false).AsEnumerable(); }

        }

        public IEnumerable<DamageOrderMaster> AllDOrderMasters
        {
            get { return DamageOrderMasterDB.Where(x => x.Deleted == false).AsEnumerable(); }

        }

        #endregion
        public Customer AllSalePersonRetailer(string srch, int id1)
        {
            try
            {
                var list = Customers.Where(x=> x.Skcode == srch.Trim()).FirstOrDefault();
                return list;
            } catch (Exception ex) {
                logger.Error(ex.Message);
                return null;
            } 
        }



        #region CurrencySetttle
        public List<DBoyCurrency> getdboysCurrency(int PeopleID)
        {
            try
            {
                var list = DBoyCurrencyDB.Where(a => a.PeopleId == PeopleID && a.Status== "Delivered Boy Currency").ToList();

                return list;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }



        public DBoyCurrency DboyCu(DBoyCurrency objlist, int PeopleID)
        {
            try
            {
               var DBoy = Peoples.Where(x => x.PeopleID == PeopleID && x.Deleted == false).FirstOrDefault();
                       if (objlist != null)
                {
                   
                    if (DBoy != null)
                    {
                        
                                string s = objlist.checkamount;
                                string[] values = s.Split(',');

                     
                        foreach (var o in values)
                        {     
                            //objlist.TotalAmount = objlist.TotalAmount + Convert.ToInt32(o.Trim());
                            objlist.checkTotalAmount = objlist.checkTotalAmount + Convert.ToInt32(o.Trim());
                        }
                        string s1 = objlist.checknumber;
                        string[] valuess = s1.Split(',');
                        //foreach (var o1 in valuess)
                        //{
                        //    //objlist.TotalAmount = objlist.TotalAmount + Convert.ToInt32(o.Trim());
                        //    objlist.checknumber = objlist.checknumber + Convert.ToInt32(o1.Trim());
                        //}



                    }
                        objlist.UpdatedDate = indianTime;
                        objlist.CreatedDate = indianTime;
                        objlist.PeopleId = DBoy.PeopleID;
                        objlist.Peoplename = DBoy.DisplayName;
                        objlist.Status = "Delivered Boy Currency";
                        DBoyCurrencyDB.Add(objlist);
                        int id = this.SaveChanges();
                    }
                   
               
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
            return objlist;
        }
        //public dynamic stockadd(dynamic objlist)
        //{
        //    try
        //    {

        //        if (objlist != null)
        //        {



        //            objlist.UpdatedDate = indianTime;
        //            objlist.CreatedDate = indianTime;

        //            //objlist.Status = "Delivered Boy Currency Settled";
        //            CurrencyStockDB.Add(objlist);
        //            int id = this.SaveChanges();
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        return null;
        //    }
        //    return objlist;
        //}
        //public CurrencyStock DboyStock(CurrencyStock objlist, int PeopleID)
        //{
        //    try
        //    {
        //        var deliveryBoy = Peoples.Where(x => x.PeopleID == PeopleID && x.Deleted == false).FirstOrDefault();

        //        if (objlist != null)
        //        {
        //            var existDatas = CurrencyStockDB.Where(x => x.Deleted == false).FirstOrDefault();
        //            var existData = CurrencyStockDB.Where(x => x.CurrencyStockid == existDatas.CurrencyStockid && x.Deleted == false).FirstOrDefault();



        //            if (deliveryBoy != null)
        //            {
        //                objlist.ArrayIds = "";
        //                foreach (var o in objlist.AssignAmountId)
        //                {
        //                    if (objlist.ArrayIds == "")
        //                    {
        //                        objlist.ArrayIds = Convert.ToString(o.DBoyCId);
        //                    }
        //                    else
        //                    {
        //                        objlist.ArrayIds = objlist.ArrayIds + "," + Convert.ToString(o.DBoyCId);
        //                    }
        //                }
        //                foreach (var o in objlist.AssignAmountId)
        //                {



        //                    try
        //                    {
        //                        if (existData == null)
        //                        {
        //                            objlist.UpdatedDate = indianTime;
        //                            objlist.CreatedDate = indianTime;
        //                            objlist.DboyName = deliveryBoy.DisplayName;
        //                            objlist.status = "Delivered Boy Currency Inserted InCST";
        //                            CurrencyStockDB.Add(objlist);
        //                            int id = this.SaveChanges();
        //                        }
        //                        else
        //                        {
        //                            CurrencyStock CST = new CurrencyStock();
        //                            CST.OneRupee = existData.OneRupee + objlist.OneRupee;
        //                            CST.onerscount = existData.onerscount + objlist.onerscount;
        //                            CST.TwoRupee = existData.TwoRupee + objlist.TwoRupee;
        //                            CST.tworscount = existData.tworscount + objlist.tworscount;
        //                            CST.FiveRupee = existData.FiveRupee + objlist.FiveRupee;
        //                            CST.fiverscount = existData.fiverscount + objlist.fiverscount;
        //                            CST.TenRupee = existData.TenRupee + objlist.TenRupee;
        //                            CST.tenrscount = existData.tenrscount + objlist.tenrscount;
        //                            CST.TwentyRupee = existData.TwentyRupee + objlist.TwentyRupee;
        //                            CST.Twentyrscount = existData.Twentyrscount + objlist.Twentyrscount;
        //                            CST.fiftyRupee = existData.fiftyRupee + objlist.fiftyRupee;
        //                            CST.fiftyrscount = existData.fiftyrscount + objlist.fiftyrscount;
        //                            CST.HunRupee = existData.HunRupee + objlist.HunRupee;
        //                            CST.hunrscount = existData.hunrscount + objlist.hunrscount;
        //                            CST.fiveHRupee = existData.fiveHRupee + objlist.fiveHRupee;
        //                            CST.fivehrscount = existData.fivehrscount + objlist.fivehrscount;
        //                            CST.twoTHRupee = existData.twoTHRupee + objlist.twoTHRupee;
        //                            CST.twoTHrscount = existData.twoTHrscount + objlist.twoTHrscount;
        //                            CST.TotalAmount = existData.TotalAmount + objlist.TotalAmount;
        //                            CST.UpdatedDate = indianTime;
        //                            CurrencyStockDB.Attach(CST);
        //                            this.Entry(CST).State = EntityState.Modified;
        //                            this.SaveChanges();
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        logger.Error(ex.Message);
        //                        return null;
        //                    }


        //                    DBoyCurrency db = DBoyCurrencyDB.Where(x => x.DBoyCId == o.DBoyCId && x.Deleted == false).FirstOrDefault();
        //                    db.Status = "Delivered Boy Currency Settled";
        //                    db.UpdatedDate = indianTime;
        //                    DBoyCurrencyDB.Attach(db);
        //                    this.Entry(db).State = EntityState.Modified;
        //                    this.SaveChanges();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //        return null;
        //    }
        //    return objlist;
        //}

        public CurrencyBankSettle BankStock(CurrencyBankSettle objlist,int id)
        {
            try
            {
                if (objlist != null)
                {
                        objlist.UpdatedDate = indianTime;
                        objlist.CreatedDate = indianTime;
                        objlist.status = "Pending for Bank Slip";
                        CurrencyBankSettleDB.Add(objlist);
                        int id1 = this.SaveChanges();
                        if (id1 > 0)
                        {
                        CurrencyHistory db = CurrencyHistoryDB.Where(x => x.CurrencyHistoryid == id && x.Deleted == false).FirstOrDefault();
                        if (db != null)
                        {
                            db.UpdatedDate = indianTime;
                            db.TotalAmount = db.TotalAmount - objlist.Withdrawl;
                            db.onerscount = db.onerscount - objlist.onerscount;
                            db.OneRupee = db.OneRupee - objlist.OneRupee;
                            db.tworscount = db.tworscount - objlist.tworscount;
                            db.TwoRupee = db.TwoRupee - objlist.TwoRupee;
                            db.fiverscount = db.fiverscount - objlist.fiverscount;
                            db.FiveRupee = db.FiveRupee - objlist.FiveRupee;
                            db.tenrscount = db.tenrscount - objlist.tenrscount;
                            db.TenRupee = db.TenRupee - objlist.TenRupee;
                            db.Twentyrscount = db.Twentyrscount - objlist.Twentyrscount;
                            db.TwentyRupee = db.TwentyRupee - objlist.TwentyRupee;
                            db.fiftyrscount = db.fiftyrscount - objlist.fiftyrscount;
                            db.fiftyRupee = db.fiftyRupee - objlist.fiftyRupee;
                            db.hunrscount = db.hunrscount - objlist.hunrscount;
                            db.HunRupee = db.HunRupee - objlist.HunRupee;
                            db.fivehrscount = db.fivehrscount - objlist.fivehrscount;
                            db.fiveHRupee = db.fiveHRupee - objlist.fiveHRupee;
                            db.twoTHrscount = db.twoTHrscount - objlist.twoTHrscount;
                            db.twoTHRupee = db.twoTHRupee - objlist.twoTHRupee;
                            CurrencyHistoryDB.Attach(db);
                            this.Entry(db).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
            return objlist;
        }



        public CurrencyBankSettle BankCurrencyPut(CurrencyBankSettle objlist)
        {
            logger.Info("put Complain: ");
            CurrencyBankSettle comp = CurrencyBankSettleDB.Where(x => x.CurrencyBankSettleId == objlist.CurrencyBankSettleId).FirstOrDefault();
            try
            {
                if (comp != null)
                {
                    comp.UpdatedDate = indianTime;
                    comp.status = "Bank Settled";
                    comp.DepositedBankSlip = objlist.DepositedBankSlip;
                    CurrencyBankSettleDB.Attach(comp);
                    this.Entry(comp).State = EntityState.Modified;
                    this.SaveChanges(); 
                }
                else
                {
                    logger.Error("This Complain is not Found int put " + objlist.Name);
                    return objlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Complain " + ex.Message);
            }
            return objlist;
        }
        #endregion


        public List<OrderDispatchedMaster> getdboysOrder(string mob)
        {
            try
            {
                var list = OrderDispatchedMasters.Where(a =>a.DboyMobileNo == mob &&(a.Status == "Ready to Dispatch" || a.Status == "Delivery Redispatch")).ToList();
                
                return list;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            } 
        }
        public Customer Resetpassword(Customer customer)
        {
            Customer cust = Customers.Where(x => x.Mobile == customer.Mobile && x.Deleted == false).FirstOrDefault();
            cust.Password = customer.Password;
            Customers.Attach(cust);
            this.Entry(cust).State = EntityState.Modified;
            this.SaveChanges();
            return cust;
        }
        public IEnumerable<News> AllNews()
        {

            if (NewsDb.AsEnumerable().Where(c => c.IsDeleted == false).Count() > 0)
            {
                List<News> complainList = new List<News>();
                complainList = NewsDb.Where(c => c.IsDeleted == false).ToList();
                return complainList;
            }
            else
            {
                List<News> complainList = new List<News>();
                return complainList;
            }
        }
        public News GetNewsId(int id)
        {
            News news = NewsDb.Where(x => x.NewsId == id).FirstOrDefault();
            return news;
        }
        
        public News AddNews(News complain)
        {
            List<News> complainList = NewsDb.Where(c => c.NewsId.Equals(complain.NewsId)).ToList();
            if (complainList.Count == 0)
            {
                IsAvai();
                //complain.IsAvailable = true;
                NewsDb.Add(complain);
                int id = this.SaveChanges();
                return complain;
            }
            else
            {
                News objComplain = new News();
                return objComplain;
            }
        }
        
        public News PutNews(News complain)
        {
            logger.Info("put Complain: ");
            News comp = NewsDb.Where(x => x.NewsId == complain.NewsId).FirstOrDefault();
            try
            {
                if (comp != null)
                {
                    IsAvai();
                    comp.NewsName = complain.NewsName;
                    comp.Description = complain.Description;
                    comp.Image = complain.Image;
                    comp.IsDeleted = complain.IsDeleted;
                    comp.IsAvailable = complain.IsAvailable;

                    NewsDb.Attach(comp);
                    this.Entry(comp).State = EntityState.Modified;
                    this.SaveChanges();
                    return complain;
                }
                else
                {
                    logger.Error("This Complain is not Found int put " + complain.NewsName);
                    return complain;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Complain " + ex.Message);
            }
            return comp;
        }
        
        public bool DeleteNews(int id)
        {
            try
            {
                News complain = NewsDb.Where(x => x.NewsId == id).FirstOrDefault();

                if (complain != null)
                {
                    complain.IsDeleted = true;
                    //complain.Deleted = false;
                    complain.IsAvailable = false;
                    NewsDb.Attach(complain);
                    Entry(complain).State = EntityState.Modified;
                    SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }
        
        public void IsAvai()
        {
            List<News> newslist = new List<News>();
            newslist = NewsDb.ToList();
            foreach (var a in newslist)
            {
                logger.Info("put Complain: ");
                News comp = NewsDb.Where(x => x.NewsId == a.NewsId).FirstOrDefault();
                try
                {
                    if (comp != null)
                    {
                        comp.NewsName = a.NewsName;
                        comp.Description = a.Description;
                        comp.Image = a.Image;
                        comp.IsDeleted = a.IsDeleted;
                        comp.IsAvailable = a.IsAvailable;

                        NewsDb.Attach(comp);
                        this.Entry(comp).State = EntityState.Modified;
                        this.SaveChanges();

                    }
                    else
                    {
                        logger.Error("This Complain is not Found int put " + a.NewsName);

                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error in put Complain " + ex.Message);
                }

            }
        }
        
        public IEnumerable<Coupon> GetAllCouopons()
        {
            //throw new NotImplementedException();
            if (CouponDb.AsEnumerable().Count() > 0)
            {
                List<Coupon> coupons = new List<Coupon>();
                coupons = CouponDb.ToList();
                coupons = CouponDb.Where(c => c.IsDeleted == false).ToList();
                return coupons.AsEnumerable();
            }
            else
            {
                List<Coupon> coupons = new List<Coupon>();
                return coupons.AsEnumerable();
            }
        }

        public Coupon GetCouponbyId(string id)
        {
            Coupon coupons = CouponDb.Where(c => c.OfferCode == id && c.IsDeleted == false).SingleOrDefault();
            if (coupons != null)
            {
                return coupons;
            }
            else
            {
                coupons = new Coupon();
            }
            return coupons;
        }

        public Coupon AddCoupon(Coupon coupon)
        {
            List<Coupon> couponList = CouponDb.Where(c => c.OfferId.Equals(coupon.OfferId)).ToList();
            if (couponList.Count == 0)
            {

                //coupon.StartDate = indianTime;
                //coupon.EndDate = indianTime;
                coupon.IsDeleted = false;
                CouponDb.Add(coupon);
                int id = this.SaveChanges();
                return coupon;
            }
            else
            {
                Coupon couponobj = new Coupon();
                return couponobj;
            }
        }

        public Coupon PutCoupon(Coupon obj)
        {
            //throw new NotImplementedException();
            logger.Info("put Categories: ");
            try
            {
                Coupon coupon = CouponDb.Where(x => x.OfferId == obj.OfferId).FirstOrDefault();
                if (coupon != null)
                {
                    coupon.OfferCode = obj.OfferCode;
                    coupon.Discount = obj.Discount;
                    coupon.IsDeleted = false;
                    coupon.MinAmount = obj.MinAmount;
                    coupon.StartDate = obj.StartDate;
                    coupon.EndDate = obj.EndDate;
                    coupon.OfferType = obj.OfferType;
                    coupon.OfferName = obj.OfferName;
                    coupon.Description = obj.Description;
                    coupon.DiscountType = obj.DiscountType;
                    coupon.SourceItemName = obj.SourceItemName;
                    coupon.FreeItemName = obj.FreeItemName;
                    CouponDb.Attach(coupon);
                    this.Entry(coupon).State = EntityState.Modified;
                    this.SaveChanges();
                    return obj;
                }
                else
                {
                    logger.Error("This coupon is not Found in put " + obj.OfferCode);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put coupon " + ex.Message);
            }
            return null;
        }

        public bool DeleteCoupon(int id)
        {
            try
            {
                Coupon coupon = CouponDb.Where(x => x.OfferId == id).SingleOrDefault();
                coupon.IsDeleted = true;
                //coupon.UpdatedDate = indianTime;
                CouponDb.Attach(coupon);
                //Entry(coupon).State = EntityState.Deleted;
                this.Entry(coupon).State = EntityState.Modified;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DailyEssential AddDailyItem(DailyEssential item)
        {
            try
            {
                DailyEssential M = DailyEssentialDb.Where(x => x.CustMobile == item.CustMobile && x.ItemId == item.ItemId).SingleOrDefault();
                if (M == null)
                {
                    item.CreatedDate = indianTime;
                    item.UpdatedDate = indianTime;

                    DailyEssentialDb.Add(item);
                    int id = this.SaveChanges();
                    return item;
                }
                else {
                    return null;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }
        public DailyItemEdit EditItem(DailyItemEdit item)
        {
            try
            {
                TimeSpan tsmin = new TimeSpan(00, 00, 0);
                TimeSpan tsmax = new TimeSpan(23, 59, 59);
                DateTime sd = item.EditDate.Date + tsmin;
                DateTime ed = item.EditDate.Date + tsmax;

                DailyEssential dailyitems = DailyEssentialDb.Where(x => x.CustMobile == item.CustMobile && x.ItemId == item.ItemId && x.EndDate > ed).SingleOrDefault();
                var d = indianTime.Date;
                var day = item.EditDate.DayOfWeek;
                if (dailyitems != null && d.AddDays(1) < dailyitems.EndDate.Date)
                {
                    DailyItemEdit DE = DailyItemCancelDb.Where(x => x.CustMobile == item.CustMobile && x.ItemId == item.ItemId && x.EditDate < ed && x.EditDate > sd).SingleOrDefault();

                    if (DE == null)
                    {
                        if (Convert.ToString(day) == "Monday")
                        {
                            if (dailyitems.Monday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Tuesday")
                        {
                            if (dailyitems.Tuesday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Wednesday")
                        {
                            if (dailyitems.Wednesday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Thursday")
                        {
                            if (dailyitems.Thursday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Friday")
                        {
                            if (dailyitems.Friday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Saturday")
                        {
                            if (dailyitems.Saturday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Sunday")
                        {
                            if (dailyitems.Sunday == true)
                            {
                                item.CreatedDate = indianTime;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Add(item);
                                int id = this.SaveChanges();
                                return item;
                            }
                        }
                    }
                    else {
                        ///////////////// else update
                        if (Convert.ToString(day) == "Monday")
                        {
                            if (dailyitems.Monday == true)
                            {
                                item.UpdatedDate = indianTime;
                                item.DailyItemCancelId = DE.DailyItemCancelId;
                                DailyItemCancelDb.Attach(item);
                                this.Entry(item).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Tuesday")
                        {
                            if (dailyitems.Tuesday == true)
                            {
                                DE.UpdatedDate = indianTime;
                                DE.Qty = item.Qty;
                                DailyItemCancelDb.Attach(DE);
                                this.Entry(DE).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Wednesday")
                        {
                            if (dailyitems.Wednesday == true)
                            {
                                item.DailyItemCancelId = DE.DailyItemCancelId;
                                item.UpdatedDate = indianTime;
                                DailyItemCancelDb.Attach(item);
                                this.Entry(item).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Thursday")
                        {
                            if (dailyitems.Thursday == true)
                            {
                                item.UpdatedDate = indianTime;
                                item.DailyItemCancelId = DE.DailyItemCancelId;
                                DailyItemCancelDb.Attach(item);
                                this.Entry(item).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Friday")
                        {
                            if (dailyitems.Friday == true)
                            {
                                item.UpdatedDate = indianTime;
                                item.DailyItemCancelId = DE.DailyItemCancelId;
                                DailyItemCancelDb.Attach(item);
                                this.Entry(item).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Saturday")
                        {
                            if (dailyitems.Saturday == true)
                            {
                                item.UpdatedDate = indianTime;
                                item.DailyItemCancelId = DE.DailyItemCancelId;
                                DailyItemCancelDb.Attach(item);
                                this.Entry(item).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                        else if (Convert.ToString(day) == "Sunday")
                        {
                            if (dailyitems.Sunday == true)
                            {
                                item.UpdatedDate = indianTime;
                                item.DailyItemCancelId = DE.DailyItemCancelId;
                                DailyItemCancelDb.Attach(item);
                                this.Entry(item).State = EntityState.Modified;
                                this.SaveChanges();
                                return item;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        public OrderMaster PutOrderMaster(OrderMaster objcity)
        {
            OrderMaster om = DbOrderMaster.Where(x => x.OrderId == objcity.OrderId && x.Deleted == false).FirstOrDefault();
            if (om != null)
            {
                //if (objcity.Status == "Process")
                //{
                //    Message msg1 = DbMessage.Where(x => x.MessageType == objcity.Status).FirstOrDefault();
                //    string smstemp1 = smstemplate1(om, msg1.MessageText);
                //    new Sms().sendOtp(om.Customerphonenum, smstemp1);
                //}
                //if (objcity.Status == "Dispatched")
                //{
                //    Message msg1 = DbMessage.Where(x => x.MessageType == objcity.Status).FirstOrDefault();
                //    string smstemp1 = smstemplate1(om, msg1.MessageText);
                //    new Sms().sendOtp(om.Customerphonenum, smstemp1);
                //}
                //if (objcity.Status == "Delivered")
                //{
                //    Message msg1 = DbMessage.Where(x => x.MessageType == objcity.Status).FirstOrDefault();
                //    string smstemp1 = smstemplate1(om, msg1.MessageText);
                //    new Sms().sendOtp(om.Customerphonenum, smstemp1);
                //}

                om.ReasonCancle = objcity.ReasonCancle;
                om.Status = objcity.Status;
                foreach (var od in om.orderDetails)
                {
                    var Odet = DbOrderDetails.Where(x => x.OrderDetailsId == od.OrderDetailsId).SingleOrDefault();
                    if (Odet != null)
                    {
                        Odet.Status = "Order Canceled";
                        Odet.UpdatedDate = indianTime;
                        DbOrderDetails.Attach(Odet);
                        this.Entry(Odet).State = EntityState.Modified;
                        this.SaveChanges();                        
                    }
                }
                DbOrderMaster.Attach(om);
                this.Entry(om).State = EntityState.Modified;
                this.SaveChanges();

                try
                {
                    var rpoint = RewardPointDb.Where(c => c.CustomerId == om.CustomerId).FirstOrDefault();
                    if (rpoint != null)
                    {
                        if (om.RewardPoint > 0)
                        {
                            rpoint.EarningPoint -= om.RewardPoint;
                            if (rpoint.EarningPoint < 0)
                                rpoint.EarningPoint = 0;
                            rpoint.UpdatedDate = indianTime;
                            rpoint.TransactionDate = indianTime;
                            RewardPointDb.Attach(rpoint);
                            this.Entry(rpoint).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
                catch (Exception ex) { }
                return objcity;
            }
            else
            {
                return objcity;
            }

        }
        
        private string smstemplate1(OrderMaster om, string text)
        {
            string bodytext = text;
            bodytext = bodytext.Replace("%CustomerName%", om.CustomerName);
            bodytext = bodytext.Replace("%OrderId%", om.invoice_no);
            bodytext = bodytext.Replace("%SubTotal%",om.TotalAmount.ToString()); 
            //bodytext = bodytext.Replace("%deliveryboy%", od.DeliveryBoyName);
            return bodytext;
        }
        
        public List<Favorites> AllFavorites(string mob)
        {
            List<Favorites> currentStock = new List<Favorites>();
            currentStock = Favoritess.Where(f => f.customerMobile == mob).ToList();
            return currentStock;
        }
        public Favorites AddFavorites(Favorites obj)
        {
            List<Favorites> itpr = new List<Favorites>();
            itpr = Favoritess.Where(i => i.customerMobile == obj.customerMobile && i.ItemId == obj.ItemId).ToList();
            if (itpr.Count == 0)
            {
                Favoritess.Add(obj);
                int id = this.SaveChanges();
            }
            else if (itpr.Count > 1)
            {
                for (var i= 0; i < (itpr.Count-1);i++ )
                {
                    Entry(itpr[i]).State = EntityState.Deleted;
                    SaveChanges();
                }
            }
            return obj;
        }
        public Feedback AddFeedBack(Feedback obj)
        {
            Customer customer = Customers.Where(c => c.Mobile == obj.Mobile && c.Deleted==false).FirstOrDefault();
            if (customer != null)
            {
                Feedback fdback = Feedbacks.Where(f => f.customerId == customer.CustomerId).SingleOrDefault();
                if (fdback == null)
                {
                    obj.customerId = customer.CustomerId;
                    obj.shopName = customer.ShopName;
                    obj.createdDate = indianTime;
                    Feedbacks.Add(obj);
                    this.SaveChanges();
                }
                else
                {
                    fdback.suggestions = obj.suggestions;
                    fdback.satisfactionLevel = obj.satisfactionLevel;
                    fdback.createdDate = indianTime;
                    Feedbacks.Attach(fdback);
                    this.Entry(fdback).State = EntityState.Modified;
                    this.SaveChanges();
                }
                return obj;
            }
            else
                return null;           
        }
        public RequestItem AddRequestItem(RequestItem obj)
        {
            Customer customer = Customers.Where(c => c.Mobile == obj.customerMobile && c.Deleted == false).FirstOrDefault();
            if (customer != null)
            {
                obj.customerId = customer.CustomerId;
                obj.shopName = customer.ShopName;
                obj.createdDate = indianTime;
                RequestItems.Add(obj);
                this.SaveChanges();
                return obj;
            }
            else
                return null;
        }
        public List<Customer> AddBulkcustomer(List<Customer> CustCollection)
        {
            logger.Info("start addbulk customer");
            try
            {
                foreach (var o in CustCollection)
                {
                    List<Customer> cust = Customers.Where(c => c.Skcode.Equals(o.Skcode) || c.Mobile == o.Mobile).ToList();
                    Warehouse warehouse = Warehouses.Where(x => x.WarehouseName == o.WarehouseName).FirstOrDefault();
                    Customer objitemMaster = new Customer();
                    if (cust.Count == 0)
                    {
                        o.CreatedDate = indianTime;
                        o.UpdatedDate = indianTime;
                        o.CreatedBy ="admin";
                        if (warehouse != null)
                        {
                            o.WarehouseName = warehouse.WarehouseName;
                            o.Warehouseid = warehouse.Warehouseid;
                        }
                        else
                        {
                            o.Warehouseid = 0;
                            o.WarehouseName = "No Warehouse";
                        }
                        var clstr = Clusters.Where(x => x.ClusterId == o.ClusterId).SingleOrDefault();
                        if (clstr != null)
                        {
                            o.ClusterId = clstr.ClusterId;
                            o.ClusterName = clstr.ClusterName;
                        }
                        else
                        {
                            Cluster fclstr = Clusters.FirstOrDefault();
                            o.ClusterId = fclstr.ClusterId;
                            o.ClusterName = fclstr.ClusterName;
                        }
                        if (o.Day == null)
                        {
                            o.Day = "";
                        }
                        Customers.Add(o);
                        int id = this.SaveChanges();

                    }
                    else
                    {
                        List<Customer> cust1 = Customers.Where(c => c.Skcode.ToLower().Equals(o.Skcode.ToLower()) && c.Mobile.Trim() == o.Mobile.Trim()).ToList();
                        if (cust1.Count() == 1)
                        {
                            logger.Info("Skcode already exists");
                            Customer editcust = cust1[0];
                            if (warehouse != null)
                            {
                                o.WarehouseName = warehouse.WarehouseName;
                                o.Warehouseid = warehouse.Warehouseid;
                            }
                            else
                            {
                                o.Warehouseid = 0;
                                o.WarehouseName = "No Warehouse";
                            }
                            editcust.lat = o.lat;
                            editcust.lg = o.lg;
                            editcust.ShopName = o.ShopName;
                            editcust.Emailid = o.Emailid;
                            editcust.Name = o.Name;
                            editcust.Mobile = o.Mobile;
                            editcust.BillingAddress = o.BillingAddress;
                            editcust.LandMark = o.LandMark;
                            editcust.ClusterId = o.ClusterId;
                            editcust.Day = o.Day;
                            editcust.BeatNumber = o.BeatNumber;
                            editcust.ExecutiveId = o.ExecutiveId;
                            editcust.City = o.City;
                            editcust.UpdatedDate = indianTime;
                            Customers.Attach(editcust);

                            this.Entry(editcust).State = EntityState.Modified;
                            this.SaveChanges();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info("error in adding customer collection"+ex.Message);
            }
            return null;
        }
        public List<People> AddBulkpeople(List<People> CustCollection)
        {
            logger.Info("start addbulk customer");
            try
            {
                foreach (var o in CustCollection)
                {
                    List<People> cust = Peoples.Where(c => c.Mobile.Equals(o.Mobile) && c.Deleted == false).ToList();

                    People objitemMaster = new People();
                    if (cust.Count == 0)
                    {
                        o.CreatedDate = indianTime;
                        o.UpdatedDate = indianTime;
                        Peoples.Add(o);
                        int id = this.SaveChanges();

                    }
                    else
                    {
                        logger.Info("Mobile number already exists");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Info("error in adding Sales Executive collection");
            }
            return null;
        }
        public List<Supplier> AddBulkSupplier(List<Supplier> supCollection)
        {
            logger.Info("start addbulk supplier");
            try
            {
                foreach (var o in supCollection)
                {
                    List<Supplier> cust = Suppliers.Where(c => c.SUPPLIERCODES.Equals(o.SUPPLIERCODES)).ToList();

                    Supplier objsupplier = new Supplier();
                    if (cust.Count == 0)
                    {
                        o.CreatedDate = indianTime;
                        o.UpdatedDate = indianTime;
                        Suppliers.Add(o);
                        int id = this.SaveChanges();

                    }
                    else
                    {
                        logger.Info("Skcode already exists");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Info("error in adding supplier collection");
            }
            return null;
        }

        public CurrentStock UpdateCurrentStock(CurrentStock stock,CurrentStock orignalStock)
        {
            this.Entry(orignalStock).CurrentValues.SetValues(stock);
            this.SaveChanges();
            return stock;
        }
        public CurrentStock UpdateCurrentStock(CurrentStock stock)
        {
            DbCurrentStock.Attach(stock);
            this.Entry(stock).State = EntityState.Modified;
            this.SaveChanges();
            return stock;
        }      
        public List<CurrentStock> Addcurrentstock(List<CurrentStock> cstkCollection)
        {
            logger.Info("start addbulk");
            try
            {
                foreach (var o in cstkCollection)
                {
                    List<CurrentStock> cst = DbCurrentStock.Where(c => c.ItemNumber.Equals(o.ItemNumber)).ToList();
                    CurrentStock objsupplier = new CurrentStock();
                    if (cst.Count == 0)
                    {
                        try
                        {
                            //Warehouse WH = Warehouses.Where(w => w.WarehouseName == o.WarehouseName && w.Deleted == false).FirstOrDefault();
                            //logger.Info("new items added in currentstock" + o.ItemName);
                            //o.WarehouseName = WH.WarehouseName;
                            //o.Warehouseid = WH.Warehouseid;
                            //o.CityId = WH.Cityid;
                            //o.CityName = WH.CityName;
                            //o.UpdatedDate = DateTime.Now;
                            //o.CreationDate = DateTime.Now;
                            //DbCurrentStock.Add(o);
                            //int id = this.SaveChanges();

                            //if (id > 0)
                            //{
                            //    var itemoldData = DbCurrentStock.Where(x => x.ItemNumber == o.ItemNumber).SingleOrDefault();
                            //    CurrentStockHistory Oss = new CurrentStockHistory();
                            //    Oss.ItemId = itemoldData.ItemId;
                            //    Oss.StockId = itemoldData.StockId;
                            //    Oss.ItemNumber = itemoldData.ItemNumber;
                            //    Oss.ItemName = itemoldData.ItemName;
                            //    Oss.TotalInventory = itemoldData.CurrentInventory;
                            //    Oss.WarehouseName = itemoldData.WarehouseName;
                            //    Oss.CreationDate = DateTime.Now;
                            //    CurrentStockHistoryDb.Add(Oss);
                            //    int idd = this.SaveChanges();

                            //}
                            //else { }

                        }
                        catch (Exception ex)
                        {
                            logger.Info("error in adding new current stock" + o.ItemName + ex);
                        }
                    }
                    else
                    {
                        try
                        {                            
                           
                            CurrentStock cstk = DbCurrentStock.Where(x => x.ItemNumber == o.ItemNumber).FirstOrDefault();
                            cstk.UpdatedDate = DateTime.Now;
                            cstk.CurrentInventory = o.CurrentInventory;
                            logger.Info("stock id already exists update it" + o.ItemName);
                            DbCurrentStock.Attach(cstk);
                            this.Entry(cstk).State = EntityState.Modified;
                            this.SaveChanges();

                           // var itemoldData = DbCurrentStock.Where(x => x.ItemNumber == o.ItemNumber).SingleOrDefault();
                            CurrentStockHistory Oss = new CurrentStockHistory();
                            if (cstk != null)
                            {
                                Oss.ItemId = cstk.ItemId;
                                Oss.StockId = cstk.StockId;
                                Oss.ItemNumber = cstk.ItemNumber;
                                Oss.ItemName = cstk.ItemName;
                                Oss.TotalInventory = cstk.CurrentInventory;
                                Oss.WarehouseName = cstk.WarehouseName;
                                Oss.CreationDate = DateTime.Now;
                                CurrentStockHistoryDb.Add(Oss);
                                int id = this.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Info("error in updating curntstok" + o.ItemName + ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info("error in adding currentstock collection" + ex);
            }
            return null;
        }
        public CurrentStock GetCurrentStock(int id)
        {
            CurrentStock ct = DbCurrentStock.Where(x => x.ItemId == id).SingleOrDefault();
            if (ct != null)
            {
                return ct;
            }

            return null;
        }

        public IEnumerable<CurrentStock> GetAllCurrentStock()
        {
            if (DbCurrentStock.AsEnumerable().Count() > 0)
            {
                return DbCurrentStock.AsEnumerable();
            }
            else
            {
                List<CurrentStock> currentStock = new List<CurrentStock>();
                return currentStock.AsEnumerable();
            }
        }
        public IEnumerable<PurchaseOrderDetail> AllPOrderDetails(int i)
        {
            return DPurchaseOrderDeatil.Where(c => c.PurchaseOrderId == i).AsEnumerable();
        }      

        public PurchaseOrderMaster AllPOrderDetails1(int i)
        {
            PurchaseOrderMaster pm =DPurchaseOrderMaster.Where(c => c.PurchaseOrderId == i).SingleOrDefault();
            pm.purDetails = PurchaseOrderRecivedDetails.Where(c => c.PurchaseOrderId == i).ToList();
            if (pm.purDetails != null)
            {
                foreach (var a in pm.purDetails)
                {
                    var item = itemMasters.Where(c => c.ItemId == a.ItemId).SingleOrDefault();
                    if (item != null)
                    {
                        a.TotalTaxPercentage = item.TotalTaxPercentage;
                    }
                }
            }
            return pm;
        }
        public IEnumerable<PurchaseOrderMaster> AllPOMaster()
        {
            if (DPurchaseOrderMaster.AsEnumerable().Count() > 0)
            {
                return DPurchaseOrderMaster.AsEnumerable();
            }
            else
            {
                List<PurchaseOrderMaster> poMasters = new List<PurchaseOrderMaster>();
                return poMasters.AsEnumerable();
            }
        }

        public IEnumerable<PurchaseOrderDetail> AllPOdetails(int compid)
        {
            if (DPurchaseOrderDeatil.AsEnumerable().Count() > 0)
            {
                return DPurchaseOrderDeatil.AsEnumerable();
            }
            else
            {
                List<PurchaseOrderDetail> purchaseOrderDetail = new List<PurchaseOrderDetail>();
                return purchaseOrderDetail.AsEnumerable();
            }
        }
        public TaxGroupDetails AddTaxGRPDetail(TaxGroupDetails e)
        {
            try
            {
                TaxMaster TaxMasters = DbTaxMaster.Where(x => x.TaxID == e.TaxID && x.Deleted == false).FirstOrDefault();
                e.TaxName = TaxMasters.TaxName;
                e.TPercent = TaxMasters.TPercent;
                DbTaxGroupDetails.Add(e);
                int id = this.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return e;
        }
        public IEnumerable<PurchaseOrder> AllPurchaseOrder(int compid)
        {

            if (DbPurchaseOrder.AsEnumerable().Count() > 0)
            {
                return DbPurchaseOrder.Where(x => x.Status == "Pending").AsEnumerable();
            }
            else

            {
                List<PurchaseOrder> purchaseorder = new List<PurchaseOrder>();
                return purchaseorder.Where(x => x.Status == "Pending").AsEnumerable();
            }

        }
        
        public List<PurchaseOrderList> AddPurchaseOrder(List<PurchaseOrderList> poList)
        {
            List<PurchaseOrderList> newpoList = new List<PurchaseOrderList>();
            var suplist = poList.Select(o => o.SupplierId).Distinct().ToList();
            foreach (var sup in suplist)
            {
                newpoList = null;
                int supid = Convert.ToInt32(sup);
                newpoList = poList.Where(s => s.SupplierId == supid).ToList();
                var diffwarehouslist = newpoList.Select(w => w.Warehouseid).Distinct().ToList();
                foreach (var diffwar in diffwarehouslist)
                {
                    newpoList = null;
                    PurchaseOrderMaster pom = new PurchaseOrderMaster();
                    pom.Warehouseid = Convert.ToInt32(diffwar);
                    newpoList = poList.Where(s => s.SupplierId == supid && s.Warehouseid == pom.Warehouseid && s.requiredqty > 0).ToList();
                    Supplier suplier = Suppliers.Where(x => x.SupplierId == supid && x.Deleted == false).SingleOrDefault();
                    Warehouse whobj = Warehouses.Where(x => x.Warehouseid == pom.Warehouseid && x.Deleted == false).SingleOrDefault();

                    if (newpoList.Count > 0)
                    {
                        pom.SupplierId = Convert.ToInt32(suplier.SupplierId);
                        pom.SupplierName = suplier.Name;
                        pom.WarehouseName = whobj.WarehouseName;
                        pom.Status = "Success";
                        pom.CreationDate = indianTime;
                        pom.CreatedBy = "Accounted";
                        pom.Acitve = true;
                        DPurchaseOrderMaster.Add(pom);
                        int id = this.SaveChanges();
                        int po_id = pom.PurchaseOrderId;
                        foreach (var i in newpoList)
                        {
                            CurrentStock cs = DbCurrentStock.Where(x => x.Warehouseid == pom.Warehouseid).Where(x => x.ItemNumber == i.SKUCode).Select(x => x).SingleOrDefault();

                            if (cs == null)
                            {
                                ItemMaster itemmaster = itemMasters.Where(x => x.ItemId == Convert.ToInt32(i.ItemId)).SingleOrDefault();

                                CurrentStock cstock = new CurrentStock();
                                cstock.CityId = whobj.Cityid;
                                cstock.CityName = whobj.CityName;

                                cstock.CreatedBy = "stock keeper";
                                cstock.CreationDate = indianTime;
                                cstock.CurrentInventory = 0;
                                cstock.Deleted = false;
                                cstock.ItemId = Convert.ToInt32(i.ItemId);
                                cstock.ItemName = i.ItemName;
                                cstock.UpdatedDate = indianTime;
                                cstock.ItemNumber = itemmaster.Number;
                                cstock.ItemName = itemmaster.PurchaseUnitName;
                                cstock.UpdateBy = "stock keeper";
                                cstock.Warehouseid = Convert.ToInt32(pom.Warehouseid);
                                cstock.WarehouseName = whobj.WarehouseName;

                                DbCurrentStock.Add(cstock);
                                int stockid = this.SaveChanges();

                            }
                            CurrentStock currentstock = DbCurrentStock.Where(x => x.Warehouseid == pom.Warehouseid).Where(x => x.ItemNumber == i.SKUCode).Select(x => x).SingleOrDefault();
                            ItemMaster item = itemMasters.Where(x => x.ItemId == i.ItemId).Select(x => x).SingleOrDefault();
                            int ci = currentstock.CurrentInventory;

                            PurchaseOrderDetail pod = new PurchaseOrderDetail();
                            pod.ItemId = Convert.ToInt32(i.ItemId);
                            pod.ItemName = i.ItemName;
                            pod.PurchaseOrderId = po_id;
                            pod.Price = Convert.ToDouble(i.Price);
                            pod.SKUCode = i.SKUCode;

                            pod.SupplierId = Convert.ToInt32(i.SupplierId);
                            pod.SupplierName = i.SupplierName;
                            pod.Warehouseid = pom.Warehouseid;
                            pod.WarehouseName = whobj.WarehouseName;


                            pod.TaxAmount = i.TaxAmount;
                            pod.TotalAmountIncTax = i.TotalAmountIncTax;
                            pod.TotalQuantity = i.NetPurchaseQty;
                            pod.MOQ = i.PurchaseMinOrderQty;
                            pod.Status = "New";
                            pod.CreationDate = indianTime;
                            pod.CreatedBy = "Store Keeper";
                            DPurchaseOrderDeatil.Add(pod);
                            int PucrchaseOrderDetailId = this.SaveChanges();

                            //Update order detail table
                            List<OrderDetails> order = DbOrderDetails.Where(x => x.ItemId == i.ItemId).Where(x => x.Warehouseid == pom.Warehouseid).Where(x => x.Status == "Pending").Select(x => x).ToList();
                            // var diffwarehouslist = newpoList.Select(w => w.Warehouseid).Distinct().ToList();
                            var ordermasterid = order.Select(o => o.OrderId).Distinct().ToList();

                            foreach (var o in order)
                            {
                                o.Status = "Approved";

                                DbOrderDetails.Attach(o);
                                this.Entry(o).State = EntityState.Modified;
                                this.SaveChanges();

                                OrderMaster ord = DbOrderMaster.Where(x => x.OrderId == o.OrderId).Where(x => x.Status == "Pending" || x.Status == "Partially").SingleOrDefault();
                                //by moin
                                OrderDetails ordtail = DbOrderDetails.Where(y => y.OrderId == o.OrderId).Where(y => y.Status == "Pending").FirstOrDefault();
                                if (ord != null && ordtail == null)
                                {
                                    ord.Status = "Approved";
                                    DbOrderMaster.Attach(ord);
                                    this.Entry(ord).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                                else if (ord != null && ordtail != null)
                                {
                                    ord.Status = "Partially";
                                    DbOrderMaster.Attach(ord);
                                    this.Entry(ord).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                            }

                            //UPDATE STATUS IN sTOCK..

                            currentstock.CurrentInventory = ((i.NetPurchaseQty * i.PurchaseMinOrderQty) - i.requiredqty);
                            this.Entry(currentstock).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                    else
                    {
                        // if Required Quantity is 0;
                        newpoList = poList.Where(s => s.SupplierId == supid && s.Warehouseid == pom.Warehouseid).ToList();
                        foreach (var i in newpoList)
                        {
                            List<OrderDetails> order = DbOrderDetails.Where(x => x.ItemId == i.ItemId).Where(x => x.Warehouseid == pom.Warehouseid).Where(x => x.Status == "Pending").Select(x => x).ToList();
                            foreach (var o in order)
                            {
                                o.Status = "Approved";
                                DbOrderDetails.Attach(o);
                                this.Entry(o).State = EntityState.Modified;
                                this.SaveChanges();
                                OrderMaster ord = DbOrderMaster.Where(x => x.OrderId == o.OrderId).Where(x => x.Status == "Pending").SingleOrDefault();
                                if (ord != null)
                                {
                                    ord.Status = "Approved";
                                    DbOrderMaster.Attach(ord);
                                    this.Entry(ord).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                            }
                            CurrentStock currentstock = DbCurrentStock.Where(x => x.ItemNumber == i.SKUCode).Where(x => x.Warehouseid == pom.Warehouseid).SingleOrDefault();
                            currentstock.CurrentInventory = ((i.NetPurchaseQty * i.PurchaseMinOrderQty) - i.requiredqty);
                            this.Entry(currentstock).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
            }
            return poList;
        }

        public PurchaseOrder AddPurchaseItem(PurchaseOrder poItem)
        {
            if (poItem != null)
            {
                try
                {
                    poItem.Status = "Pending";
                    poItem.CreationDate = indianTime;
                    poItem.UpdatedDate = indianTime;
                    poItem.TotalQuantity = Convert.ToInt32(poItem.finalqty * poItem.ConversionFactor);
                    poItem.TotalAmountIncTax = poItem.TotalQuantity * poItem.Price;
                    DbPurchaseOrder.Add(poItem);
                    this.SaveChanges();
                }
                catch (Exception ee)
                {
                    logger.Error(ee.Message);
                    poItem = null;
                }
                return poItem;
            }
            else
            {
                return null;
            }           
        }

        /// <summary>
        /// sUMIT
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public IEnumerable<PurchaseOrder> PurchaseOrderbyDemand(int compid)
        {

            if (DbPurchaseOrder.AsEnumerable().Count() > 0)
            {
                return DbPurchaseOrder.AsEnumerable();
            }
            else

            {
                List<PurchaseOrder> purchaseorder = new List<PurchaseOrder>();
                return purchaseorder.AsEnumerable();
            }

        }
        
        public IEnumerable<TaxGroupDetails> AlltaxgroupDetails(int i)
        {
            return DbTaxGroupDetails.Where(c => c.GruopID == i).AsEnumerable();

        }
        
        public IEnumerable<TaxMaster> AllTaxMaster(int compid)
        {

            if (DbTaxMaster.AsEnumerable().Count() > 0)
            {
                return DbTaxMaster.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else

            {
                List<TaxMaster> state = new List<TaxMaster>();
                return state.AsEnumerable();
            }

        }
        public TaxMaster AddTaxMaster(TaxMaster taxMaster)
        {
            List<TaxMaster> taxmasters = DbTaxMaster.Where(c => c.TaxName.Trim().Equals(taxMaster.TaxName.Trim()) && c.Deleted == false).ToList();
            TaxMaster objTaxMaster = new TaxMaster();
            if (taxmasters.Count == 0)
            {

                taxMaster.CreatedDate = indianTime;
                taxMaster.UpdatedDate = indianTime;
                DbTaxMaster.Add(taxMaster);
                int id = this.SaveChanges();
                return taxMaster;
            }
            else
            {
                //objProject.Exception = "Already";
                return objTaxMaster;
            }
        }
        public TaxMaster PutTaxMaster(TaxMaster objTaxMaster)
        {

            TaxMaster TaxMasters = DbTaxMaster.Where(x => x.TaxID == objTaxMaster.TaxID && x.Deleted == false).FirstOrDefault();
            if (TaxMasters != null)
            {
                TaxMasters.UpdatedDate = indianTime;
                TaxMasters.TaxName = objTaxMaster.TaxName;
                TaxMasters.TAlias = objTaxMaster.TAlias;

                TaxMasters.TDiscription = objTaxMaster.TDiscription;
                TaxMasters.CreatedDate = objTaxMaster.CreatedDate;
                TaxMasters.TPercent = objTaxMaster.TPercent;


                DbTaxMaster.Attach(TaxMasters);
                this.Entry(TaxMasters).State = EntityState.Modified;
                this.SaveChanges();
                return objTaxMaster;
            }
            else
            {
                return objTaxMaster;
            }
        }
        public bool DeleteTaxMaster(int id)
        {
            try
            {
                TaxMaster TaxMasters = DbTaxMaster.Where(x => x.TaxID == id && x.Deleted == false).FirstOrDefault();
                TaxMasters.Deleted = true;
                DbTaxMaster.Attach(TaxMasters);
                this.Entry(TaxMasters).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<TaxGroup> AllTaxGroup(int compid)
        {

            if (DbTaxGroup.AsEnumerable().Count() > 0)
            {
                return DbTaxGroup.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else

            {
                List<TaxGroup> taxgrp = new List<TaxGroup>();
                return taxgrp.AsEnumerable();
            }

        }
        public TaxGroup AddTaxGroup(TaxGroup taxGroup)
        {
            List<TaxGroup> TaxGroups = DbTaxGroup.Where(c => c.TGrpName.Trim().Equals(taxGroup.TGrpName.Trim()) && c.Deleted == false).ToList();
            TaxGroup objTaxgrp = new TaxGroup();
            if (TaxGroups.Count == 0)
            {

                taxGroup.CreatedDate = indianTime;
                taxGroup.UpdatedDate = indianTime;
                DbTaxGroup.Add(taxGroup);
                int id = this.SaveChanges();
                return taxGroup;
            }
            else
            {
                return objTaxgrp;
            }
        }
        public TaxGroup PutTaxGroup(TaxGroup objTaxGroup)
        {

            TaxGroup TaxGroups = DbTaxGroup.Where(x => x.GruopID == objTaxGroup.GruopID && x.Deleted == false).FirstOrDefault();
            if (TaxGroups != null)
            {
                TaxGroups.UpdatedDate = indianTime;
                TaxGroups.TGrpName = objTaxGroup.TGrpName;
                TaxGroups.TGrpAlias = objTaxGroup.TGrpAlias;
                TaxGroups.TGrpDiscription = objTaxGroup.TGrpDiscription;
                TaxGroups.CreatedDate = objTaxGroup.CreatedDate;

                // TaxGroups.TPercent = objTaxGroup.TPercent;

                DbTaxGroup.Attach(TaxGroups);
                this.Entry(TaxGroups).State = EntityState.Modified;
                this.SaveChanges();
                return objTaxGroup;
            }
            else
            {
                return objTaxGroup;
            }
        }
        public bool DeleteTaxGroup(int id)
        {
            try
            {
                TaxGroup TaxGroups = DbTaxGroup.Where(x => x.GruopID == id && x.Deleted == false).FirstOrDefault();
                TaxGroups.Deleted = true;
                DbTaxGroup.Attach(TaxGroups);
                this.Entry(TaxGroups).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public WarehouseSupplier AddWarehouseSupplierExcel(WarehouseSupplier warehouseSupplier)
        {
            List<WarehouseSupplier> WhSupplier = DbWarehouseSupplier.Where(c => c.Whsupid.Equals(warehouseSupplier.Whsupid)).ToList();

            WarehouseSupplier objWarehouseSupplier = new WarehouseSupplier();
            if (WhSupplier.Count == 0)
            {
                warehouseSupplier.CreatedDate = indianTime;
                warehouseSupplier.Deleted = false;
                DbWarehouseSupplier.Add(warehouseSupplier);

                int id = this.SaveChanges();
                return warehouseSupplier;
            }
            else
            {
                return objWarehouseSupplier;
            }
        }
        public WarehouseSupplier PutWarehouseSupplierExcel(WarehouseSupplier objWarehouseSupplier)
        {
            WarehouseSupplier warehouseSupplier = DbWarehouseSupplier.Where(x => x.Whsupid == objWarehouseSupplier.Whsupid).FirstOrDefault();
            City city = Cities.Where(x => x.Cityid == objWarehouseSupplier.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State St = States.Where(x => x.Stateid == objWarehouseSupplier.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();
            Supplier supplier = Suppliers.Where(s => s.SupplierId == objWarehouseSupplier.SupplierId && s.Deleted == false).Select(s => s).FirstOrDefault();
            Warehouse warehouse = Warehouses.Where(w => w.Warehouseid == objWarehouseSupplier.Warehouseid && w.Deleted == false).Select(w => w).FirstOrDefault();

            if (warehouseSupplier != null)
            {
                warehouseSupplier.SupplierName = supplier.Name;
                warehouseSupplier.SupplierId = objWarehouseSupplier.SupplierId;
                warehouseSupplier.WarehouseName = warehouse.WarehouseName;
                warehouseSupplier.Warehouseid = objWarehouseSupplier.Warehouseid;

                warehouseSupplier.Stateid = objWarehouseSupplier.Stateid;
                warehouseSupplier.Cityid = objWarehouseSupplier.Cityid;
                warehouseSupplier.CityName = city.CityName;
                warehouseSupplier.Stateid = St.Stateid;
                warehouseSupplier.StateName = St.StateName;
                warehouseSupplier.CompanyId = 1;
                warehouseSupplier.CreatedDate = indianTime;
                warehouseSupplier.Deleted = false;
                warehouseSupplier.Active = true;
                warehouseSupplier.CreatedDate = objWarehouseSupplier.CreatedDate;


                DbWarehouseSupplier.Attach(warehouseSupplier);
                this.Entry(warehouseSupplier).State = EntityState.Modified;
                this.SaveChanges();
                return warehouseSupplier;
            }
            else
            {
                return objWarehouseSupplier;
            }
        }
        public IEnumerable<WarehouseSupplier> AllWarehouseSupplier(int compid)
        {
            if (Warehouses.AsEnumerable().Count() > 0)
            {
                return DbWarehouseSupplier.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<WarehouseSupplier> warehouseSupplier = new List<WarehouseSupplier>();
                return warehouseSupplier.AsEnumerable();
            }
        }

        public WarehouseSupplier AddWarehouseSupplier(WarehouseSupplier warehouseSupplier)
        {
            List<WarehouseSupplier> warehouses = DbWarehouseSupplier.Where(c => c.Warehouseid.Equals(warehouseSupplier.Warehouseid)).ToList();

            City city = Cities.Where(x => x.Cityid == warehouseSupplier.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State St = States.Where(x => x.Stateid == warehouseSupplier.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();
            Supplier supplier = Suppliers.Where(s => s.SupplierId == warehouseSupplier.SupplierId && s.Deleted == false).Select(s => s).FirstOrDefault();
            Warehouse warehouse = Warehouses.Where(w => w.Warehouseid == warehouseSupplier.Warehouseid && w.Deleted == false).Select(w => w).FirstOrDefault();

            WarehouseSupplier objWarehouseSupplier = new WarehouseSupplier();

            if (warehouses.Count == 0)
            {

                warehouseSupplier.CreatedDate = indianTime;
                warehouseSupplier.CityName = city.CityName;
                warehouseSupplier.StateName = St.StateName;
                warehouseSupplier.SupplierName = supplier.Name;
                warehouseSupplier.WarehouseName = warehouse.WarehouseName;
                DbWarehouseSupplier.Add(warehouseSupplier);
                int id = this.SaveChanges();
                return warehouseSupplier;
            }
            else
            {
                return objWarehouseSupplier;
            }
        }

        public WarehouseSupplier PutWarehouseSupplier(WarehouseSupplier objWarehouseSupplier)
        {
            WarehouseSupplier warehouseSupplier = DbWarehouseSupplier.Where(x => x.Whsupid == objWarehouseSupplier.Whsupid).FirstOrDefault();
            City city = Cities.Where(x => x.Cityid == objWarehouseSupplier.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State St = States.Where(x => x.Stateid == objWarehouseSupplier.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();
            Supplier supplier = Suppliers.Where(s => s.SupplierId == objWarehouseSupplier.SupplierId && s.Deleted == false).Select(s => s).FirstOrDefault();
            Warehouse warehouse = Warehouses.Where(w => w.Warehouseid == objWarehouseSupplier.Warehouseid && w.Deleted == false).Select(w => w).FirstOrDefault();

            if (warehouseSupplier != null)
            {
                warehouseSupplier.SupplierName = supplier.Name;
                warehouseSupplier.SupplierId = objWarehouseSupplier.SupplierId;
                warehouseSupplier.WarehouseName = warehouse.WarehouseName;
                warehouseSupplier.Warehouseid = objWarehouseSupplier.Warehouseid;

                warehouseSupplier.Stateid = objWarehouseSupplier.Stateid;
                warehouseSupplier.Cityid = objWarehouseSupplier.Cityid;
                warehouseSupplier.CityName = city.CityName;
                warehouseSupplier.Stateid = St.Stateid;
                warehouseSupplier.StateName = St.StateName;
                warehouseSupplier.CompanyId = 1;
                warehouseSupplier.CreatedDate = indianTime;
                warehouseSupplier.Deleted = false;
                warehouseSupplier.Active = true;
                warehouseSupplier.CreatedDate = objWarehouseSupplier.CreatedDate;


                DbWarehouseSupplier.Attach(warehouseSupplier);
                this.Entry(warehouseSupplier).State = EntityState.Modified;
                this.SaveChanges();
                return warehouseSupplier;
            }
            else
            {
                return objWarehouseSupplier;
            }
        }
        public bool DeleteWarehouseSupplier(int id)
        {
            try
            {
                WarehouseSupplier DL = new WarehouseSupplier();
                DL.Whsupid = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
                
        public IEnumerable<OrderDetails> AllOrderDetails(int i)
        {
            return DbOrderDetails.Where(c => c.OrderId == i).AsEnumerable();

        }

        public IEnumerable<DamageOrderDetails> AllDOrderDetails(int i)
        {
            return DamageOrderDetailsDB.Where(c => c.DamageOrderId == i).AsEnumerable();

        }

        public IList<DemandDetailsNew> AllfilteredOrderDetails(string Cityid, string Warehouseid, DateTime datefrom, DateTime dateto)
        {
            if (Cityid == "undefined" || Cityid == null || Cityid == "0")
            {
                if (Warehouseid == "undefined" || Warehouseid == null || Warehouseid == "0")
                {
                    var filteredlist = (from od in DbOrderDetails.Where(x => x.Status == "Pending")
                                        join i in itemMasters on od.ItemId equals i.ItemId
                                        where od.CreatedDate > datefrom && od.CreatedDate <= dateto
                                        select new DemandDetailsNew
                                        {
                                            ItemId = od.ItemId,
                                            ItemCode = i.itemcode,
                                            MinOrderQty = i.PurchaseMinOrderQty,
                                            qty = od.qty,
                                            City = od.City,
                                            CityId = od.CityId,
                                            Warehouseid = od.Warehouseid,
                                            CreatedDate = od.CreatedDate,
                                            status = od.Status
                                        }).ToList();
                    return filteredlist;
                }
                else
                {

                    int warehouseid = Convert.ToInt32(Warehouseid.Trim());
                    var filteredlist = (from od in DbOrderDetails.Where(x => x.Status == "Pending")
                                        join i in itemMasters on od.ItemId equals i.ItemId
                                        where od.Warehouseid == warehouseid && od.CreatedDate > datefrom && od.CreatedDate <= dateto
                                        select new DemandDetailsNew
                                        {
                                            ItemId = od.ItemId,
                                            itemname = od.itemname,
                                            ItemCode = i.itemcode,
                                            MinOrderQty = i.PurchaseMinOrderQty,
                                            qty = od.qty,
                                            City = od.City,
                                            CityId = od.CityId,
                                            Warehouseid = od.Warehouseid,
                                            CreatedDate = od.CreatedDate,
                                            status = od.Status
                                        }).ToList();
                    return filteredlist;
                }
            }
            else
            {
                if (Warehouseid == "undefined" || Warehouseid == null || Warehouseid == "0")
                {
                    int cityid = Convert.ToInt32(Cityid.Trim());

                    var filteredlist = (from od in DbOrderDetails.Where(x => x.Status == "Pending")
                                        join i in itemMasters on od.ItemId equals i.ItemId
                                        where od.CityId == cityid && od.CreatedDate > datefrom && od.CreatedDate <= dateto
                                        select new DemandDetailsNew
                                        {
                                            ItemId = od.ItemId,
                                            itemname = od.itemname,
                                            ItemCode = i.itemcode,
                                            MinOrderQty = i.PurchaseMinOrderQty,
                                            qty = od.qty,
                                            City = od.City,
                                            CityId = od.CityId,
                                            Warehouseid = od.Warehouseid,
                                            CreatedDate = od.CreatedDate,
                                            status = od.Status

                                        }).ToList();
                    return filteredlist;
                }
                else
                {
                    int cityid = Convert.ToInt32(Cityid.Trim());
                    int warehouseid = Convert.ToInt32(Warehouseid.Trim());
                    var filteredlist = (from od in DbOrderDetails.Where(x => x.Status == "Pending")
                                        join i in itemMasters on od.ItemId equals i.ItemId
                                        where od.CityId == cityid && od.Warehouseid == warehouseid && od.CreatedDate > datefrom && od.CreatedDate <= dateto
                                        select new DemandDetailsNew
                                        {
                                            ItemId = od.ItemId,
                                            itemname = od.itemname,
                                            ItemCode = i.itemcode,
                                            MinOrderQty = i.PurchaseMinOrderQty,
                                            qty = od.qty,
                                            City = od.City,
                                            CityId = od.CityId,
                                            Warehouseid = od.Warehouseid,
                                            CreatedDate = od.CreatedDate,
                                            status = od.Status

                                        }).ToList();
                    return filteredlist;
                }
            }
        }

        public IList<PurchaseOrderList> AllfilteredOrderDetails2(string Cityid, string Warehouseid, DateTime? datefrom, DateTime? dateto)
        {
            if (datefrom == null && dateto == null && Warehouseid == "undefined" || Warehouseid == null || Warehouseid == "0")
            {
                int cid = Convert.ToInt32(Cityid);
                var poList = (from a in DbOrderDetails
                              where a.Status == "Pending" && a.Deleted == false
                              join i in itemMasters on a.ItemId equals i.ItemId
                              join c in DbCurrentStock on i.ItemId equals c.ItemId into ps
                              from c in ps.DefaultIfEmpty()
                              where a.CityId == cid
                              select new PurchaseOrderList
                              {
                                  OrderDetailsId = a.OrderDetailsId,
                                  Warehouseid = a.Warehouseid,
                                  Cityid = a.CityId,
                                  WarehouseName = a.WarehouseName,
                                  OrderDate = a.OrderDate,
                                  SupplierId = i.SupplierId,
                                  SupplierName = i.SupplierName,
                                  OrderId = a.OrderId,
                                  ItemId = a.ItemId,
                                  SKUCode = i.itemcode,
                                  ItemName = i.itemname,
                                  //Unit = i.itemname,
                                  Discription = "",
                                  qty = a.qty,
                                  CurrentInventory = c == null ? 0 : c.CurrentInventory,
                                  Price = i.PurchasePrice,
                                  NetAmmount = a.NetAmmount,
                                  TaxPercentage = a.TaxPercentage,
                                  TaxAmount = a.TaxAmmount,
                                  TotalAmountIncTax = a.TotalAmt,
                                  PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                  Status = a.Status,
                                  CreationDate = a.CreatedDate,
                                  Deleted = a.Deleted
                              }).ToList();
                return poList;
            }
            else if (Warehouseid != null && datefrom == null && dateto == null)
            {
                int wid = Convert.ToInt32(Warehouseid);
                int cityid = Convert.ToInt32(Cityid.Trim());
                var poList = (from a in DbOrderDetails
                              where a.Status == "Pending" && a.Deleted == false
                              join i in itemMasters on a.ItemId equals i.ItemId
                              join c in DbCurrentStock on i.ItemId equals c.ItemId into ps
                              from c in ps.DefaultIfEmpty()
                              where a.CityId == cityid && a.Warehouseid == wid
                              select new PurchaseOrderList
                              {
                                  OrderDetailsId = a.OrderDetailsId,
                                  Warehouseid = a.Warehouseid,
                                  Cityid = a.CityId,
                                  WarehouseName = a.WarehouseName,
                                  OrderDate = a.OrderDate,
                                  SupplierId = i.SupplierId,
                                  SupplierName = i.SupplierName,
                                  OrderId = a.OrderId,
                                  ItemId = a.ItemId,
                                  SKUCode = i.itemcode,
                                  ItemName = i.itemname,
                                  //Unit = i.UnitName,
                                  Discription = "",
                                  qty = a.qty,
                                  CurrentInventory = c == null ? 0 : c.CurrentInventory,
                                  Price = i.PurchasePrice,
                                  NetAmmount = a.NetAmmount,
                                  TaxPercentage = a.TaxPercentage,
                                  TaxAmount = a.TaxAmmount,
                                  TotalAmountIncTax = a.TotalAmt,
                                  PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                  Status = a.Status,
                                  CreationDate = a.CreatedDate,
                                  Deleted = a.Deleted
                              }).ToList();
                return poList;
            }
            else if (Warehouseid != null && datefrom != null && dateto != null)
            {
                int cityid = Convert.ToInt32(Cityid.Trim());
                int warehouseid = Convert.ToInt32(Warehouseid.Trim());
                var poList = (from a in DbOrderDetails
                              where a.Status == "Pending" && a.Deleted == false
                              join i in itemMasters on a.ItemId equals i.ItemId
                              join c in DbCurrentStock on i.ItemId equals c.ItemId into ps
                              from c in ps.DefaultIfEmpty()
                              where a.CityId == cityid && a.Warehouseid == warehouseid && a.CreatedDate > datefrom && a.CreatedDate <= dateto
                              select new PurchaseOrderList
                              {
                                  OrderDetailsId = a.OrderDetailsId,
                                  Warehouseid = a.Warehouseid,
                                  Cityid = a.CityId,
                                  WarehouseName = a.WarehouseName,
                                  OrderDate = a.OrderDate,
                                  SupplierId = i.SupplierId,
                                  SupplierName = i.SupplierName,
                                  OrderId = a.OrderId,
                                  ItemId = a.ItemId,
                                  SKUCode = i.itemcode,
                                  ItemName = i.itemname,
                                  //Unit = i.UnitName,
                                  Discription = "",
                                  qty = a.qty,
                                  CurrentInventory = c == null ? 0 : c.CurrentInventory,
                                  Price = i.PurchasePrice,
                                  NetAmmount = a.NetAmmount,
                                  TaxPercentage = a.TaxPercentage,
                                  TaxAmount = a.TaxAmmount,
                                  TotalAmountIncTax = a.TotalAmt,
                                  PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                  Status = a.Status,
                                  CreationDate = a.CreatedDate,
                                  Deleted = a.Deleted
                              }).ToList();
                return poList;
            }
            else if (Warehouseid == null && datefrom != null && dateto != null)
            {
                int cityid = Convert.ToInt32(Cityid.Trim());
                int warehouseid = Convert.ToInt32(Warehouseid.Trim());
                var poList = (from a in DbOrderDetails
                              where a.Status == "Pending" && a.Deleted == false
                              join i in itemMasters on a.ItemId equals i.ItemId
                              join c in DbCurrentStock on i.ItemId equals c.ItemId into ps
                              from c in ps.DefaultIfEmpty()
                              where a.CityId == cityid && a.CreatedDate > datefrom && a.CreatedDate <= dateto
                              select new PurchaseOrderList
                              {
                                  OrderDetailsId = a.OrderDetailsId,
                                  Warehouseid = a.Warehouseid,
                                  Cityid = a.CityId,
                                  WarehouseName = a.WarehouseName,
                                  OrderDate = a.OrderDate,
                                  SupplierId = i.SupplierId,
                                  SupplierName = i.SupplierName,
                                  OrderId = a.OrderId,
                                  ItemId = a.ItemId,
                                  SKUCode = i.itemcode,
                                  ItemName = i.itemname,
                                  //Unit = i.UnitName,
                                  Discription = "",
                                  qty = a.qty,
                                  CurrentInventory = c == null ? 0 : c.CurrentInventory,
                                  Price = i.PurchasePrice,
                                  NetAmmount = a.NetAmmount,
                                  TaxPercentage = a.TaxPercentage,
                                  TaxAmount = a.TaxAmmount,
                                  TotalAmountIncTax = a.TotalAmt,
                                  PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                  Status = a.Status,
                                  CreationDate = a.CreatedDate,
                                  Deleted = a.Deleted
                              }).ToList();
                return poList;
            }
            else
            {
                int cityid = Convert.ToInt32(Cityid.Trim());
                int warehouseid = Convert.ToInt32(Warehouseid.Trim());
                var poList = (from a in DbOrderDetails
                              where a.Status == "Pending" && a.Deleted == false
                              join i in itemMasters on a.ItemId equals i.ItemId
                              join c in DbCurrentStock on i.ItemId equals c.ItemId into ps
                              from c in ps.DefaultIfEmpty()

                              select new PurchaseOrderList
                              {
                                  OrderDetailsId = a.OrderDetailsId,
                                  Warehouseid = a.Warehouseid,
                                  Cityid = a.CityId,
                                  WarehouseName = a.WarehouseName,
                                  OrderDate = a.OrderDate,
                                  SupplierId = i.SupplierId,
                                  SupplierName = i.SupplierName,
                                  OrderId = a.OrderId,
                                  ItemId = a.ItemId,
                                  SKUCode = i.itemcode,
                                  ItemName = i.itemname,
                                  //Unit = i.UnitName,
                                  Discription = "",
                                  qty = a.qty,
                                  CurrentInventory = c == null ? 0 : c.CurrentInventory,
                                  Price = i.PurchasePrice,
                                  NetAmmount = a.NetAmmount,
                                  TaxPercentage = a.TaxPercentage,
                                  TaxAmount = a.TaxAmmount,
                                  TotalAmountIncTax = a.TotalAmt,
                                  PurchaseMinOrderQty = i.PurchaseMinOrderQty,
                                  Status = a.Status,
                                  CreationDate = a.CreatedDate,
                                  Deleted = a.Deleted
                              }).ToList();
                return poList;
            }
        }
        
        public IEnumerable<OrderMaster> OrderMasterbySalesPersonId(int id)
        {
            return DbOrderMaster.Where(c => c.SalesPersonId == id && c.Deleted == false).OrderByDescending(d => d.CreatedDate).Take(50).AsEnumerable();
        }
       
        public IEnumerable<OrderMaster> OrderMasterbymobile(string Mobile)
        {
            return DbOrderMaster.Where(c => c.Customerphonenum == Mobile && c.Deleted == false).AsEnumerable();
        }
        public OrderMaster postOrderMaster(OrderMaster orderMaster)
        {
            OrderMaster customer = DbOrderMaster.Where(c => c.CustomerName.Trim().Equals(orderMaster.CustomerName.Trim())).SingleOrDefault();

            OrderMaster objOrderMaster = new OrderMaster();
            orderMaster.CompanyId = 1;
            orderMaster.BillingAddress = orderMaster.BillingAddress;
            orderMaster.ShippingAddress = orderMaster.ShippingAddress;
            orderMaster.Warehouseid = orderMaster.Warehouseid;
            orderMaster.active = true;
            orderMaster.CreatedDate = indianTime;
            orderMaster.UpdatedDate = indianTime;
            orderMaster.Deleted = false;
            DbOrderMaster.Add(orderMaster);
            int id = this.SaveChanges();
            return orderMaster;
        }
        public IEnumerable<OrderDispatchedMaster> AllDispatchedOrderMaster()
        {
            List<OrderDispatchedMaster> newdata = new List<OrderDispatchedMaster>();

            if (OrderDispatchedMasters.AsEnumerable().Count() > 0)
            {
                var orders = OrderDispatchedMasters.Where(x => x.Deleted != true).OrderByDescending(x => x.OrderId).ToList();                
                return orders;
            }
            else
            {  
                return OrderDispatchedMasters.OrderByDescending(x => x.OrderId).AsEnumerable();
            }
        }
        public IList<Customer> filteredCustomerMaster(string Cityid, DateTime? datefrom, DateTime? dateto, string mobile, string skcode)
        {
            if (mobile != "" && skcode == "" && dateto == null && datefrom == null)
            {
                //int cityid = Convert.ToInt32(Cityid.Trim());
                var filteredlist = (from od in Customers where od.Mobile.Contains(mobile) && od.Deleted == false select od).ToList();
                return filteredlist;
            }
            else if (dateto == null && datefrom == null && mobile != "" && skcode != "")
            {
                var filteredlist = (from od in Customers where od.Mobile.Contains(mobile) && od.Skcode.Contains(skcode) && od.Deleted == false select od).ToList();
                return filteredlist;
            }
            else if (dateto == null && datefrom == null && mobile == "" && skcode != "")
            {
                var filteredlist = (from od in Customers where od.Skcode.Contains(skcode) && od.Deleted == false select od).ToList();
                return filteredlist;
            }
            else
            {
                var filteredlist = (from od in Customers where od.Deleted == false && od.CreatedDate > datefrom && od.CreatedDate < dateto select od).ToList();
                return filteredlist;
            }
            return null;
        }
        //public IList<Customer> filteredCustomerMaster(string Cityid, string Warehouseid, DateTime? datefrom, DateTime? dateto)
        //{
        //    if (Warehouseid == "undefined" && dateto == null && datefrom == null)
        //    {
        //        int cityid = Convert.ToInt32(Cityid.Trim());
        //        var filteredlist = (from od in Customers where od.Cityid == cityid && od.Deleted == false select od).ToList();
        //        return filteredlist;
        //    }
        //    else if (dateto == null && datefrom == null)
        //    {
        //        int warehouseid = Convert.ToInt32(Warehouseid.Trim());
        //        int cityid = Convert.ToInt32(Cityid.Trim());
        //        var filteredlist = (from od in Customers where od.Cityid == cityid && od.Warehouseid == warehouseid && od.Deleted == false select od).ToList();
        //        return filteredlist;
        //    }
        //    else
        //    {
        //        int cityid = Convert.ToInt32(Cityid.Trim());
        //        int warehouseid = Convert.ToInt32(Warehouseid.Trim());
        //        var filteredlist = (from od in Customers where od.Cityid == cityid && od.Deleted == false && od.CreatedDate > datefrom && od.CreatedDate < dateto select od).ToList();
        //        return filteredlist;
        //    }
        //}


        public IList<OrderMaster> filteredOrderMasters1( string Warehouseid, DateTime datefrom, DateTime dateto)
        {
            int warehouseid = Convert.ToInt32(Warehouseid.Trim());
            {   
                    var result = DbOrderMaster.Where(x => x.Warehouseid == warehouseid && x.CreatedDate > datefrom && x.CreatedDate < dateto).ToList();
                    return result;
            }
        }        
        public IList<OrderMaster> filteredOrderMaster(string Cityid, string Warehouseid, DateTime datefrom, DateTime dateto, string search, string status, string deliveryboy)
        {
            int cityid = Convert.ToInt32(Cityid.Trim());
            int warehouseid = Convert.ToInt32(Warehouseid.Trim());
            if (string.IsNullOrEmpty(search) || search == "undefined")
            {
                var filteredlist = (
                    from od in DbOrderMaster
                    where od.Warehouseid == warehouseid &&
                    od.CityId == cityid &&
                    od.CreatedDate > datefrom &&
                    od.CreatedDate < dateto
                    select od
                    ).ToList();
                return filteredlist;
            }
            else
            {
                if (status == "Show All")
                {
                    var result = DbOrderMaster.Where(x => x.Warehouseid == warehouseid && x.CityId == cityid && x.CreatedDate > datefrom && x.CreatedDate < dateto && (x.CustomerName.Contains(search) || x.Customerphonenum.Contains(search))).ToList();
                    return result;
                }
                else
                {
                    var result = DbOrderMaster.Where(x => x.Warehouseid == warehouseid && x.CityId == cityid && x.CreatedDate > datefrom && x.CreatedDate < dateto && (x.CustomerName.Contains(search) || x.Customerphonenum.Contains(search))).ToList();
                    return result;
                }
            }
        }
        public bool DeleteOrderMaster(int id)
        {
            try
            {
                OrderMaster om = DbOrderMaster.Where(x => x.OrderId == id).SingleOrDefault();
                om.Deleted = true;
                DbOrderMaster.Attach(om);
                this.Entry(om).State = EntityState.Modified;
                this.SaveChanges();

                List<OrderDetails> orderdetail = DbOrderDetails.Where(x => x.OrderId == id).ToList();
                foreach (var od in orderdetail)
                {
                    od.Deleted = true;
                    DbOrderDetails.Attach(od);
                    this.Entry(od).State = EntityState.Modified;
                    this.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public OrderMaster AddOrderMaster(ShoppingCart sc)
        {
            double data = 0;
            List<IDetail> cart = new List<IDetail>();
            cart = sc.itemDetails.Where(a => a.qty > 0).Select(a => a).ToList<IDetail>();
            double finaltotal = 0;
            double finalTaxAmount = 0;
            double finalSGSTTaxAmount = 0;
            double finalCGSTTaxAmount = 0;
            double finalGrossAmount = 0;
            double finalTotalTaxAmount = 0;

            Customer cust = Customers.Where(c => c.Active == true && c.Mobile.Equals(sc.Customerphonenum)).Select(c => c).SingleOrDefault();
            Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == cust.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
            if (cust != null)
            {
                cust.ShippingAddress = sc.ShippingAddress;
                cust.CompanyId = 2;
                cust.ordercount = cust.ordercount + 1;
                cust.MonthlyTurnOver = cust.MonthlyTurnOver + sc.TotalAmount;
                Customers.Attach(cust);
                this.Entry(cust).State = EntityState.Modified;
                this.SaveChanges();
            }
            OrderMaster objOrderMaster = new OrderMaster();
            try
            {
                OrderMaster order = new OrderMaster();     

                People p = Peoples.Where(x => x.PeopleID == cust.ExecutiveId && x.Deleted == false).SingleOrDefault();
                objOrderMaster.CompanyId = 1;
                objOrderMaster.TotalAmount = sc.TotalAmount;

                if (objOrderMaster.TotalAmount >= 1000)
                {
                    data = (Convert.ToDouble(objOrderMaster.TotalAmount) / 100);
                    var rev = data;
                    int value = (int)rev;
                    cust.Rewardspoints = Convert.ToString(value);
                }
                objOrderMaster.CustomerCategoryId = 2;
                objOrderMaster.Status = "Pending";                
                objOrderMaster.CustomerName = cust.Name;
                objOrderMaster.ShopName = cust.ShopName;
                objOrderMaster.LandMark = cust.LandMark;
                objOrderMaster.Skcode = cust.Skcode;
                objOrderMaster.Tin_No = cust.RefNo;
                objOrderMaster.CustomerType = cust.CustomerType;
                objOrderMaster.Warehouseid = warehouse.Warehouseid;
                objOrderMaster.WarehouseName = warehouse.WarehouseName;
                objOrderMaster.CustomerId = cust.CustomerId;
                objOrderMaster.CityId = warehouse.Cityid;
                objOrderMaster.Customerphonenum = (sc.Customerphonenum);
                objOrderMaster.ClusterId = Convert.ToInt16(cust.ClusterId);

                var clstr = Clusters.Where(x => x.ClusterId == cust.ClusterId).SingleOrDefault();
                if (clstr != null)
                {
                    objOrderMaster.ClusterName = clstr.ClusterName;

                }
                if (p != null)
                {
                    try
                    {
                        objOrderMaster.OrderTakenSalesPersonId = sc.SalesPersonId;
                        People pep = Peoples.Where(x => x.PeopleID == sc.SalesPersonId && x.Deleted == false).SingleOrDefault();
                        if (pep != null)
                            objOrderMaster.OrderTakenSalesPerson = pep.PeopleFirstName + " " + pep.PeopleLastName;
                        else
                            objOrderMaster.OrderTakenSalesPerson = "Self";
                    }
                    catch (Exception ex)
                    {
                        objOrderMaster.OrderTakenSalesPersonId = 0;
                        objOrderMaster.OrderTakenSalesPerson = "Self";
                    }
                    objOrderMaster.SalesPersonId = p.PeopleID;
                    objOrderMaster.SalesPerson = p.PeopleFirstName + " " + p.PeopleLastName;
                    objOrderMaster.SalesMobile = p.Mobile;
                }
                else
                {
                    try
                    {
                        People pep = Peoples.Where(x => x.PeopleID == sc.SalesPersonId && x.Deleted == false).SingleOrDefault();
                        objOrderMaster.SalesPersonId = pep.PeopleID;
                        objOrderMaster.SalesPerson = pep.PeopleFirstName + " " + pep.PeopleLastName;
                        objOrderMaster.SalesMobile = pep.Mobile;
                        objOrderMaster.OrderTakenSalesPersonId = sc.SalesPersonId;
                        objOrderMaster.OrderTakenSalesPerson = pep.PeopleFirstName + " " + pep.PeopleLastName;
                    }
                    catch (Exception ex)
                    {
                        objOrderMaster.OrderTakenSalesPersonId = 0;
                        objOrderMaster.OrderTakenSalesPerson = "Self";
                    }
                    
                }
                objOrderMaster.BillingAddress = sc.ShippingAddress;
                objOrderMaster.ShippingAddress = sc.ShippingAddress;

                objOrderMaster.active = true;
                objOrderMaster.CreatedDate = indianTime;
                if (indianTime.Hour > 16)
                {
                    objOrderMaster.Deliverydate = indianTime.AddDays(2);
                }
                else
                {
                    objOrderMaster.Deliverydate = indianTime.AddDays(1);
                }
                objOrderMaster.UpdatedDate = indianTime;
                objOrderMaster.Deleted = false;

                List<OrderDetails> collection = new List<OrderDetails>();
                objOrderMaster.orderDetails = collection;

                
                var rewardpoint = 0;
                foreach (var i in cart.Select(x => x))
                {
                    if (i.qty != 0 && i.qty > 0)
                    {
                        ItemMaster items = itemMasters.Where(x => x.ItemId == i.ItemId).Select(x => x).FirstOrDefault();
                        OrderDetails od = new OrderDetails();
                        od.CustomerId = cust.CustomerId;
                        od.CustomerName = cust.Name;
                        od.City = warehouse.CityName;
                        od.CityId = warehouse.Cityid;
                        od.Mobile = cust.Mobile;
                        od.OrderDate = indianTime;
                        od.Status = "Pending";
                        od.CompanyId = Convert.ToInt32(cust.CompanyId);
                        od.Warehouseid = warehouse.Warehouseid;
                        od.WarehouseName = warehouse.WarehouseName;
                        od.NetPurchasePrice = items.NetPurchasePrice + ((items.NetPurchasePrice*items.TotalTaxPercentage)/100);
                        od.ItemId = items.ItemId;
                        od.Itempic = items.LogoUrl;
                        od.itemname = items.SellingUnitName;
                        od.SubcategoryName = items.SubcategoryName;
                        od.SubsubcategoryName = items.SubsubcategoryName;
                        od.itemcode = items.itemcode;
                        od.HSNCode = items.HSNCode;
                        od.itemNumber = items.Number;
                        od.Barcode = items.itemcode;
                        od.UnitPrice = items.UnitPrice;
                        od.price = items.price;
                        od.MinOrderQty = items.MinOrderQty;
                        int MOQ = items.MinOrderQty;

                        od.MinOrderQtyPrice = MOQ * items.UnitPrice;

                        od.qty = Convert.ToInt32(i.qty);

                        int qty = 0;
                        qty = Convert.ToInt32(od.qty);

                        od.SizePerUnit = items.SizePerUnit;
                        od.TaxPercentage = items.TotalTaxPercentage;

                        if (od.TaxPercentage >= 0) {
                            od.SGSTTaxPercentage = od.TaxPercentage/2;
                            od.CGSTTaxPercentage = od.TaxPercentage/2;
                        }

                        //........CALCULATION FOR NEW SHOPKIRANA.............................
                        od.Noqty = qty; // for total qty (no of items)

                        // STEP 1  (UNIT PRICE * QTY)     - SHOW PROPERTY                  
                        od.TotalAmt = System.Math.Round(od.UnitPrice * qty, 2);

                        // STEP 2 (AMOUT WITHOU TEX AND WITHOUT DISCOUNT ) - SHOW PROPERTY
                        od.AmtWithoutTaxDisc = ((100 * od.UnitPrice * qty) / (1 + od.TaxPercentage / 100)) / 100;

                        // STEP 3 (AMOUNT WITHOUT TAX AFTER DISCOUNT) - UNSHOW PROPERTY
                        od.AmtWithoutAfterTaxDisc = (100 * od.AmtWithoutTaxDisc) / (100 + items.PramotionalDiscount);

                        //STEP 4 (TAX AMOUNT) - UNSHOW PROPERTY
                        od.TaxAmmount = (od.AmtWithoutAfterTaxDisc * od.TaxPercentage) / 100;

                        if (od.TaxAmmount >= 0)
                        {
                            od.SGSTTaxAmmount = od.TaxAmmount/2;
                            od.CGSTTaxAmmount = od.TaxAmmount/2;
                        }

                        //STEP 5(TOTAL TAX AMOUNT) - UNSHOW PROPERTY
                        od.TotalAmountAfterTaxDisc = od.AmtWithoutAfterTaxDisc + od.TaxAmmount;


                        //...............Calculate Discount.............................
                        od.DiscountPercentage = items.PramotionalDiscount;
                        od.DiscountAmmount = (od.NetAmmount * items.PramotionalDiscount) / 100;
                        double DiscountAmmount = od.DiscountAmmount;
                        double NetAmtAfterDis = (od.NetAmmount - DiscountAmmount);
                        od.NetAmtAfterDis = (od.NetAmmount - DiscountAmmount);

                        double TaxAmmount = od.TaxAmmount;

                        od.Purchaseprice = items.price;
                        //od.VATTax = items.VATTax;
                        od.CreatedDate = indianTime;
                        od.UpdatedDate = indianTime;
                        od.Deleted = false;

                        //////////////////////////////////////////////////////////////////////////////////////////////
                        od.marginPoint = items.marginPoint *od.qty;

                        rewardpoint += od.marginPoint.GetValueOrDefault();
                        if (items.promoPoint > 0)
                        {
                            var pp = ((items.promoPerItems * od.qty));
                            if (items.promoPoint < pp)
                            {
                                var sup = supplierPointDb.Where(s=>s.SupplierCode == items.SUPPLIERCODES).FirstOrDefault();
                                if(sup != null)
                                {
                                    var ppp = pp - items.promoPoint;
                                    if (ppp > sup.PromoPoint) { 
                                        ppp = sup.PromoPoint;
                                        sup.PromoPoint -= sup.PromoPoint;
                                    }
                                    else
                                        sup.PromoPoint -= ppp.GetValueOrDefault();
                                    supplierPointDb.Attach(sup);
                                    this.Entry(sup).State = EntityState.Modified;
                                    this.SaveChanges();
                                    pp = ppp + items.promoPoint;
                                }
                                else
                                    pp = 0;
                            }
                            od.promoPoint = pp.GetValueOrDefault();
                            rewardpoint += pp.GetValueOrDefault();
                            items.promoPoint -= pp;
                            if (items.promoPoint < 0)
                                items.promoPoint = 0;
                            if (items.promoPoint == 0 || items.promoPoint < 0)
                                items.promoPerItems = 0;
                            itemMasters.Attach(items);
                            this.Entry(items).State = EntityState.Modified;
                            this.SaveChanges();
                            AngularJSAuthentication.API.Helper.refreshItemMaster(items.warehouse_id, items.Categoryid);
                        }

                        objOrderMaster.orderDetails.Add(od);
                        // for master
                        finaltotal = finaltotal + od.TotalAmt;
                        finalTaxAmount = finalTaxAmount + od.TaxAmmount;
                        finalSGSTTaxAmount = finalSGSTTaxAmount + od.SGSTTaxAmmount;
                        finalCGSTTaxAmount = finalCGSTTaxAmount + od.CGSTTaxAmmount;
                        finalGrossAmount = finalGrossAmount + od.TotalAmountAfterTaxDisc;
                        finalTotalTaxAmount = finalTotalTaxAmount + od.TotalAmountAfterTaxDisc;

                    }
                }

                CashConversion cash = CashConversionDb.FirstOrDefault();
                if (sc.WalletAmount > 0 && cash != null)
                {
                    var amount = (sc.WalletAmount / cash.point);
                    objOrderMaster.WalletAmount = amount;
                }
                else
                    objOrderMaster.WalletAmount = 0;
                if (sc.UsedPoint > 0)
                    objOrderMaster.UsedPoint = sc.UsedPoint;
                else
                    objOrderMaster.UsedPoint = 0;
                objOrderMaster.RewardPoint = rewardpoint;
                objOrderMaster.deliveryCharge = sc.deliveryCharge;
                objOrderMaster.TotalAmount = System.Math.Round(finaltotal, 2) + sc.deliveryCharge - objOrderMaster.WalletAmount.GetValueOrDefault();
                objOrderMaster.TaxAmount = System.Math.Round(finalTaxAmount, 2);
                objOrderMaster.SGSTTaxAmmount = System.Math.Round(finalSGSTTaxAmount, 2);
                objOrderMaster.CGSTTaxAmmount = System.Math.Round(finalCGSTTaxAmount, 2);
                // objOrderMaster.GrossAmount = Convert.ToInt32(finalGrossAmount) + sc.deliveryCharge - objOrderMaster.WalletAmount.GetValueOrDefault();
                objOrderMaster.GrossAmount = System.Math.Round((Convert.ToInt32(finalGrossAmount) + sc.deliveryCharge - objOrderMaster.WalletAmount.GetValueOrDefault()), 0);

                objOrderMaster.DiscountAmount = finalTotalTaxAmount - finaltotal;
                objOrderMaster.Trupay = sc.Trupay;

                List<OrderMaster> ord = DbOrderMaster.ToList();
                if (ord.Count > 0)
                {
                    order = ord.Last();
                }
                else
                {
                    order.OrderId = 0;
                }
                objOrderMaster.invoice_no = "Od_" + Convert.ToString(order.OrderId + 1);

                DbOrderMaster.Add(objOrderMaster);
                int id = this.SaveChanges();

                if (objOrderMaster.WalletAmount > 0)
                {
                    Wallet w = WalletDb.Where(c => c.CustomerId == cust.CustomerId).SingleOrDefault();
                    if (w != null)
                    {
                        if (objOrderMaster.WalletAmount > 0)
                            //if (objOrderMaster.WalletAmount < w.TotalAmount && objOrderMaster.WalletAmount > 0)
                            {
                            w.TotalAmount -= sc.WalletAmount;
                            w.TransactionDate = indianTime;

                            WalletDb.Attach(w);
                            this.Entry(w).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
                try
                {
                    var rpoint = RewardPointDb.Where(c => c.CustomerId == cust.CustomerId).SingleOrDefault();
                    if (rpoint != null)
                    {
                        if (rewardpoint > 0 || objOrderMaster.WalletAmount > 0)
                        {
                            if (rewardpoint > 0)
                               rpoint.EarningPoint += rewardpoint;
                            if (objOrderMaster.WalletAmount > 0)
                                rpoint.UsedPoint += sc.WalletAmount;
                            rpoint.UpdatedDate = indianTime;

                            RewardPointDb.Attach(rpoint);
                            this.Entry(rpoint).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                    else
                    {
                        RewardPoint point = new RewardPoint();
                        point.CustomerId = cust.CustomerId;
                        if (rewardpoint > 0)
                            point.EarningPoint = rewardpoint;
                        else
                            point.EarningPoint = 0;
                        point.TotalPoint = 0;
                        point.UsedPoint = 0;
                        point.MilestonePoint = 0;
                        point.CreatedDate = indianTime;
                        point.UpdatedDate = indianTime;
                        point.Deleted = false;
                        this.RewardPointDb.Add(point);
                        this.SaveChanges();
                    }
                    //string name = objOrderMaster.CustomerName.ToString();
                    string invoice = objOrderMaster.invoice_no.ToString();
                    try
                    {
                        //new Sms().sendOtp("8878121280", objOrderMaster.invoice_no + "\n------\n" + objOrderMaster.CustomerName + "\n" + objOrderMaster.Customerphonenum + "\n" + objOrderMaster.ShippingAddress + "\n------\nRs." + objOrderMaster.TotalAmount);
                        //Message msg1 = DbMessage.Where(x => x.MessageType == "Pending").FirstOrDefault();

                        //string smstemp = smstemplate(name, invoice, msg1.MessageText);
                        //new Sms().sendOtp(objOrderMaster.Customerphonenum, smstemp);
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error in Get single GetcusomerWallets " + ex.Message);
                }
                return objOrderMaster;

            }//end order master catch
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }





        private string smstemplate(string nm, string invoice, string text)
        {
            string bodytext = text;
            bodytext = bodytext.Replace("%CustomerName%", nm);
            bodytext = bodytext.Replace("%OrderId%", invoice);
            return bodytext;
        }

        public IEnumerable<OrderDetails> Allorddetails(int compid)
        {
            if (DbOrderMaster.AsEnumerable().Count() > 0)
            {
                return DbOrderDetails.AsEnumerable();
            }
            else
            {
                List<OrderDetails> itemMasters = new List<OrderDetails>();
                return DbOrderDetails.AsEnumerable();
            }
        }

        public IEnumerable<DamageOrderDetails> AllDorddetails(int compid)
        {
            if (DamageOrderDetailsDB.AsEnumerable().Count() > 0)
            {
                return DamageOrderDetailsDB.AsEnumerable();
            }
            else
            {
                List<DamageOrderDetails> itemMasters = new List<DamageOrderDetails>();
                return DamageOrderDetailsDB.AsEnumerable();
            }
        }

        public DemandMaster Adddemand(DemandMaster dm)
        {
            DemandMaster demand = new DemandMaster();
            demand.CityId = 1;
            demand.WharehouseId = 6;
            demand.CreatedDate = indianTime;
            demand.demand = dm.demand;
            dbDemandMasters.Add(demand);
            int id = this.SaveChanges();
            DemandDetails dd = new DemandDetails();
            foreach (var i in demand.demand.Select(x => x))
            {
                ItemMaster items = itemMasters.Where(x => x.itemname.Trim().ToLower() == i.itemname.Trim().ToLower()).Select(x => x).FirstOrDefault();
                dd.itemname = i.itemname;
                dd.ItemId = items.ItemId;
                dd.Description = i.Description;
                dd.ItemCode = items.itemcode;
                dd.Quantity = i.Quantity;
                dd.MOQ = items.MinOrderQty;
            }
            return null;
        }
        /// <summary>
        /// Category Module
        /// </summary>
        /// <param name="compid"></param>
        /// <returns></returns>       
        public IEnumerable<Category> AllCategory(int compid)
        {
            if (Categorys.AsEnumerable().Count() > 0)
            {
                return Categorys.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<Category> category = new List<Category>();
                return category.AsEnumerable();
            }
        }
        public Category AddCategory(Category category)
        {
            List<Category> cat = Categorys.Where(c => c.Deleted == false && c.CategoryName.Trim().Equals(category.CategoryName.Trim())).ToList();
            Category objcat = new Category();
            if (cat.Count == 0)
            {
                category.CreatedBy = objcat.CreatedBy;
                category.CreatedDate = indianTime;
                category.UpdatedDate = indianTime;
                category.IsActive = true;
                category.Deleted = false;
                Categorys.Add(category);
                this.SaveChanges();
        
                //category.LogoUrl = "http://ec2-34-208-118-110.us-west-2.compute.amazonaws.com:8888/../../images/catimages/" + category.BaseCategoryId + ".jpg";
                category.LogoUrl = category.LogoUrl;
                Categorys.Attach(category);
                this.Entry(category).State = EntityState.Modified;
                int id = this.SaveChanges();
                return category;
            }
            else
            {
                return objcat;
            }
        }
        public Category PutCategory(Category objcat)
        {
            Category category = Categorys.Where(x => x.Categoryid == objcat.Categoryid && x.Deleted == false).FirstOrDefault();
            BaseCategory BC = BaseCategoryDb.Where(x => x.BaseCategoryId == objcat.BaseCategoryId && x.Deleted == false).SingleOrDefault();
            if (category != null)
            {
                if (BC != null)
                {
                    category.BaseCategoryId = BC.BaseCategoryId;
                }
                else
                {
                    category.BaseCategoryId = objcat.BaseCategoryId;
                }
                category.UpdatedDate = indianTime;
                category.CategoryName = objcat.CategoryName;
                category.Discription = objcat.Discription;
                category.LogoUrl = objcat.LogoUrl;
                category.CreatedBy = objcat.CreatedBy;
                category.CreatedDate = objcat.CreatedDate;
                category.UpdateBy = objcat.UpdateBy;
                category.Code = objcat.Code;
                //category.LogoUrl = "http://ec2-34-208-118-110.us-west-2.compute.amazonaws.com:8888/../../images/catimages/" + category.BaseCategoryId + ".jpg";
                category.IsActive = objcat.IsActive;
                category.Deleted = objcat.Deleted;
                Categorys.Attach(category);
                this.Entry(category).State = EntityState.Modified;
                this.SaveChanges();

                AngularJSAuthentication.API.Helper.refreshCategory();
                return objcat;
            }
            else
            {
                return objcat;
            }
        }
        public bool DeleteCategory(int id)
        {
            try
            {
                Category category = Categorys.Where(x => x.Categoryid == id && x.Deleted == false).FirstOrDefault();
                category.Deleted = true;
                category.IsActive = false;
                Categorys.Attach(category);
                this.Entry(category).State = EntityState.Modified;
                this.SaveChanges();

                AngularJSAuthentication.API.Helper.refreshCategory();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<SubCategory> AllSubCategory(int compid)
        {
            if (SubCategorys.AsEnumerable().Count() > 0)
            {
                return SubCategorys.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<SubCategory> subcategory = new List<SubCategory>();
                return subcategory.AsEnumerable();
            }
        }
        public IEnumerable<SubCategory> AllSubCategoryy(int subcat)
        {
            if (SubCategorys.AsEnumerable().Count() > 0)
            {
                return SubCategorys.Where(p => p.Categoryid == subcat && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<SubCategory> subcategory = new List<SubCategory>();
                return subcategory.AsEnumerable();
            }
        }
        public SubCategory AddSubCategory(SubCategory subCategory)
        {
            Category category = Categorys.Where(c => c.Categoryid == subCategory.Categoryid && c.Deleted == false).FirstOrDefault();
            SubCategory objSubcat = new SubCategory();
            if (category != null)
            {
                subCategory.CreatedBy = subCategory.CreatedBy;
                subCategory.CreatedDate = indianTime;
                subCategory.UpdatedDate = indianTime;
                subCategory.CategoryName = category.CategoryName;
                subCategory.IsActive = true;
                subCategory.Deleted = false;
                SubCategorys.Add(subCategory);
                int id = this.SaveChanges();

                AngularJSAuthentication.API.Helper.refreshCategory();
                return subCategory;
            }
            else
            {
                return objSubcat;
            }
        }
        public SubCategory PutSubCategory(SubCategory objSubCategory)
        {
            SubCategory subCategory = SubCategorys.Where(x => x.SubCategoryId == objSubCategory.SubCategoryId && x.Deleted == false).FirstOrDefault();
            Category cat = Categorys.Where(x => x.Categoryid == objSubCategory.Categoryid).FirstOrDefault();
            if (subCategory != null)
            {
                if (cat != null)
                {
                    subCategory.Categoryid = cat.Categoryid;
                    subCategory.CategoryName = cat.CategoryName;
                }
                else
                {
                    subCategory.Categoryid = objSubCategory.Categoryid;
                }
                subCategory.UpdatedDate = indianTime;
                subCategory.SubCategoryId = objSubCategory.SubCategoryId;
                subCategory.SubcategoryName = objSubCategory.SubcategoryName;
                subCategory.Discription = objSubCategory.Discription;
                subCategory.LogoUrl = objSubCategory.LogoUrl;
                subCategory.CreatedBy = objSubCategory.CreatedBy;
                subCategory.CreatedDate = objSubCategory.CreatedDate;
                subCategory.UpdateBy = objSubCategory.UpdateBy;
                subCategory.Code = objSubCategory.Code;
                subCategory.IsActive = objSubCategory.IsActive;
                subCategory.Deleted = objSubCategory.Deleted;
                SubCategorys.Attach(subCategory);
                this.Entry(subCategory).State = EntityState.Modified;
                this.SaveChanges();

                AngularJSAuthentication.API.Helper.refreshCategory();
                return objSubCategory;
            }
            else
            {
                return objSubCategory;
            }
        }
        public bool DeleteSubCategory(int id)
        {
            try
            {
                SubCategory subCategory = SubCategorys.Where(x => x.SubCategoryId == id && x.Deleted == false).FirstOrDefault();
                subCategory.Deleted = true;
                subCategory.IsActive = false;
                SubCategorys.Attach(subCategory);
                this.Entry(subCategory).State = EntityState.Modified;
                this.SaveChanges();
                
                AngularJSAuthentication.API.Helper.refreshCategory();
                return true;
            }
            catch
            {
                return false;
            }
        }        
        public IEnumerable<SubsubCategory> AllSubsubCat(int compid)
        {
            if (SubsubCategorys.AsEnumerable().Count() > 0)
            {
                return SubsubCategorys.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<SubsubCategory> subsubcat = new List<SubsubCategory>();
                return subsubcat.AsEnumerable();
            }
        }
        public SubsubCategory AddSubsubCat(SubsubCategory subsubcat)
        {
            List<SubsubCategory> subsubcats = SubsubCategorys.Where(c => c.SubsubCategoryid.Equals(subsubcat.SubsubCategoryid) && c.Deleted == false).ToList();
            Category cat = Categorys.Where(x => x.Categoryid == subsubcat.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubCategory subcat = SubCategorys.Where(x => x.SubCategoryId == subsubcat.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubsubCategory objsubsubcat = new SubsubCategory();
            if (subsubcats.Count == 0)
            {
                subsubcat.CreatedBy = subsubcat.CreatedBy;
                subsubcat.CreatedDate = indianTime;
                subsubcat.UpdatedDate = indianTime;
                subsubcat.CategoryName = cat.CategoryName;
                subsubcat.SubcategoryName = subcat.SubcategoryName;
                subsubcat.IsActive = true;
                SubsubCategorys.Add(subsubcat);
                int id = this.SaveChanges();
                
                AngularJSAuthentication.API.Helper.refreshsubsubCategory(cat.Categoryid);
                return subsubcat;
            }
            else
            {
                return objsubsubcat;
            }
        }
        public SubsubCategory PutSubsubCat(SubsubCategory objsubsubcat)
        {
            SubsubCategory quesanss = SubsubCategorys.Where(x => x.SubsubCategoryid == objsubsubcat.SubsubCategoryid).Where(x => x.Deleted == false).FirstOrDefault();
            Category cat = Categorys.Where(x => x.Categoryid == objsubsubcat.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubCategory subcat = SubCategorys.Where(x => x.SubCategoryId == objsubsubcat.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();

            if (quesanss != null)
            {
                quesanss.UpdatedDate = indianTime;
                quesanss.Categoryid = objsubsubcat.Categoryid;
                quesanss.SubCategoryId = objsubsubcat.SubCategoryId;
                quesanss.CategoryName = cat.CategoryName;
                quesanss.LogoUrl = objsubsubcat.LogoUrl;
                quesanss.SubcategoryName = subcat.SubcategoryName;
                quesanss.SubsubcategoryName = objsubsubcat.SubsubcategoryName;
                quesanss.CreatedBy = objsubsubcat.CreatedBy;
                quesanss.CreatedDate = objsubsubcat.CreatedDate;
                quesanss.UpdateBy = objsubsubcat.UpdateBy;
                quesanss.Code = objsubsubcat.Code;
                quesanss.Type = objsubsubcat.Type;
                quesanss.IsActive = objsubsubcat.IsActive;
                quesanss.Deleted = objsubsubcat.Deleted;

                SubsubCategorys.Attach(quesanss);
                this.Entry(quesanss).State = EntityState.Modified;
                this.SaveChanges();

                AngularJSAuthentication.API.Helper.refreshsubsubCategory(cat.Categoryid);
                return objsubsubcat;
            }
            else
            {
                return objsubsubcat;
            }
        }
        public bool DeleteSubsubCat(int id)
        {
            try
            {
                SubsubCategory quesanss = SubsubCategorys.Where(x => x.SubsubCategoryid == id && x.Deleted == false).FirstOrDefault();
                quesanss.Deleted = true;
                quesanss.IsActive = false;
                SubsubCategorys.Attach(quesanss);
                this.Entry(quesanss).State = EntityState.Modified;
                this.SaveChanges();

                Category cat = Categorys.Where(x => x.Categoryid == quesanss.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
                AngularJSAuthentication.API.Helper.refreshsubsubCategory(cat.Categoryid);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public SubsubCategory AddQuesAnsxl(SubsubCategory quesans)
        {
            List<SubsubCategory> quesanss = SubsubCategorys.Where(c => c.Categoryid.Equals(quesans.Categoryid) && c.Deleted == false).ToList();
            SubsubCategory objQuesAns = new SubsubCategory();
            if (quesanss.Count == 0)
            {
                quesans.CreatedBy = quesans.CreatedBy;
                quesans.CreatedDate = indianTime;
                quesans.UpdatedDate = indianTime;
                SubsubCategorys.Add(quesans);
                int id = this.SaveChanges();
                return quesans;
            }
            else
            {
                return objQuesAns;
            }
        }

        //.............Item Master...................//        
        public List<ItemMaster> AddBulkItemMaster(List<ItemMaster> itemCollection)
        {
            logger.Info("start Item Upload Exel File: ");
            List<Warehouse> wh = Warehouses.Where(x => x.Deleted == false).Select(x => x).ToList();
            List<string> Ids = new List<string>();

            foreach (var itemmaster in itemCollection)
            {
                try
                {
                    City citym = Cities.Where(X => X.CityName == itemmaster.CityName && X.Deleted == false).Select(x => x).SingleOrDefault();
                    List<TaxGroupDetails> TaxG = DbTaxGroupDetails.Where(x => x.GruopID == itemmaster.GruopID).Select(x => x).ToList();
                    List<Warehouse> warehouse = Warehouses.Where(x => x.Cityid == citym.Cityid && x.Warehouseid == itemmaster.warehouse_id && x.Deleted == false).Select(x => x).ToList();
                    TaxGroup Tg = DbTaxGroup.Where(x => x.GruopID == itemmaster.GruopID && x.Deleted == false).Select(x => x).FirstOrDefault();
                    double TotalTax = 0;
                    if (TaxG.Count != 0)
                    {
                        foreach (var i in TaxG)
                        {
                            TotalTax += i.TPercent;
                        }
                    }
                    foreach (var o in warehouse)
                    {
                        ItemMaster objitemmaster = new ItemMaster();
                        ItemMaster check = itemMasters.Where(w => w.warehouse_id == o.Warehouseid && w.SellingSku == itemmaster.SellingSku).SingleOrDefault();
                        int stock = itemmaster.CurrentStock;
                        if (check == null)
                        {
                            try
                            {
                                WarehouseCategory category = DbWarehouseCategory.Where(X => X.Warehouseid == o.Warehouseid && X.Deleted == false).Where(x => x.CategoryName == itemmaster.CategoryName).Select(x => x).FirstOrDefault();
                                Category cate = Categorys.Where(x => x.CategoryName == itemmaster.CategoryName && x.Deleted == false).Select(x => x).SingleOrDefault();
                                BaseCategory basecat = BaseCategoryDb.Where(x => x.BaseCategoryName == itemmaster.BaseCategoryName && x.Deleted == false).Select(x => x).SingleOrDefault();
                                SubCategory subcategory = SubCategorys.Where(x => x.SubcategoryName == itemmaster.SubcategoryName && x.Categoryid == cate.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
                                SubsubCategory Subsubcategory = SubsubCategorys.Where(x => x.SubsubcategoryName == itemmaster.SubsubcategoryName && x.SubCategoryId == subcategory.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();

                                itemmaster.CategoryName = category.CategoryName;
                                itemmaster.Categoryid = category.Categoryid;
                                // base cat 
                                itemmaster.BaseCategoryName = basecat.BaseCategoryName;
                                itemmaster.BaseCategoryid = basecat.BaseCategoryId;

                                itemmaster.SubcategoryName = subcategory.SubcategoryName;
                                itemmaster.SubCategoryId = subcategory.SubCategoryId;
                                itemmaster.SubsubcategoryName = Subsubcategory.SubsubcategoryName;
                                itemmaster.SubsubCategoryid = Subsubcategory.SubsubCategoryid;

                                if (cate.LogoUrl != null)
                                {
                                    itemmaster.CatLogoUrl = cate.LogoUrl;
                                }
                                itemmaster.warehouse_id = o.Warehouseid;
                                itemmaster.WarehouseName = o.WarehouseName;
                                itemmaster.Cityid = citym.Cityid;
                                itemmaster.CityName = citym.CityName;
                                itemmaster.CreatedDate = indianTime;
                                itemmaster.UpdatedDate = indianTime;
                                itemmaster.PramotionalDiscount = 0;
                                itemmaster.LogoUrl = "http://SK10-12-15.moreyeahs.in/../../UploadedLogos/" + itemmaster.SellingSku + ".jpg";
                                itemmaster.TotalTaxPercentage = TotalTax;
                                logger.Info(" itemmaster excel for each loop before add: " + itemmaster.itemname);

                                itemmaster.UnitPrice = itemmaster.PurchasePrice + (itemmaster.PurchasePrice * itemmaster.Margin / 100);
                                if (itemmaster.Margin > 0)
                                {
                                    var rs = RetailerShareDb.Where(r => r.cityid == itemmaster.Cityid).FirstOrDefault();
                                    if (rs != null)
                                    {
                                        var cf = RPConversionDb.FirstOrDefault();
                                        try
                                        {
                                            double mv = (itemmaster.PurchasePrice * (itemmaster.Margin / 100) * (rs.share / 100) * cf.point);
                                            var value = Math.Round(mv, MidpointRounding.AwayFromZero);
                                            itemmaster.marginPoint = Convert.ToInt32(value);
                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Error(ex.Message);
                                        }
                                    }
                                }
                                itemMasters.Add(itemmaster);
                                int Itemid = this.SaveChanges();
                                Itemid = itemmaster.ItemId;
                                Ids.Add(Itemid.ToString());
                                try
                                {
                                    CurrentStock cntstock = DbCurrentStock.Where(x => x.ItemNumber == itemmaster.Number && x.Warehouseid == itemmaster.warehouse_id).SingleOrDefault();
                                    if (cntstock == null)
                                    {
                                        CurrentStock newCstk = new CurrentStock();
                                        newCstk.ItemId = itemmaster.ItemId;
                                        newCstk.ItemName = itemmaster.itemname;
                                        newCstk.ItemNumber = itemmaster.Number;
                                        newCstk.Barcode = itemmaster.Barcode;
                                        newCstk.Warehouseid = category.Warehouseid;
                                        newCstk.WarehouseName = category.WarehouseName;
                                        newCstk.CurrentInventory = stock;
                                        newCstk.CreationDate = DateTime.Now;
                                        newCstk.UpdatedDate = DateTime.Now;
                                        DbCurrentStock.Add(newCstk);
                                        this.SaveChanges();
                                    }
                                    else
                                    {
                                        cntstock.ItemName = itemmaster.itemname;
                                        cntstock.ItemNumber = itemmaster.Number;
                                        cntstock.Barcode = itemmaster.Barcode;
                                        cntstock.Warehouseid = category.Warehouseid;
                                        cntstock.WarehouseName = category.WarehouseName;
                                        cntstock.UpdatedDate = DateTime.Now;
                                        cntstock.Deleted = false;
                                        DbCurrentStock.Attach(cntstock);
                                        this.Entry(cntstock).State = EntityState.Modified;
                                        this.SaveChanges();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                var itemoldData = itemMasters.Where(x => x.ItemId == check.ItemId).SingleOrDefault();
                                ItemMasterHistory Os = new ItemMasterHistory();
                                if (itemoldData != null)
                                {
                                    Os.ItemId = itemoldData.ItemId;
                                    Os.Cityid = itemoldData.Cityid;
                                    Os.CityName = itemoldData.CityName;
                                    Os.Categoryid = itemoldData.Categoryid;
                                    Os.SubCategoryId = itemoldData.SubCategoryId;
                                    Os.SubsubCategoryid = itemoldData.SubsubCategoryid;
                                    Os.warehouse_id = itemoldData.warehouse_id;
                                    Os.SupplierId = itemoldData.SupplierId;
                                    Os.SUPPLIERCODES = itemoldData.SUPPLIERCODES;
                                    Os.CompanyId = itemoldData.CompanyId;
                                    Os.CategoryName = itemoldData.CategoryName;
                                    Os.BaseCategoryid = itemoldData.BaseCategoryid;
                                    Os.BaseCategoryName = itemoldData.BaseCategoryName;
                                    Os.SubcategoryName = itemoldData.SubcategoryName;
                                    Os.SubsubcategoryName = itemoldData.SubsubcategoryName;
                                    Os.SupplierName = itemoldData.SupplierName;
                                    Os.itemname = itemoldData.itemname;
                                    Os.itemcode = itemoldData.itemcode;
                                    Os.SellingUnitName = itemoldData.SellingUnitName;
                                    Os.PurchaseUnitName = itemoldData.PurchaseUnitName;
                                    Os.price = itemoldData.price;
                                    Os.VATTax = itemoldData.VATTax;
                                    Os.active = itemoldData.active;
                                    Os.LogoUrl = itemoldData.LogoUrl;
                                    Os.CatLogoUrl = itemoldData.CatLogoUrl;
                                    Os.MinOrderQty = itemoldData.MinOrderQty;
                                    Os.PurchaseMinOrderQty = itemoldData.PurchaseMinOrderQty;
                                    Os.GruopID = itemoldData.GruopID;
                                    Os.TGrpName = itemoldData.TGrpName;
                                    Os.Discount = itemoldData.Discount;
                                    Os.UnitPrice = itemoldData.UnitPrice;
                                    Os.Number = itemoldData.Number;
                                    Os.PurchaseSku = itemoldData.PurchaseSku;
                                    Os.SellingSku = itemoldData.SellingSku;
                                    Os.PurchasePrice = itemoldData.PurchasePrice;
                                    Os.GeneralPrice = itemoldData.GeneralPrice;
                                    Os.title = itemoldData.title;
                                    Os.Description = itemoldData.Description;
                                    Os.StartDate = itemoldData.StartDate;
                                    Os.EndDate = itemoldData.EndDate;
                                    Os.PramotionalDiscount = itemoldData.PramotionalDiscount;
                                    Os.TotalTaxPercentage = itemoldData.TotalTaxPercentage;
                                    Os.WarehouseName = itemoldData.WarehouseName;
                                    // Os.CreatedDate = itemoldData.CreatedDate;
                                    Os.CreatedDate = DateTime.Now;
                                    Os.UpdatedDate = itemoldData.UpdatedDate;
                                    Os.Deleted = itemoldData.Deleted;
                                    Os.IsDailyEssential = itemoldData.IsDailyEssential;
                                    Os.DisplaySellingPrice = itemoldData.DisplaySellingPrice;
                                    Os.StoringItemName = itemoldData.StoringItemName;
                                    Os.SizePerUnit = itemoldData.SizePerUnit;
                                    Os.HindiName = itemoldData.HindiName;
                                    Os.Barcode = itemoldData.Barcode;
                                    Os.HindiName = itemoldData.HindiName;
                                    Os.Barcode = itemoldData.Barcode;
                                    ItemMasterHistoryDb.Add(Os);
                                    int id = this.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Error loading  item:- " + itemmaster.itemname + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
                            }

                            WarehouseCategory category = DbWarehouseCategory.Where(X => X.Warehouseid == o.Warehouseid && X.Deleted == false).Where(x => x.CategoryName == itemmaster.CategoryName).Select(x => x).FirstOrDefault();
                            Category cate = Categorys.Where(x => x.CategoryName == itemmaster.CategoryName && x.Deleted == false).Select(x => x).SingleOrDefault();
                            BaseCategory basecat = BaseCategoryDb.Where(x => x.BaseCategoryName == itemmaster.BaseCategoryName && x.Deleted == false).Select(x => x).SingleOrDefault();
                            SubCategory subcategory = SubCategorys.Where(x => x.SubcategoryName == itemmaster.SubcategoryName && x.Categoryid == cate.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
                            SubsubCategory Subsubcategory = SubsubCategorys.Where(x => x.SubsubcategoryName == itemmaster.SubsubcategoryName && x.SubCategoryId == subcategory.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
                            logger.Info(" itemmaster excel update item: " + itemmaster.itemname);


                            check.BaseCategoryName = basecat.BaseCategoryName;
                            check.BaseCategoryid = basecat.BaseCategoryId;
                            check.Categoryid = category.Categoryid;
                            check.CategoryName = category.CategoryName;
                            check.SubcategoryName = subcategory.SubcategoryName;
                            check.SubCategoryId = subcategory.SubCategoryId;
                            check.SubsubCategoryid = Subsubcategory.SubsubCategoryid;
                            check.SubsubcategoryName = Subsubcategory.SubsubcategoryName;

                            check.itemname = itemmaster.itemname;
                            check.price = itemmaster.price;
                            check.GeneralPrice = itemmaster.GeneralPrice;
                            check.UnitPrice = itemmaster.UnitPrice;
                            check.MinOrderQty = itemmaster.MinOrderQty;
                            check.TotalTaxPercentage = TotalTax;
                            check.TGrpName = Tg.TGrpName;
                            check.GruopID = Tg.GruopID;
                            check.HSNCode = itemmaster.HSNCode;

                            check.PurchaseUnitName = itemmaster.PurchaseUnitName;
                            check.PurchasePrice = itemmaster.PurchasePrice;
                            check.PurchaseMinOrderQty = itemmaster.PurchaseMinOrderQty;

                            check.SellingUnitName = itemmaster.SellingUnitName;
                            check.StoringItemName = itemmaster.StoringItemName;
                            check.PurchaseSku = itemmaster.PurchaseSku;
                            check.Number = itemmaster.Number;
                            check.SizePerUnit = itemmaster.SizePerUnit;
                            check.HindiName = itemmaster.HindiName;
                            check.SupplierId = itemmaster.SupplierId;
                            check.SUPPLIERCODES = itemmaster.SUPPLIERCODES;
                            check.SupplierName = itemmaster.SupplierName;
                            check.Cityid = itemmaster.Cityid;
                            check.Barcode = itemmaster.Barcode;
                            check.active = itemmaster.active;
                            check.Deleted = itemmaster.Deleted;
                            check.CityName = citym.CityName;
                            check.warehouse_id = o.Warehouseid;
                            check.WarehouseName = o.WarehouseName;
                            check.Margin = itemmaster.Margin;
                            check.promoPoint = itemmaster.promoPoint;
                            check.HSNCode = itemmaster.HSNCode;

                            itemmaster.LogoUrl = "http://SK10-12-15.moreyeahs.in/../../UploadedLogos/" + itemmaster.SellingSku + ".jpg";
                            check.CatLogoUrl = cate.LogoUrl;

                            check.CreatedDate = check.CreatedDate;
                            check.UpdatedDate = indianTime;
                            check.UnitPrice = check.PurchasePrice + (check.PurchasePrice * check.Margin / 100);

                            if (check.Margin > 0)
                            {
                                var rs = RetailerShareDb.Where(r => r.cityid == itemmaster.Cityid).FirstOrDefault();
                                if (rs != null)
                                {
                                    var cf = RPConversionDb.FirstOrDefault();
                                    try
                                    {
                                        double mv = (check.PurchasePrice * (check.Margin / 100) * (rs.share / 100) * cf.point);
                                        var value = Math.Round(mv, MidpointRounding.AwayFromZero);
                                        itemmaster.marginPoint = Convert.ToInt32(value);
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                    }
                                }
                            }
                            itemMasters.Attach(check);
                            this.Entry(check).State = EntityState.Modified;
                            this.SaveChanges();

                            try
                            {
                                CurrentStock cntstock = DbCurrentStock.Where(x => x.ItemNumber == itemmaster.Number && x.Warehouseid == check.warehouse_id).SingleOrDefault();
                                if (cntstock == null)
                                {
                                    CurrentStock newCstk = new CurrentStock();
                                    newCstk.ItemId = itemmaster.ItemId;
                                    newCstk.ItemName = itemmaster.itemname;
                                    newCstk.ItemNumber = itemmaster.Number;
                                    newCstk.Barcode = itemmaster.Barcode;
                                    newCstk.Warehouseid = category.Warehouseid;
                                    newCstk.WarehouseName = category.WarehouseName;
                                    newCstk.CurrentInventory = stock;
                                    newCstk.CreationDate = DateTime.Now;
                                    newCstk.UpdatedDate = DateTime.Now;
                                    DbCurrentStock.Add(newCstk);
                                    this.SaveChanges();
                                }
                                else
                                {
                                    cntstock.ItemName = itemmaster.itemname;
                                    cntstock.ItemNumber = itemmaster.Number;
                                    cntstock.Barcode = itemmaster.Barcode;
                                    cntstock.Warehouseid = category.Warehouseid;
                                    cntstock.WarehouseName = category.WarehouseName;
                                    cntstock.UpdatedDate = DateTime.Now;
                                    cntstock.Deleted = false;
                                    DbCurrentStock.Attach(cntstock);
                                    this.Entry(cntstock).State = EntityState.Modified;
                                    this.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Error loading  item:- " + itemmaster.itemname + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
                }
            }
            return null;
        }
        public IEnumerable<ItemMaster> itembyid(int id)
        {
            return itemMasters.Where(c => c.ItemId.Equals(id));
        }
        public IEnumerable<ItemMaster> AllItemMaster()
        {
                return itemMasters.Where(x=>x.active ==true && x.Deleted == false).AsEnumerable();           
        }
        public IEnumerable<ItemMaster> itembystring(string itemnm)
        {

            List<ItemMaster> item = new List<ItemMaster>();
            var listitems = itemMasters.Where(x => x.itemname.ToLower().Contains(itemnm.Trim().ToLower())&& x.active==true).ToList().Take(10);
            if (listitems.Count() > 0)
          {
                foreach (var litem in listitems)
                {
                    Category category = Categorys.Where(x => x.Categoryid == litem.Categoryid && x.Deleted == false && x.IsActive==true).Select(x => x).FirstOrDefault();
                    SubCategory subcategory = SubCategorys.Where(x => x.SubCategoryId == litem.SubCategoryId && x.Deleted == false && x.IsActive==true).Select(x => x).FirstOrDefault();
                    SubsubCategory Subsubcategory = SubsubCategorys.Where(x => x.SubsubCategoryid == litem.SubsubCategoryid && x.Deleted == false && x.IsActive==true).Select(x => x).FirstOrDefault();
                    if (category!= null && subcategory!=null && Subsubcategory!=null) {

                        item.Add(litem);

                    }

                }

            }
            else {

            }
            return item;
        }        
        public ItemMaster AddItemMaster(ItemMaster itemmaster)
        {
            List<ItemMaster> itemMaster = itemMasters.Where(c => c.SellingSku.Trim().Equals(itemmaster.SellingSku.Trim()) && c.warehouse_id == itemmaster.warehouse_id).ToList();
            City citym = Cities.Where(X => X.Cityid == itemmaster.Cityid && X.Deleted == false).Select(x => x).SingleOrDefault();
            Category category = Categorys.Where(x => x.Categoryid == itemmaster.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
            BaseCategory basecat = BaseCategoryDb.Where(x => x.BaseCategoryId == category.BaseCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubCategory subcategory = SubCategorys.Where(x => x.SubCategoryId == itemmaster.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubsubCategory Subsubcategory = SubsubCategorys.Where(x => x.SubsubCategoryid == itemmaster.SubsubCategoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
            TaxGroupDetails taxgroup = DbTaxGroupDetails.Where(x => x.GruopID == itemmaster.GruopID).Select(x => x).FirstOrDefault();
            TaxGroup Tg = DbTaxGroup.Where(x => x.GruopID == itemmaster.GruopID && x.Deleted == false).Select(x => x).FirstOrDefault();
            List<TaxGroupDetails> TaxG = DbTaxGroupDetails.Where(x => x.GruopID == itemmaster.GruopID).Select(x => x).ToList();
            Supplier SN = Suppliers.Where(x => x.SupplierId == itemmaster.SupplierId && x.Deleted == false).Select(x => x).FirstOrDefault();
            List<Warehouse> warehouse = Warehouses.Where(x => x.Cityid == itemmaster.Cityid && x.Deleted == false).Select(x => x).ToList();
            double TotalTax = 0;
            if (TaxG.Count != 0)
            {
                foreach (var i in TaxG)
                {
                    TotalTax += i.TPercent;
                }
            }
            foreach (var o in warehouse)
            {
                ItemMaster objitemmaster = new ItemMaster();

                ItemMaster check = itemMasters.Where(w => w.warehouse_id == o.Warehouseid && w.SellingSku == itemmaster.SellingSku).SingleOrDefault();
                if (check == null)
                {
                    if (itemMaster.Count == 0)
                    {
                        itemmaster.GruopID = taxgroup.GruopID;
                        itemmaster.TGrpName = Tg.TGrpName;
                        itemmaster.TotalTaxPercentage = TotalTax;
                        itemmaster.CatLogoUrl = category.LogoUrl;
                        itemmaster.warehouse_id = o.Warehouseid;
                        itemmaster.WarehouseName = o.WarehouseName;
                        itemmaster.SUPPLIERCODES = SN.SUPPLIERCODES;
                        itemmaster.SupplierId = SN.SupplierId;
                        itemmaster.SupplierName = SN.Name;
                        itemmaster.CityName = citym.CityName;
                        //base cat
                        itemmaster.BaseCategoryid = basecat.BaseCategoryId;
                        itemmaster.BaseCategoryName = basecat.BaseCategoryName;
                        
                        itemmaster.LogoUrl = "http://ec2-34-208-118-110.us-west-2.compute.amazonaws.com:8888/../../images/itemimages/" + itemmaster.SellingSku + ".jpg";
                        
                        itemmaster.UpdatedDate = indianTime;
                        itemmaster.CreatedDate = indianTime;
                        itemmaster.CategoryName = category.CategoryName;
                        itemmaster.SubcategoryName = subcategory.SubcategoryName;
                        itemmaster.SubsubcategoryName = Subsubcategory.SubsubcategoryName;

                        if (itemmaster.Margin > 0) {
                            var rs = RetailerShareDb.Where(r => r.cityid == itemmaster.Cityid).FirstOrDefault();
                            if(rs != null) {
                                var cf = RPConversionDb.FirstOrDefault();
                                try
                                {
                                    double mv = (itemmaster.PurchasePrice * (itemmaster.Margin / 100) * (rs.share / 100) * cf.point);
                                    var value = Math.Round(mv, MidpointRounding.AwayFromZero);
                                    itemmaster.marginPoint = Convert.ToInt32(value);
                                }
                                catch (Exception ex) {
                                    logger.Error(ex.Message);
                                }
                            }
                        }

                        itemMasters.Add(itemmaster);
                        int Itemid = this.SaveChanges();

                        AngularJSAuthentication.API.Helper.refreshItemMaster(itemmaster.warehouse_id);
                        try
                        {
                            CurrentStock cntstock = DbCurrentStock.Where(x => x.ItemNumber == itemmaster.Number && x.Warehouseid == itemmaster.warehouse_id).SingleOrDefault();
                            if (cntstock == null)
                            {
                                CurrentStock newCstk = new CurrentStock();
                                newCstk.ItemId = itemmaster.ItemId;
                                newCstk.ItemName = itemmaster.itemname;
                                newCstk.ItemNumber = itemmaster.Number;
                                newCstk.Barcode = itemmaster.Barcode;
                                newCstk.Warehouseid = itemmaster.warehouse_id;
                                newCstk.WarehouseName = itemmaster.WarehouseName;
                                newCstk.CurrentInventory = 0;
                                newCstk.CreationDate = indianTime;
                                newCstk.UpdatedDate = indianTime;
                                DbCurrentStock.Add(newCstk);
                                this.SaveChanges();

                                try
                                {
                                    var itemoldData = DbCurrentStock.Where(x => x.ItemNumber == itemmaster.Number).SingleOrDefault();
                                    CurrentStockHistory Oss = new CurrentStockHistory();
                                    Oss.ItemId = itemoldData.ItemId;
                                    Oss.StockId = itemoldData.StockId;
                                    Oss.ItemNumber = itemoldData.ItemNumber;
                                    Oss.ItemName = itemoldData.ItemName;
                                    Oss.TotalInventory = itemoldData.CurrentInventory;
                                    Oss.WarehouseName = itemoldData.WarehouseName;
                                    Oss.CreationDate = DateTime.Now;
                                    CurrentStockHistoryDb.Add(Oss);
                                    int idd = this.SaveChanges();
                                }
                                catch(Exception ex) {

                                }
                            }
                            else
                            {
                                cntstock.ItemName = itemmaster.itemname;
                                cntstock.ItemNumber = itemmaster.Number;
                                cntstock.Barcode = itemmaster.Barcode;
                                cntstock.Warehouseid = itemmaster.warehouse_id;
                                cntstock.WarehouseName = itemmaster.WarehouseName;
                                cntstock.Deleted = false;
                                cntstock.UpdatedDate = indianTime;
                                DbCurrentStock.Attach(cntstock);
                                this.Entry(cntstock).State = EntityState.Modified;
                                this.SaveChanges();
                                try {
                                    var itemoldData = DbCurrentStock.Where(x => x.ItemNumber == cntstock.ItemNumber).SingleOrDefault();
                                CurrentStockHistory Oss = new CurrentStockHistory();
                                Oss.ItemId = itemoldData.ItemId;
                                Oss.StockId = itemoldData.StockId;
                                Oss.ItemNumber = itemoldData.ItemNumber;
                                Oss.ItemName = itemoldData.ItemName;
                                Oss.TotalInventory = itemoldData.CurrentInventory;
                                Oss.WarehouseName = itemoldData.WarehouseName;
                                Oss.CreationDate = DateTime.Now;
                                CurrentStockHistoryDb.Add(Oss);
                                int idd = this.SaveChanges();

                                } catch (Exception ex) {


                                }
                               
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                    else
                    {
                        return objitemmaster;
                    }
                }
            }
            return itemmaster;
        }
        public ItemMaster Saveediteditem(List<ItemMaster> itemmasterList)
        {
            ItemMaster objItemMaster = new ItemMaster();
            objItemMaster = itemmasterList[0];
            List<ItemMaster> item = new List<ItemMaster>();

            ItemMaster im = new ItemMaster();
            foreach (var itemmaster in itemmasterList)
            {
                item = null;
                item = itemMasters.Where(i => i.warehouse_id == objItemMaster.warehouse_id && i.SellingSku == itemmaster.SellingSku).ToList();
                foreach (var it in item)
                {
                    im = null;
                    im = itemMasters.Where(i => i.ItemId == it.ItemId).SingleOrDefault();
                    if (im != null)
                    {
                        im.price = itemmaster.price;
                        im.Margin = itemmaster.Margin;
                        im.PurchasePrice = itemmaster.PurchasePrice;
                        im.UnitPrice = itemmaster.PurchasePrice + (itemmaster.PurchasePrice * itemmaster.Margin / 100);
                        if (im.Margin > 0)
                        {
                            var rs = RetailerShareDb.Where(r => r.cityid == im.Cityid).FirstOrDefault();
                            if (rs != null)
                            {
                                var cf = RPConversionDb.FirstOrDefault();
                                try
                                {
                                    double mv = (im.PurchasePrice * (im.Margin / 100) * (rs.share / 100) * cf.point);
                                    var value1 = Math.Round(mv, MidpointRounding.AwayFromZero);
                                    im.marginPoint = Convert.ToInt32(value1);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.Message);
                                }
                            }
                        }
                        itemMasters.Attach(im);
                        this.Entry(im).State = EntityState.Modified;
                        this.SaveChanges();
                        AngularJSAuthentication.API.Helper.refreshItemMaster(im.warehouse_id, im.Categoryid);
                    }
                }
                //else
                //{
                //    return objItemMaster;
                //}
            }
            return im;
        }
        public void convertitemimage(string url, string sku)
        {
            var webRootPath = HttpContext.Current.Server.MapPath("~/images/itemimages");
            string oldname = webRootPath + "/" + url;
            string newname = webRootPath + "/" + sku + ".jpg";
            System.IO.File.Move(oldname, newname);
        }

        public ItemMaster PutItemMaster(ItemMaster objItemMaster)
        {
            List<ItemMaster> itemmasterList = itemMasters.Where(x => x.SellingSku == objItemMaster.SellingSku && x.warehouse_id == objItemMaster.warehouse_id ).ToList();
            City citym = Cities.Where(X => X.Cityid == objItemMaster.Cityid && X.Deleted == false).Select(x => x).SingleOrDefault();
            Category cit = Categorys.Where(x => x.Categoryid == objItemMaster.Categoryid && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubCategory cita = SubCategorys.Where(x => x.SubCategoryId == objItemMaster.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            SubsubCategory st = SubsubCategorys.Where(x => x.SubsubCategoryid == objItemMaster.SubsubCategoryid && x.Deleted == false).Select(x => x).FirstOrDefault();            
            TaxGroupDetails taxgroup = DbTaxGroupDetails.Where(x => x.GruopID == objItemMaster.GruopID).Select(x => x).FirstOrDefault();
            TaxGroup Tg = DbTaxGroup.Where(x => x.GruopID == objItemMaster.GruopID && x.Deleted == false).Select(x => x).FirstOrDefault();
            List<TaxGroupDetails> TaxG = DbTaxGroupDetails.Where(x => x.GruopID == objItemMaster.GruopID).Select(x => x).ToList();
            List<Warehouse> warehouse = Warehouses.Where(x => x.Cityid == objItemMaster.Cityid && x.Deleted == false).Select(x => x).ToList();
            Supplier SN = Suppliers.Where(x => x.SupplierId == objItemMaster.SupplierId && x.Deleted == false).Select(x => x).FirstOrDefault();

            double TotalTax = 0;
            if (TaxG.Count != 0)
            {
                foreach (var i in TaxG)
                {
                    TotalTax += i.TPercent;
                }
            }
            ItemMaster im = new ItemMaster();
                foreach (var itemmaster in itemmasterList)
            {
                var ids = itemmaster.Categoryid;
                if (itemmaster != null)
                {
                    var itemoldData = itemMasters.Where(x => x.ItemId == itemmaster.ItemId).SingleOrDefault();
                    try
                    {                        
                        ItemMasterHistory Os = new ItemMasterHistory();
                        if (itemoldData != null)
                        {
                            Os.ItemId = itemoldData.ItemId;
                            Os.Cityid = itemoldData.Cityid;
                            Os.CityName = itemoldData.CityName;
                            Os.Categoryid = itemoldData.Categoryid;
                            Os.SubCategoryId = itemoldData.SubCategoryId;
                            Os.SubsubCategoryid = itemoldData.SubsubCategoryid;
                            Os.warehouse_id = itemoldData.warehouse_id;
                            Os.SupplierId = itemoldData.SupplierId;
                            Os.SUPPLIERCODES = itemoldData.SUPPLIERCODES;
                            Os.CompanyId = itemoldData.CompanyId;
                            Os.CategoryName = itemoldData.CategoryName;
                            Os.BaseCategoryid = itemoldData.BaseCategoryid;
                            Os.BaseCategoryName = itemoldData.BaseCategoryName;
                            Os.SubcategoryName = itemoldData.SubcategoryName;
                            Os.SubsubcategoryName = itemoldData.SubsubcategoryName;
                            Os.SupplierName = itemoldData.SupplierName;
                            Os.itemname = itemoldData.itemname;
                            Os.itemcode = itemoldData.itemcode;
                            Os.SellingUnitName = itemoldData.SellingUnitName;
                            Os.PurchaseUnitName = itemoldData.PurchaseUnitName;
                            Os.price = itemoldData.price;
                            Os.VATTax = itemoldData.VATTax;
                            Os.active = itemoldData.active;
                            Os.LogoUrl = itemoldData.LogoUrl;
                            Os.CatLogoUrl = itemoldData.CatLogoUrl;
                            Os.MinOrderQty = itemoldData.MinOrderQty;
                            Os.PurchaseMinOrderQty = itemoldData.PurchaseMinOrderQty;
                            Os.GruopID = itemoldData.GruopID;
                            Os.TGrpName = itemoldData.TGrpName;
                            Os.Discount = itemoldData.Discount;
                            Os.UnitPrice = itemoldData.UnitPrice;
                            Os.Number = itemoldData.Number;
                            Os.PurchaseSku = itemoldData.PurchaseSku;
                            Os.SellingSku = itemoldData.SellingSku;
                            Os.PurchasePrice = itemoldData.PurchasePrice;
                            Os.GeneralPrice = itemoldData.GeneralPrice;
                            Os.title = itemoldData.title;
                            Os.Description = itemoldData.Description;
                            Os.StartDate = itemoldData.StartDate;
                            Os.EndDate = itemoldData.EndDate;
                            Os.PramotionalDiscount = itemoldData.PramotionalDiscount;
                            Os.TotalTaxPercentage = itemoldData.TotalTaxPercentage;
                            Os.WarehouseName = itemoldData.WarehouseName;
                            // Os.CreatedDate = itemoldData.CreatedDate;
                            Os.CreatedDate = DateTime.Now;
                            Os.UpdatedDate = itemoldData.UpdatedDate;
                            Os.Deleted = itemoldData.Deleted;
                            Os.IsDailyEssential = itemoldData.IsDailyEssential;
                            Os.SellingPrice = itemoldData.UnitPrice;
                            Os.StoringItemName = itemoldData.StoringItemName;
                            Os.SizePerUnit = itemoldData.SizePerUnit;
                            Os.HindiName = itemoldData.HindiName;
                            Os.Barcode = itemoldData.Barcode;
                            Os.promoPoint = itemoldData.promoPoint;
                            Os.marginPoint = itemoldData.marginPoint;
                            ItemMasterHistoryDb.Add(Os);
                            int id = this.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error loading  item:- " + itemmaster.itemname + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
                    }
                    itemmaster.GeneralPrice = objItemMaster.GeneralPrice;
                    itemmaster.UnitPrice = objItemMaster.UnitPrice;
                    itemmaster.Discount = objItemMaster.Discount;
                    itemmaster.Categoryid = objItemMaster.Categoryid;
                    itemmaster.SubCategoryId = objItemMaster.SubCategoryId;
                    itemmaster.SubsubCategoryid = objItemMaster.SubsubCategoryid;
                    try
                    {
                        itemmaster.PurchaseUnitName = objItemMaster.PurchaseUnitName;
                        itemmaster.SellingUnitName = objItemMaster.SellingUnitName;
                        itemmaster.TGrpName = Tg.TGrpName;
                        itemmaster.GruopID = Tg.GruopID;
                      
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                   
                    itemmaster.CategoryName = cit.CategoryName;
                    
                    itemmaster.MinOrderQty = objItemMaster.MinOrderQty;
                    itemmaster.SupplierId = objItemMaster.SupplierId;
                    itemmaster.SupplierName = SN.Name;
                    try
                    {
                        if (objItemMaster.Cityid != 0)
                        {
                            itemmaster.Cityid = objItemMaster.Cityid;
                            itemmaster.CityName = citym.CityName;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    itemmaster.TotalTaxPercentage = TotalTax;
                    itemmaster.TotalTaxPercentage = TotalTax;
                    if (objItemMaster.LogoUrl != null)
                    {
                        itemmaster.LogoUrl = objItemMaster.LogoUrl;
                        try
                        {
                            convertitemimage(objItemMaster.LogoUrl, objItemMaster.SellingSku);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                    }
                    else
                    {
                        itemmaster.LogoUrl = "http://ec2-34-208-118-110.us-west-2.compute.amazonaws.com:8888/../../images/itemimages/" + itemmaster.SellingSku + ".jpg";
                    }
                    itemmaster.CatLogoUrl = cit.LogoUrl;
                    itemmaster.SubcategoryName = cita.SubcategoryName;
                    itemmaster.SubsubcategoryName = st.SubsubcategoryName;
                    itemmaster.itemname = objItemMaster.itemname;
                    itemmaster.price = objItemMaster.price;
                    itemmaster.PurchasePrice = objItemMaster.PurchasePrice;
                    itemmaster.HindiName = objItemMaster.HindiName;
                    itemmaster.PurchaseMinOrderQty = objItemMaster.PurchaseMinOrderQty;
                    itemmaster.IsDailyEssential = objItemMaster.IsDailyEssential;
                    itemmaster.active = objItemMaster.active;
                    itemmaster.Deleted = objItemMaster.Deleted;
                    itemmaster.UpdatedDate = indianTime;
                    itemmaster.Barcode = objItemMaster.Barcode;
                    itemmaster.Margin = objItemMaster.Margin;
                    itemmaster.promoPerItems = objItemMaster.promoPerItems;
                    itemmaster.free = objItemMaster.free;
                    itemmaster.HSNCode = objItemMaster.HSNCode;

                    if (itemmaster.promoPoint != objItemMaster.promoPoint) {
                        var pp = 0;
                        if (itemmaster.promoPoint > 0)
                            pp = (objItemMaster.promoPoint - itemmaster.promoPoint).GetValueOrDefault();
                        else
                            pp = objItemMaster.promoPoint.GetValueOrDefault(); 
                        if(pp != 0)
                        {
                            var suppPromo = supplierPointDb.Where(c => c.SupplierCode == itemmaster.SUPPLIERCODES).SingleOrDefault();
                            if (suppPromo != null) {
                                suppPromo.PromoPoint -= pp;
                                supplierPointDb.Attach(suppPromo);
                                this.Entry(suppPromo).State = EntityState.Modified;
                                this.SaveChanges();
                            }
                        }
                        itemmaster.promoPoint = objItemMaster.promoPoint;
                    }
                    if (itemmaster.Margin > 0)
                    {
                        var rs = RetailerShareDb.Where(r => r.cityid == itemmaster.Cityid).FirstOrDefault();
                        if (rs != null)
                        {
                            var cf = RPConversionDb.FirstOrDefault();
                            try
                            {
                                double mv = (itemmaster.PurchasePrice * (itemmaster.Margin / 100) * (rs.share / 100) * cf.point);
                                var value = Math.Round(mv, MidpointRounding.AwayFromZero);
                                itemmaster.marginPoint = Convert.ToInt32(value);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message);
                            }
                        }
                    }

                    itemMasters.Attach(itemmaster);
                    this.Entry(itemmaster).State = EntityState.Modified;
                    this.SaveChanges();

                    AngularJSAuthentication.API.Helper.refreshItemMaster(itemmaster.warehouse_id, ids);
                    AngularJSAuthentication.API.Helper.refreshItemMaster(itemmaster.warehouse_id);

                    try
                    {
                        CurrentStock cntstock = DbCurrentStock.Where(x => x.ItemNumber == itemmaster.Number && x.Warehouseid == itemmaster.warehouse_id).SingleOrDefault();
                        if (cntstock == null)
                        {
                            CurrentStock newCstk = new CurrentStock();
                            newCstk.ItemId = itemmaster.ItemId;
                            newCstk.ItemName = itemmaster.itemname;
                            newCstk.ItemNumber = itemmaster.Number;
                            newCstk.Barcode = itemmaster.Barcode;
                            newCstk.Warehouseid = itemmaster.warehouse_id;
                            newCstk.WarehouseName = itemmaster.WarehouseName;
                            newCstk.CurrentInventory = 0;
                            newCstk.CreationDate = indianTime;
                            newCstk.UpdatedDate = indianTime;
                            DbCurrentStock.Add(newCstk);
                            this.SaveChanges();
                        }
                        else
                        {
                            cntstock.ItemName = itemmaster.itemname;
                            cntstock.ItemNumber = itemmaster.Number;
                            cntstock.Barcode = itemmaster.Barcode;
                            cntstock.Warehouseid = itemmaster.warehouse_id;
                            cntstock.WarehouseName = itemmaster.WarehouseName;
                            cntstock.UpdatedDate = indianTime;
                            cntstock.Deleted = false;
                            DbCurrentStock.Attach(cntstock);
                            this.Entry(cntstock).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    } 
                }
                else
                {
                    return objItemMaster;
                }

            }
            return objItemMaster;
        }        
        public IList<ItemMaster> filteredItemMaster(string cityid, string categoryid, string subcategoryid, string subsubcategoryid)
        {
            int CityID = Convert.ToInt32(cityid.Trim());
            int CategoryID = Convert.ToInt32(categoryid.Trim());
            int SubCategoryID = Convert.ToInt32(subcategoryid.Trim());
            int SubSubCategoryID = Convert.ToInt32(subsubcategoryid.Trim());

            if (cityid == "undefined" || cityid == null || cityid == "0")
            {
                if (categoryid == "undefined" || categoryid == null || categoryid == "0")
                {
                    var filteredlist = (from od in itemMasters where od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted==false select od).ToList();
                    return filteredlist;
                }
                else
                {
                    var filteredlist = (from od in itemMasters where od.Categoryid == CategoryID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false select od).ToList();
                    return filteredlist;
                }
            }
            else
            {
                if (categoryid == "undefined" || categoryid == null || categoryid == "0")
                {
                    var filteredlist = (from od in itemMasters where od.Cityid == CityID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false select od).ToList();
                    return filteredlist;
                }
                else
                {
                    var filteredlist = (from od in itemMasters where od.Categoryid == CategoryID && od.Cityid == CityID && od.SubCategoryId == SubCategoryID && od.SubsubCategoryid == SubSubCategoryID && od.active == true && od.Deleted == false select od).ToList();
                    return filteredlist;
                }
            }
        }        
        public List<ItemMaster> AddItemMove(List<MoveWarehouse> item, int warehid)
        {
            List<ItemMaster> ItemList = new List<ItemMaster>();
            foreach (var i in item)
            {
                Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == warehid && x.Deleted == false).Select(x => x).FirstOrDefault();
                int id = i.ItemId;
                ItemMaster im = itemMasters.Where(x => x.ItemId.Equals(id)).Select(x => x).SingleOrDefault();
                if (im != null)
                {
                    ItemMaster objitem = new ItemMaster();
                    im.warehouse_id = warehid;
                    im.WarehouseName = warehouse.WarehouseName;
                    itemMasters.Add(im);
                    int id1 = this.SaveChanges();
                    ItemList.Add(im);
                    AngularJSAuthentication.API.Helper.refreshItemMaster(im.warehouse_id, im.Categoryid);
                }
            }
            return ItemList;
        }        
        public bool DeleteItemMaster(int id)
        {
            try
            {
                ItemMaster itemmasr = itemMasters.Where(x => x.ItemId == id && x.Deleted == false).SingleOrDefault();
                if (itemmasr != null) {
                    itemmasr.active = false;
                    itemmasr.Deleted = true;
                    itemMasters.Attach(itemmasr);
                    this.Entry(itemmasr).State = EntityState.Modified;
                    this.SaveChanges();

                    List<ItemMaster> itemList = itemMasters.Where(x => x.Number == itemmasr.Number && x.Deleted == false).ToList();
                    if(itemList.Count == 0)
                    {
                        CurrentStock cntstock = DbCurrentStock.Where(x => x.ItemNumber == itemmasr.Number && x.Warehouseid == itemmasr.warehouse_id && x.Deleted == false).SingleOrDefault();

                        if (cntstock != null)
                        {
                            cntstock.Deleted = true;
                            DbCurrentStock.Attach(cntstock);
                            this.Entry(cntstock).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }                    
                }
                AngularJSAuthentication.API.Helper.refreshItemMaster(itemmasr.warehouse_id, itemmasr.Categoryid);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //*************************** Develop by sumit**********************************************************************//
        public IEnumerable<Warehouse> AllWarehouse(int compid)
        {
            if (Warehouses.AsEnumerable().Count() > 0)
            {
                return Warehouses.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<Warehouse> warehouse = new List<Warehouse>();
                return warehouse.AsEnumerable();
            }
        }
        public Warehouse AddWarehouse(Warehouse warehouse)
        {
            List<Warehouse> warehouses = Warehouses.Where(c => c.Warehouseid.Equals(warehouse.Warehouseid) && c.Deleted == false).ToList();
            City city = Cities.Where(x => x.Cityid == warehouse.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State St = States.Where(x => x.Stateid == warehouse.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();
            TaxGroup Tg = DbTaxGroup.Where(x => x.GruopID == warehouse.GruopID && x.Deleted == false).Select(x => x).SingleOrDefault();
            Warehouse objWarehouse = new Warehouse();
            if (warehouses.Count == 0)
            {
                warehouse.GruopID = warehouse.GruopID;
                if (Tg != null)
                {
                    warehouse.TGrpName = Tg.TGrpName;
                }
                else
                {
                    warehouse.TGrpName = "Tax";
                }

                warehouse.CreatedBy = warehouse.CreatedBy;
                warehouse.CreatedDate = indianTime;
                warehouse.UpdatedDate = indianTime;
                warehouse.CityName = city.CityName;
                warehouse.StateName = St.StateName;
                Warehouses.Add(warehouse);
                int id = this.SaveChanges();
                return warehouse;
            }
            else
            {
                return objWarehouse;
            }
        }
        public Warehouse getwarehousebyid(int id)
        {
            return Warehouses.Where(p => p.Warehouseid == id && p.Deleted == false).SingleOrDefault();
        }
        public IEnumerable<Warehouse> AllWHouseforapp()
        {
            //var warehouse = Warehouses.Where(c => c.Mobile == mobile).AsEnumerable();
            if (Warehouses.AsEnumerable().Count() > 0)
            {
                return Warehouses.Where(x => x.Deleted == false).AsEnumerable();
            }
            else
            {
                return null;
            }
        }
        public Warehouse PutWarehouse(Warehouse objwarehouse)
        {
            Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == objwarehouse.Warehouseid && x.Deleted == false).FirstOrDefault();
            City cit = Cities.Where(x => x.Cityid == objwarehouse.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State st = States.Where(x => x.Stateid == objwarehouse.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();

            if (warehouse != null)
            {
                warehouse.UpdatedDate = indianTime;
                warehouse.Stateid = objwarehouse.Stateid;
                warehouse.Cityid = objwarehouse.Cityid;
                warehouse.CityName = cit.CityName;
                warehouse.WarehouseName = objwarehouse.WarehouseName;
                warehouse.StateName = st.StateName;
                warehouse.CreatedBy = objwarehouse.CreatedBy;
                warehouse.CreatedDate = objwarehouse.CreatedDate;
                warehouse.UpdateBy = objwarehouse.UpdateBy;
                warehouse.Address = objwarehouse.Address;
                warehouse.Email = objwarehouse.Email;
                warehouse.Phone = objwarehouse.Phone;

                Warehouses.Attach(warehouse);
                this.Entry(warehouse).State = EntityState.Modified;
                this.SaveChanges();
                return objwarehouse;
            }
            else
            {
                return objwarehouse;
            }
        }
        public bool DeleteWarehouse(int id)
        {
            try
            {
                Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == id).Where(x => x.Deleted == false).FirstOrDefault();

                warehouse.Deleted = true;
                Warehouses.Attach(warehouse);
                this.Entry(warehouse).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public WarehouseCategory Addwarehousecatxl(WarehouseCategory warehouseCategory)
        {
            List<WarehouseCategory> whcategory = DbWarehouseCategory.Where(x => x.Deleted == false).Where(c => c.WhCategoryid.Equals(warehouseCategory.WhCategoryid)).ToList();
            WarehouseCategory objWhcategory = new WarehouseCategory();
            if (whcategory.Count == 0)
            {
                warehouseCategory.CreatedBy = warehouseCategory.CreatedBy;
                warehouseCategory.CreatedDate = indianTime;
                warehouseCategory.UpdatedDate = indianTime;
                DbWarehouseCategory.Add(warehouseCategory);
                int id = this.SaveChanges();
                return warehouseCategory;
            }
            else
            {
                return objWhcategory;
            }
        }
        public List<SubCategory> Updatebrands(List<SubCategory> sub)
        {
            foreach (var k in sub)
            {
                try
                {
                    SubCategory subcat = SubCategorys.Where(s => s.SubCategoryId == k.SubCategoryId && s.Deleted == false).SingleOrDefault();
                    subcat.IsPramotional = k.IsPramotional;
                    subcat.SortOrder = k.SortOrder;
                    SubCategorys.Attach(subcat);
                    this.Entry(subcat).State = EntityState.Modified;
                    this.SaveChanges();

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            return sub;
        }
        
        public List<SubCategory> subcategorybycity(int id)
        {
            List<SubCategory> subcat = new List<SubCategory>();
            List<Warehouse> warehouse = Warehouses.Where(w => w.Cityid == id && w.Deleted == false).ToList();
            List<WarehouseCategory> cat = new List<WarehouseCategory>();
            List<SubCategory> temp = new List<SubCategory>();
            foreach (var c in warehouse)
            {
                cat = null;
                cat = DbWarehouseCategory.Where(w => w.Warehouseid == c.Warehouseid && w.Deleted == false).ToList();

                foreach (var a in cat)
                {
                    temp = null;
                    temp = SubCategorys.Where(s => s.Categoryid == a.Categoryid && s.Deleted == false).ToList();
                    foreach (var b in temp)
                    {
                        subcat.Add(b);
                    }
                }
            }
            return subcat;
        }
        public List<SubCategory> subcategorybyPramotion(int id)
        {
            List<SubCategory> subcat = new List<SubCategory>();
            List<WarehouseCategory> cat = DbWarehouseCategory.Where(w => w.Warehouseid == id && w.Deleted == false).ToList();
            List<SubCategory> temp = new List<SubCategory>();
            foreach (var a in cat)
            {
                temp = null;
                temp = SubCategorys.Where(s => s.Categoryid == a.Categoryid && s.IsPramotional == true && s.Deleted == false).OrderByDescending(p => p.SortOrder).ToList();
                foreach (var b in temp)
                {
                    subcat.Add(b);
                }
            }
            return subcat;
        }

        public List<SubCategory> subcategorybyWarehouse(int id)
        {
            List<SubCategory> subcat = new List<SubCategory>();
            List<WarehouseCategory> cat = DbWarehouseCategory.Where(w => w.Warehouseid == id && w.Deleted == false).ToList();
            List<SubCategory> temp = new List<SubCategory>();
            foreach (var a in cat)
            {
                temp = null;
                temp = SubCategorys.Where(s => s.Categoryid == a.Categoryid && s.Deleted == false).ToList();
                foreach (var b in temp)
                {
                    subcat.Add(b);
                }
            }
            return subcat;
        }
        public IEnumerable<WarehouseCategory> AllWarehouseCategory(int compid)
        {
            if (DbWarehouseCategory.AsEnumerable().Count() > 0)
            {
                return DbWarehouseCategory.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<WarehouseCategory> WarehouseCategory = new List<WarehouseCategory>();
                return WarehouseCategory.AsEnumerable();
            }
        }
        public IEnumerable<WarehouseCategory> AllWhCategory()
        {
            var warehouseCategory = from a in DbWarehouseCategory.Where(a => a.Deleted == false).Include("warehouseSubCategory.WarehouseSubsubCategory") select a;
            return warehouseCategory;
        }
        public List<WarehouseCategory> AddWarehouseCategory(List<WarehouseCategory> WHCategory, string desc)
        {
            List<WarehouseCategory> wareHouse = new List<WarehouseCategory>();
            foreach (var i in WHCategory)
            {
                List<Category> cat = Categorys.Where(c => c.CategoryName.Trim().Equals(i.CategoryName.Trim()) && c.Deleted == false).ToList();
                WarehouseCategory WCate = DbWarehouseCategory.Where(x => x.Categoryid == i.Categoryid && x.Warehouseid==i.Warehouseid).Select(x => x).SingleOrDefault();
                Warehouse objWH = Warehouses.Where(x => x.Warehouseid == i.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
                List<WarehouseCategory> objcat = new List<WarehouseCategory>();
                if (WCate == null && objWH != null)
                {
                    if (cat.Count != -1)
                    {
                        if (i.IsVisible)
                        {
                            WarehouseCategory objw = new WarehouseCategory();
                            objw.WarehouseName = objWH.WarehouseName;
                            objw.Warehouseid = i.Warehouseid;
                            objw.Categoryid = i.Categoryid;
                            objw.CategoryName = i.CategoryName;
                            objw.Stateid = i.Stateid;
                            objw.State = i.State;
                            objw.Cityid = i.Cityid;
                            objw.City = i.City;
                            objw.Discription = desc;
                            objw.CompanyId = 1;
                            objw.IsVisible = i.IsVisible;
                            objw.SortOrder = i.SortOrder;
                            objw.CreatedBy = i.CreatedBy;
                            objw.CreatedDate = indianTime;
                            objw.UpdatedDate = indianTime;
                            DbWarehouseCategory.Add(objw);
                            int id = this.SaveChanges();
                            wareHouse.Add(objw);
                        }
                    }
                }
                else
                {
                    if (!i.IsVisible)
                    {

                        WCate.IsVisible = i.IsVisible;
                        WCate.SortOrder = i.SortOrder;
                        WCate.UpdatedDate = indianTime;
                        WCate.Deleted = true;
                        DbWarehouseCategory.Attach(WCate);
                        this.Entry(WCate).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                    else {
                        WCate.IsVisible = i.IsVisible;
                        WCate.SortOrder = i.SortOrder;
                        WCate.UpdatedDate = indianTime;
                        WCate.Deleted = false;
                        DbWarehouseCategory.Attach(WCate);
                        this.Entry(WCate).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                }
            }
            AngularJSAuthentication.API.Helper.refreshItemMaster(WHCategory[0].Warehouseid);
            return wareHouse;
        }
        public List<WarehouseCategory> PutWarehouseCategory(List<WarehouseCategory> WHCategory, string desc)
        {
            List<WarehouseCategory> wareHouse = new List<WarehouseCategory>();
            int id = WHCategory[0].Warehouseid;

            List<WarehouseCategory> AllWC = DbWarehouseCategory.AsNoTracking().Where(x => x.Warehouseid == id && x.Deleted == false).Select(x => x).ToList();
            foreach (var k in AllWC)
            {
                try
                {
                    WarehouseCategory DL = new WarehouseCategory();
                    DL.WhCategoryid = k.WhCategoryid;
                    Entry(DL).State = EntityState.Deleted;

                    SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            foreach (var i in WHCategory)
            {
                List<Category> cat = Categorys.Where(c => c.CategoryName.Trim().Equals(i.CategoryName.Trim()) && c.Deleted == false).ToList();

                WarehouseCategory WCate = DbWarehouseCategory.Where(x => x.WhCategoryid == i.WhCategoryid && x.Deleted == false).Select(x => x).SingleOrDefault();

                Warehouse objWH = Warehouses.Where(x => x.Warehouseid == i.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
                List<WarehouseCategory> objcat = new List<WarehouseCategory>();
                if (WCate == null)
                {
                    if (cat.Count != -1)
                    {
                        if (i.IsVisible)
                        {
                            WarehouseCategory objw = new WarehouseCategory();
                            objw.WarehouseName = objWH.WarehouseName;
                            objw.Warehouseid = i.Warehouseid;
                            objw.Categoryid = i.Categoryid;
                            objw.CategoryName = i.CategoryName;
                            objw.Stateid = i.Stateid;
                            objw.State = i.State;
                            objw.Cityid = i.Cityid;
                            objw.City = i.City;
                            objw.Discription = desc;
                            objw.CompanyId = 1;
                            objw.IsVisible = i.IsVisible;
                            objw.SortOrder = i.SortOrder;
                            objw.CreatedBy = i.CreatedBy;
                            objw.CreatedDate = indianTime;
                            objw.UpdatedDate = indianTime;
                            DbWarehouseCategory.Add(objw);
                            int id2 = this.SaveChanges();
                            wareHouse.Add(objw);
                        }
                    }
                }
            }
            return wareHouse;
        }        
        public bool DeleteWarehouseCategory(int id)
        {
            try
            {
                WarehouseCategory objWH = DbWarehouseCategory.Where(x => x.WhCategoryid == id && x.Deleted == false).Select(x => x).FirstOrDefault();
                objWH.Deleted = true;
                DbWarehouseCategory.Attach(objWH);
                this.Entry(objWH).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public IEnumerable<WarehouseSubCategory> AllWarehouseSubCategory(int compid)
        {
            if (DbWarehouseSubCategory.AsEnumerable().Count() > 0)
            {
                return DbWarehouseSubCategory.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<WarehouseSubCategory> warehouseSubCategory = new List<WarehouseSubCategory>();
                return warehouseSubCategory.AsEnumerable();
            }
        }
        public WarehouseSubCategory AddWarehouseSubCategory(WarehouseSubCategory ObjWarehouseSubCategory)
        {
            List<SubCategory> subCategory = SubCategorys.Where(c => c.SubcategoryName.Trim().Equals(ObjWarehouseSubCategory.SubcategoryName.Trim()) && c.Deleted == false).ToList();
            SubCategory objSCN = SubCategorys.Where(x => x.SubCategoryId == ObjWarehouseSubCategory.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            Warehouse objWH = Warehouses.Where(x => x.Warehouseid == ObjWarehouseSubCategory.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State ObjState = States.Where(x => x.Stateid == ObjWarehouseSubCategory.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();
            City ObjCity = Cities.Where(x => x.Cityid == ObjWarehouseSubCategory.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();

            WarehouseSubCategory objcat = new WarehouseSubCategory();
            if (subCategory.Count == 0)
            {
                ObjWarehouseSubCategory.WarehouseName = objWH.WarehouseName;
                ObjWarehouseSubCategory.SubcategoryName = objSCN.SubcategoryName;
                ObjWarehouseSubCategory.State = ObjState.StateName;
                ObjWarehouseSubCategory.City = ObjCity.CityName;

                ObjWarehouseSubCategory.CreatedBy = objcat.CreatedBy;
                ObjWarehouseSubCategory.CreatedDate = indianTime;
                ObjWarehouseSubCategory.UpdatedDate = indianTime;

                DbWarehouseSubCategory.Add(ObjWarehouseSubCategory);
                int id = this.SaveChanges();
                return ObjWarehouseSubCategory;
            }
            else
            {
                return objcat;
            }
        }
        public WarehouseSubCategory PutWarehouseSubCategory(WarehouseSubCategory ObjWarehouseSubCategory)
        {
            WarehouseSubCategory warehouseSubCategorycategory = DbWarehouseSubCategory.Where(x => x.WhSubCategoryId == ObjWarehouseSubCategory.WhSubCategoryId).FirstOrDefault();
            SubCategory objSCN = SubCategorys.Where(x => x.SubCategoryId == ObjWarehouseSubCategory.SubCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            Warehouse objWH = Warehouses.Where(x => x.Warehouseid == ObjWarehouseSubCategory.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
            State ObjState = States.Where(x => x.Stateid == ObjWarehouseSubCategory.Stateid && x.Deleted == false).Select(x => x).FirstOrDefault();
            City ObjCity = Cities.Where(x => x.Cityid == ObjWarehouseSubCategory.Cityid && x.Deleted == false).Select(x => x).FirstOrDefault();
            
            if (warehouseSubCategorycategory != null)
            {
                warehouseSubCategorycategory.WarehouseName = objWH.WarehouseName;
                warehouseSubCategorycategory.SubcategoryName = objSCN.SubcategoryName;
                warehouseSubCategorycategory.State = ObjState.StateName;
                warehouseSubCategorycategory.City = ObjCity.CityName;

                warehouseSubCategorycategory.Discription = ObjWarehouseSubCategory.Discription;

                warehouseSubCategorycategory.UpdatedDate = indianTime;
                warehouseSubCategorycategory.CreatedBy = ObjWarehouseSubCategory.CreatedBy;
                warehouseSubCategorycategory.CreatedDate = ObjWarehouseSubCategory.CreatedDate;
                warehouseSubCategorycategory.UpdateBy = ObjWarehouseSubCategory.UpdateBy;

                DbWarehouseSubCategory.Attach(warehouseSubCategorycategory);
                this.Entry(warehouseSubCategorycategory).State = EntityState.Modified;
                this.SaveChanges();
                return ObjWarehouseSubCategory;
            }
            else
            {
                return ObjWarehouseSubCategory;
            }
        }
        public bool DeleteWarehouseSubCategory(int id)
        {
            try
            {
                WarehouseSubCategory DL = new WarehouseSubCategory();
                DL.WhSubCategoryId = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public WarehouseSubsubCategory AddWhsubsubxl(WarehouseSubsubCategory warehouseSubsubCategory)
        {
            List<WarehouseSubsubCategory> warehouseSubsubCategoryList = DbWarehousesubsubcats.Where(c => c.WhSubsubCategoryid.Equals(warehouseSubsubCategory.WhSubsubCategoryid)).ToList();
            WarehouseSubsubCategory objQuesAns = new WarehouseSubsubCategory();
            if (warehouseSubsubCategoryList.Count == 0)
            {
                warehouseSubsubCategory.CreatedBy = warehouseSubsubCategory.CreatedBy;
                warehouseSubsubCategory.CreatedDate = indianTime;
                warehouseSubsubCategory.UpdatedDate = indianTime;
                DbWarehousesubsubcats.Add(warehouseSubsubCategory);
                int id = this.SaveChanges();
                return warehouseSubsubCategory;
            }
            else
            {
                return objQuesAns;
            }
        }
        public IEnumerable<WarehouseSubsubCategory> AllWarehouseSubsubCat(int compid)
        {
            if (DbWarehousesubsubcats.AsEnumerable().Count() > 0)
            {
                return DbWarehousesubsubcats.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<WarehouseSubsubCategory> whsubsubcat = new List<WarehouseSubsubCategory>();
                return whsubsubcat.AsEnumerable();
            }
        }
        public WarehouseSubsubCategory AddWarehouseSubsubCat(WarehouseSubsubCategory whsubsubcat)
        {
            List<WarehouseSubsubCategory> whsubsubcats = DbWarehousesubsubcats.Where(c => c.SubsubcategoryName.Equals(whsubsubcat.SubsubcategoryName)).ToList();
            SubsubCategory objSSCN = SubsubCategorys.Where(x => x.Deleted == false).Where(x => x.SubsubCategoryid == whsubsubcat.SubsubCategoryid).Select(x => x).FirstOrDefault();
            Warehouse objWH = Warehouses.Where(x => x.Deleted == false).Where(x => x.Warehouseid == whsubsubcat.WhSubsubCategoryid).Select(x => x).FirstOrDefault();
            State ObjState = States.Where(x => x.Stateid == whsubsubcat.Stateid).Where(x => x.Deleted == false).Select(x => x).FirstOrDefault();
            City ObjCity = Cities.Where(x => x.Cityid == whsubsubcat.Cityid).Where(x => x.Deleted == false).Select(x => x).FirstOrDefault();
            WarehouseSubsubCategory objwhsubsubcat = new WarehouseSubsubCategory();
            if (whsubsubcats.Count == 0)
            {
                whsubsubcat.WarehouseName = objWH.WarehouseName;
                whsubsubcat.SubsubcategoryName = objSSCN.SubsubcategoryName;
                whsubsubcat.State = ObjState.StateName;
                whsubsubcat.City = ObjCity.CityName;

                whsubsubcat.CreatedBy = whsubsubcat.CreatedBy;
                whsubsubcat.CreatedDate = indianTime;
                whsubsubcat.UpdatedDate = indianTime;
                DbWarehousesubsubcats.Add(whsubsubcat);
                int id = this.SaveChanges();
                return whsubsubcat;
            }
            else
            {
                return objwhsubsubcat;
            }
        }
        public WarehouseSubsubCategory PutWarehouseSubsubCat(WarehouseSubsubCategory objwhsubsubcat)
        {
            WarehouseSubsubCategory whsscat = DbWarehousesubsubcats.Where(x => x.WhSubsubCategoryid == objwhsubsubcat.WhSubsubCategoryid).FirstOrDefault();
            WarehouseCategory whcat = DbWarehouseCategory.Where(x => x.Deleted == false).Where(x => x.WhCategoryid == objwhsubsubcat.WhCategoryid).Select(x => x).FirstOrDefault();
            WarehouseSubCategory whsubcat = DbWarehouseSubCategory.Where(x => x.WhSubCategoryId == objwhsubsubcat.WhSubCategoryid).Select(x => x).FirstOrDefault();

            if (whsscat != null)
            {
                whsscat.UpdatedDate = indianTime;
                whsscat.CreatedBy = objwhsubsubcat.CreatedBy;
                whsscat.CreatedDate = objwhsubsubcat.CreatedDate;
                whsscat.UpdateBy = objwhsubsubcat.UpdateBy;

                DbWarehousesubsubcats.Attach(whsscat);
                this.Entry(whsscat).State = EntityState.Modified;
                this.SaveChanges();
                return objwhsubsubcat;
            }
            else
            {
                return objwhsubsubcat;
            }
        }
        public bool DeletewarehouseSubsubCat(int id)
        {
            try
            {
                WarehouseSubsubCategory DL = new WarehouseSubsubCategory();
                DL.WhSubsubCategoryid = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Supplier module
        public IEnumerable<SupplierCategory> AllSupplierCategory()
        {
            if (SupplierCategory.AsEnumerable().Count() > 0)
            {
                return SupplierCategory.AsEnumerable();
            }
            else
            {
                List<SupplierCategory> SupplierCategory = new List<SupplierCategory>();
                return SupplierCategory.AsEnumerable();
            }
        }
        public SupplierCategory AddSupplierCategory(SupplierCategory suppcategory)
        {
            List<SupplierCategory> supplierCategoryList = SupplierCategory.Where(c => c.CategoryName.Trim().Equals(suppcategory.CategoryName.Trim())).ToList();
            SupplierCategory SuppCate = new SupplierCategory();
            if (supplierCategoryList.Count == 0)
            {
                SuppCate.CategoryName = suppcategory.CategoryName;
                SupplierCategory.Add(SuppCate);
                int id = this.SaveChanges();
                return suppcategory;
            }
            else
            {
                SuppCate.Exception = "Already";
                return SuppCate;
            }
        }
        public SupplierCategory PutSupplierCategory(SupplierCategory supppCategory)
        {
            SupplierCategory suppCat = SupplierCategory.Where(x => x.SupplierCaegoryId == supppCategory.SupplierCaegoryId).FirstOrDefault();
            if (suppCat != null)
            {
                suppCat.CategoryName = supppCategory.CategoryName;
                SupplierCategory.Attach(suppCat);
                this.Entry(suppCat).State = EntityState.Modified;
                this.SaveChanges();
                return suppCat;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteSupplierCategory(int id)
        {
            try
            {
                SupplierCategory DL = new SupplierCategory();
                DL.SupplierCaegoryId = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Supplier> AllSupplier()
        {
            if (Suppliers.AsEnumerable().Count() > 0)
            {
                return Suppliers.Where(x => x.Deleted == false).OrderBy(o=>o.Name).AsEnumerable();
            }
            else
            {
                List<Supplier> supplier = new List<Supplier>();
                return supplier.AsEnumerable();
            }
        }
        public IEnumerable<Supplier> AllSubSupplierforsupplier(int suppcate)
        {
            if (Suppliers.AsEnumerable().Count() > 0)
            {
                return Suppliers.Where(p => p.SupplierCaegoryId == suppcate && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<Supplier> supplier = new List<Supplier>();
                return supplier.AsEnumerable();
            }
        }
        public Supplier AddSupplier(Supplier sup)
        {
            SupplierCategory suppcat = SupplierCategory.Where(c => c.SupplierCaegoryId == sup.SupplierCaegoryId).FirstOrDefault();
            if (suppcat == null)
            {
                SupplierCategory sc = new SupplierCategory();

            }
            List<Supplier> supplierList = Suppliers.Where(c => c.SUPPLIERCODES.Trim().Equals(sup.SUPPLIERCODES.Trim())).ToList();
            Supplier supplier = new Supplier();
            if (supplierList.Count == 0)
            {
                Suppliers.Add(sup);
                int id = this.SaveChanges();
                return sup;
            }
            else
            {
                var s = supplierList[0];
                s.Deleted = false;
                Suppliers.Attach(s);
                this.Entry(s).State = EntityState.Modified;
                this.SaveChanges();
                supplier.Exception = "Already";
                return supplier;
            }
        }
        public Supplier PutSupplier(Supplier supp)
        {
            SupplierCategory suppcat = SupplierCategory.Where(c => c.SupplierCaegoryId == supp.SupplierCaegoryId).FirstOrDefault();
            Supplier supplier = Suppliers.Where(x => x.SupplierId == supp.SupplierId).FirstOrDefault();
            if (supplier != null)
            {
                supplier.Name = supp.Name;
                supplier.CategoryName = suppcat.CategoryName;
                supplier.PhoneNumber = supp.PhoneNumber;
                supplier.Avaiabletime = supp.Avaiabletime;
                supplier.rating = supp.rating;
                supplier.BillingAddress = supp.BillingAddress;
                supplier.ShippingAddress = supp.ShippingAddress;
                supplier.Comments = supp.Comments;
                supplier.TINNo = supp.TINNo;
                supplier.OfficePhone = supp.OfficePhone;
                supplier.MobileNo = supp.MobileNo;
                supplier.EmailId = supp.EmailId;
                supplier.WebUrl = supp.WebUrl;
                supplier.SalesManager = supp.SalesManager;
                supplier.ContactPerson = supp.ContactPerson;
                supplier.ContactImage = supp.ContactImage;

                Suppliers.Attach(supplier);
                this.Entry(supplier).State = EntityState.Modified;
                this.SaveChanges();
                return supplier;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteSupplier(int id)
        {
            try
            {
                Supplier supplier = Suppliers.Where(x => x.SupplierId == id && x.Deleted == false).FirstOrDefault();
                supplier.Deleted = true;
                Suppliers.Attach(supplier);
                this.Entry(supplier).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        //***************************************************************************************************************
        public IEnumerable<State> Allstates(int compid)
        {
            if (States.AsEnumerable().Count() > 0)
            {
                return States.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<State> state = new List<State>();
                return state.AsEnumerable();
            }
        }
        public State AddState(State state)
        {
            List<State> states = States.Where(c => c.StateName.Trim().Equals(state.StateName.Trim()) && c.Deleted == false).ToList();
            State objState = new State();
            if (states.Count == 0)
            {
                state.CreatedBy = state.CreatedBy;
                state.CreatedDate = indianTime;
                state.UpdatedDate = indianTime;
                States.Add(state);
                int id = this.SaveChanges();
                return state;
            }
            else
            {
                return objState;
            }
        }
        public State PutState(State objState)
        {

            State states = States.Where(x => x.Stateid == objState.Stateid && x.Deleted == false).FirstOrDefault();
            if (states != null)
            {
                states.UpdatedDate = indianTime;
                states.StateName = objState.StateName;
                states.AliasName = objState.AliasName;

                states.CreatedBy = objState.CreatedBy;
                states.CreatedDate = objState.CreatedDate;
                states.UpdateBy = objState.UpdateBy;

                States.Attach(states);
                this.Entry(states).State = EntityState.Modified;
                this.SaveChanges();
                return objState;
            }
            else
            {
                return objState;
            }
        }
        public bool DeleteState(int id)
        {
            try
            {
                State states = States.Where(x => x.Stateid == id && x.Deleted == false).FirstOrDefault();
                states.Deleted = true;
                States.Attach(states);
                this.Entry(states).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<City> AllCitys(int compid)
        {
            if (Cities.AsEnumerable().Count() > 0)
            {
                return Cities.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<City> city = new List<City>();
                return city.AsEnumerable();
            }
        }
        public IEnumerable<City> AllCity(int sid)
        {
            if (SubsubCategorys.AsEnumerable().Count() > 0)
            {
                return Cities.Where(p => p.Stateid == sid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<City> city = new List<City>();
                return city.AsEnumerable();
            }
        }
        public City AddCity(City city)
        {
            State states = States.Where(c => c.Stateid == city.Stateid && c.Deleted == false).FirstOrDefault();
            List<City> citys = Cities.Where(c => c.CityName.Trim().Equals(city.CityName.Trim()) && c.Deleted == false).ToList();
            City objCity = new City();
            if (citys.Count == 0)
            {
                city.CreatedBy = city.CreatedBy;
                city.CreatedDate = indianTime;
                city.UpdatedDate = indianTime;
                city.StateName = states.StateName;
                Cities.Add(city);
                int id = this.SaveChanges();
                return city;
            }
            else
            {
                return objCity;
            }
        }
        public City PutCity(City objcity)
        {

            City citys = Cities.Where(x => x.Cityid == objcity.Cityid && x.Deleted == false).FirstOrDefault();
            if (citys != null)
            {
                citys.UpdatedDate = indianTime;
                citys.CityName = objcity.CityName;
                citys.aliasName = objcity.aliasName;
                citys.Code = objcity.Code;

                citys.CreatedBy = objcity.CreatedBy;
                citys.CreatedDate = objcity.CreatedDate;
                citys.UpdateBy = objcity.UpdateBy;

                Cities.Attach(citys);
                this.Entry(citys).State = EntityState.Modified;
                this.SaveChanges();
                return objcity;
            }
            else
            {
                return objcity;
            }
        }
        public bool DeleteCity(int id)
        {
            try
            {
                City citys = Cities.Where(x => x.Cityid == id).FirstOrDefault();
                citys.Deleted = true;
                Cities.Attach(citys);
                this.Entry(citys).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }        
        public IEnumerable<FinancialYear> AllFinancialYear(int compid)
        {

            if (DbItemBrand.AsEnumerable().Count() > 0)
            {
                return DbFinacialYear.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else

            {
                List<FinancialYear> fyear = new List<FinancialYear>();
                return fyear.AsEnumerable();
            }

        }
        public FinancialYear AddFinancialYear(FinancialYear financialYear)
        {
            List<FinancialYear> fyears = DbFinacialYear.Where(c => c.Financialyearid.Equals(financialYear.Financialyearid)).ToList();
            FinancialYear objfyear = new FinancialYear();
            if (fyears.Count == 0)
            {
                financialYear.StartDate = financialYear.StartDate;
                financialYear.EndDate = financialYear.EndDate;
                financialYear.CreatedBy = financialYear.CreatedBy;
                financialYear.CreatedDate = indianTime;
                financialYear.UpdatedDate = indianTime;
                DbFinacialYear.Add(financialYear);
                int id = this.SaveChanges();
                return financialYear;
            }
            else
            {
                return objfyear;
            }
        }
        public FinancialYear PutFinancialYear(FinancialYear objFinancialYear)
        {
            FinancialYear fyears = DbFinacialYear.Where(x => x.Financialyearid == objFinancialYear.Financialyearid).FirstOrDefault();
            if (fyears != null)
            {
                fyears.UpdatedDate = indianTime;
                fyears.StartDate = objFinancialYear.StartDate;
                fyears.EndDate = objFinancialYear.EndDate;

                fyears.CreatedBy = objFinancialYear.CreatedBy;
                fyears.CreatedDate = objFinancialYear.CreatedDate;
                fyears.UpdateBy = objFinancialYear.UpdateBy;

                DbFinacialYear.Attach(fyears);
                this.Entry(fyears).State = EntityState.Modified;
                this.SaveChanges();
                return objFinancialYear;
            }
            else
            {
                return objFinancialYear;
            }
        }
        public bool DeleteFinancialYear(int id)
        {
            try
            {
                FinancialYear DL = new FinancialYear();
                DL.Financialyearid = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CustomerRegistration> Allcustomers()
        {
            if (CustomerRegistrations.AsEnumerable().Count() > 0)
            {
                return CustomerRegistrations.AsEnumerable();
            }
            else
            {
                List<Category> category = new List<Category>();
                return CustomerRegistrations.AsEnumerable();
            }
        }
        
        public Customer getcustomers(string mobile)
        {
            try
            {
                Customer customers = Customers.Where(c => c.Mobile.Trim().Equals(mobile.Trim()) && c.Deleted == false).SingleOrDefault();
                if (customers == null)
                {
                    return null;
                }
                else
                {
                    return customers;
                }
            }
            catch
            {
                return null;
            }
        }

        public Customer getAllcustomers(string Mobile, string Password)
        {
            try
            {
                Customer customers = Customers.Where(c => c.Mobile.Trim().Equals(Mobile.Trim()) && c.Deleted == false).Where(d => d.Password.Trim().Equals(Password.Trim())).SingleOrDefault();
                if (customers == null)
                {
                    return null;
                }
                else
                {
                    return customers;
                }
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<ItemPramotions> AllItemPramotion()
        {
            if (itempramotions.AsEnumerable().Count() > 0)
            {
                return itempramotions.AsEnumerable();
            }
            else
            {
                return null;
            }
        }
        
        public ItemPramotions AddPramotion(ItemMaster item)
        {
            ItemPramotions it = itempramotions.Where(x => x.ItemId == item.ItemId).Select(x => x).SingleOrDefault();

            ItemMaster i = itemMasters.Where(x => x.ItemId.Equals(item.ItemId)).Select(x => x).SingleOrDefault();
            ItemPramotions itemprom = new ItemPramotions();
            if (it == null)
            {

                itemprom.ItemId = item.ItemId;
                itemprom.ItemName = i.itemname;
                itemprom.PramotionalDiscount = item.PramotionalDiscount;
                itemprom.StartDate = Convert.ToDateTime(item.StartDate);
                itemprom.EndDate = Convert.ToDateTime(item.EndDate);
                itemprom.title = item.title;
                itemprom.Warehouseid = item.warehouse_id;
                itemprom.IsActive = true;

                itempramotions.Add(itemprom);
                int id = this.SaveChanges();
                
                return it;
            }
            else
            {
                return null;
            }
        }
        public ItemMaster AddItemPramotion(ItemMaster item)
        {
            ItemMaster it = itemMasters.Where(i => i.ItemId.Equals(item.ItemId)).FirstOrDefault();           
            if (it != null)
            {
                it.title = item.title;
                it.Description = item.Description;
                it.StartDate = item.StartDate;
                it.EndDate = item.EndDate;
                it.PramotionalDiscount = item.PramotionalDiscount;
                
                itemMasters.Attach(it);
                this.Entry(it).State = EntityState.Modified;
                this.SaveChanges();
                return it;
            }
            else
            {
                return null;
            }
        }
        public Customer CustomerRegistration(Customer Cust)
        {

            Customer customer = Customers.Where(c => c.Mobile.Trim().Equals(Cust.Mobile.Trim())).SingleOrDefault();
            List<Warehouse> citywarehouse = Warehouses.Where(w => w.CityName.Trim().ToLower().Equals(Cust.City.Trim().ToLower()) && w.Deleted == false).ToList();
            if (customer == null)
            {
                if (Cust.BillingAddress == null && Cust.BAGPSCoordinates == null)
                {
                    return Cust;
                }
                else {
                    Cust.Warehouseid = 0;
                    Cust.Skcode = this.skcode();
                    Cust.ClusterId = 1;
                    var clstr = Clusters.Where(x => x.ClusterId == Cust.ClusterId).SingleOrDefault();
                    if (clstr != null)
                    {
                        Cust.ClusterId = clstr.ClusterId;
                        Cust.ClusterName = clstr.ClusterName;
                    }
                    else
                    {
                        Cluster fclstr = Clusters.FirstOrDefault();
                        Cust.ClusterId = fclstr.ClusterId;
                        Cust.ClusterName = fclstr.ClusterName;
                    }
                    if (Cust.Day == null)
                    {
                        Cust.Day = "";
                    }
                    Cust.CreatedDate = indianTime;
                    Cust.UpdatedDate = indianTime;
                    Cust.Deleted = false;
                    Customers.Add(Cust);
                    int id = this.SaveChanges();
                    return Cust;
                }
            }
            else
            {
                customer.Emailid = Cust.Emailid;
                customer.Name = Cust.Name;
                customer.Password = Cust.Password;
                customer.ShippingAddress = Cust.ShippingAddress;
                customer.BillingAddress = Cust.BillingAddress;
                if (Cust.City != null)
                {
                    customer.City = Cust.City;
                }
                if (Cust.State != null)
                {
                    customer.State = Cust.State;
                }
                if (Cust.Country != null)
                {
                    customer.Country = Cust.Country;
                }
                if (Cust.BAGPSCoordinates != null)
                {
                    customer.BAGPSCoordinates = Cust.BAGPSCoordinates;
                }

                customer.ZipCode = Cust.ZipCode;
                customer.LandMark = Cust.LandMark;
                customer.Deleted = false;

                Customers.Attach(customer);
                this.Entry(customer).State = EntityState.Modified;
                this.SaveChanges();
                return customer;
            }
        }
        // customer update        
        public Customer CustomerUpdate(Customer Cust)
        {
            Customer customer = Customers.Where(c => c.Mobile.Trim().Equals(Cust.Mobile.Trim()) && c.Deleted == false).SingleOrDefault();
            List<Warehouse> citywarehouse = Warehouses.Where(w => w.CityName.Trim().ToLower().Equals(Cust.City.Trim().ToLower()) && w.Deleted == false).ToList();
            try
            {
                if (customer != null)
                {
                    customer.Emailid = Cust.Emailid;
                    customer.Name = Cust.Name;
                    customer.Password = Cust.Password;
                    customer.ShippingAddress = Cust.ShippingAddress;
                    customer.BillingAddress = Cust.BillingAddress;
                    if (Cust.City != null)
                    {
                        customer.City = Cust.City;
                    }
                    if (Cust.State != null)
                    {
                        customer.State = Cust.State;
                    }
                    if (Cust.Country != null)
                    {
                        customer.Country = Cust.Country;
                    }
                    if (Cust.BAGPSCoordinates != null)
                    {
                        customer.BAGPSCoordinates = Cust.BAGPSCoordinates;
                    }

                    customer.ZipCode = Cust.ZipCode;
                    customer.LandMark = Cust.LandMark;

                    Customers.Attach(customer);
                    this.Entry(customer).State = EntityState.Modified;
                    this.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return customer;
        }

        public People CheckPeople(string mob,string password)
        {
            People check = new People();
            check = Peoples.Where(p => p.Mobile == mob && p.Password == password && p.Deleted == false).SingleOrDefault();
            if (check != null)
            {
                return check;
            }
            else
            {
                return null;
            }
        }
        public CustomerRegistration PutCustomerRegistration(CustomerRegistration customer)
        {
            CustomerRegistration cust = CustomerRegistrations.Where(x => x.Mobile == customer.Mobile).FirstOrDefault();
            if (cust != null)
            {
                cust.City = customer.City;
                cust.Country = customer.Country;
                cust.State = customer.State;
                cust.GeoLocation = customer.GeoLocation;
                cust.ZipCode = customer.ZipCode;
                cust.Address = customer.Address;
                cust.Warehouseid = 6;
                CustomerRegistrations.Attach(cust);
                this.Entry(cust).State = EntityState.Modified;
                this.SaveChanges();
                return cust;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<ItemBrand> AllItemBrand(int compid)
        {
            if (DbItemBrand.AsEnumerable().Count() > 0)
            {
                return DbItemBrand.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<ItemBrand> itembname = new List<ItemBrand>();
                return itembname.AsEnumerable();
            }
        }
        public ItemBrand AddItemBrand(ItemBrand itembrand)
        {
            List<ItemBrand> itembrands = DbItemBrand.Where(c => c.ItemBrandName.Trim().Equals(itembrand.ItemBrandName.Trim())).ToList();
            ItemBrand objItemBrand = new ItemBrand();
            if (itembrands.Count == 0)
            {
                itembrand.CreatedBy = itembrand.CreatedBy;
                itembrand.CreatedDate = indianTime;
                itembrand.UpdatedDate = indianTime;
                DbItemBrand.Add(itembrand);
                int id = this.SaveChanges();
                return itembrand;
            }
            else
            {
                return objItemBrand;
            }
        }
        public ItemBrand PutItemBrand(ItemBrand objitembrand)
        {

            ItemBrand itembrands = DbItemBrand.Where(x => x.ItemBrandid == objitembrand.ItemBrandid).FirstOrDefault();
            if (itembrands != null)
            {
                itembrands.UpdatedDate = indianTime;
                itembrands.ItemBrandName = objitembrand.ItemBrandName;

                itembrands.CreatedBy = objitembrand.CreatedBy;
                itembrands.CreatedDate = objitembrand.CreatedDate;
                itembrands.UpdateBy = objitembrand.UpdateBy;

                DbItemBrand.Attach(itembrands);
                this.Entry(itembrands).State = EntityState.Modified;
                this.SaveChanges();
                return objitembrand;
            }
            else
            {
                return objitembrand;
            }
        }
        public bool DeleteItemBrand(int id)
        {
            try
            {
                ItemBrand DL = new ItemBrand();
                DL.ItemBrandid = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //=======================================================================================================================================//
        //.............Customer...................//     
        public IEnumerable<Customer> AllCustomerbyCompanyId(int cmpid)
        {
            return Customers.Where(c => c.CompanyId == cmpid && c.Deleted == false).AsEnumerable();
        }
        public IEnumerable<Customer> AllCustomers
        {
            get { return Customers.Where(x => x.Deleted == false).AsEnumerable(); }

        }
        public Customer AddCustomer(Customer customer)
        {
            customer.Skcode = this.skcode();
            List<Customer> customers = Customers.Where(c => c.Mobile.Trim().Equals(customer.Mobile.Trim()) || c.Skcode == customer.Skcode).ToList();
            CustomerCategory cat = CustomerCategorys.Where(x => x.CustomerCategoryId == customer.CustomerCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == customer.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
            Customer objCustomer = new Customer();
            Cluster clstr = Clusters.Where(x => x.ClusterId == customer.ClusterId && x.Deleted == false).SingleOrDefault();
            City city = Cities.Where(x => x.Cityid == customer.Cityid && x.Deleted == false).SingleOrDefault();
            if (customers.Count == 0)
            {
                
                customer.City = city.CityName;
                customer.Cityid = city.Cityid;
                if(warehouse != null)
                {
                    customer.WarehouseName = warehouse.WarehouseName;
                }
                else 
                {
                    customer.WarehouseName = "No Warehouse";
                }
                if (clstr != null)
                {
                    customer.ClusterId = clstr.ClusterId;
                    customer.ClusterName = clstr.ClusterName;
                }
                else
                {
                    Cluster fclstr = Clusters.FirstOrDefault();
                    customer.ClusterId = fclstr.ClusterId;
                    customer.ClusterName = fclstr.ClusterName;
                }
                if (customer.Day == null)
                {
                    customer.Day = "";
                }
                customer.State = warehouse.StateName;
                customer.CreatedDate = indianTime;
                customer.UpdatedDate = indianTime;

                customer.Active = false;
                Customers.Add(customer);
                int id = this.SaveChanges();
                return customer;
            }
            else
            {
                objCustomer.Exception = "Already";
                return objCustomer;
            }
        }
       
        public Customer PutCustomer(Customer customer)
        {            
            Customer cust = Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            CustomerCategory cat = CustomerCategorys.Where(x => x.CustomerCategoryId == customer.CustomerCategoryId && x.Deleted == false).Select(x => x).FirstOrDefault();
            Warehouse warehouse = Warehouses.Where(x => x.Warehouseid == customer.Warehouseid && x.Deleted == false).Select(x => x).FirstOrDefault();
            City city = Cities.Where(x => x.Cityid == customer.Cityid && x.Deleted == false).SingleOrDefault();
            Cluster clstr = Clusters.Where(x => x.ClusterId == customer.ClusterId && x.Deleted == false).SingleOrDefault();
            if (cust != null)
            {
                City cit = Cities.Where(x => x.Cityid == customer.Cityid && x.Deleted == false).SingleOrDefault();
                cust.UpdatedDate = indianTime;
                if (warehouse != null)
                {
                    cust.Warehouseid = warehouse.Warehouseid;
                    cust.WarehouseName = warehouse.WarehouseName;
                }               
                else if (customer.Warehouseid == 0)
                {
                    cust.Warehouseid = customer.Warehouseid;
                    cust.WarehouseName = "No Warehouse";
                }
                if (clstr != null)
                {
                    cust.ClusterId = clstr.ClusterId;
                    cust.ClusterName = clstr.ClusterName;
                }
                else 
                {
                    Cluster fclstr = Clusters.FirstOrDefault();
                    cust.ClusterId = fclstr.ClusterId;
                    cust.ClusterName = fclstr.ClusterName;
                }
                cust.Name = customer.Name;
                //cust.Skcode = customer.Skcode;
                cust.Password = customer.Password;
                cust.LandMark = customer.LandMark;
                cust.Description = customer.Description;
                cust.CustomerType = customer.CustomerType;
                cust.BillingAddress = customer.BillingAddress;
                cust.ShippingAddress = customer.ShippingAddress;
                if (customer.ExecutiveId != null)
                {
                    cust.ExecutiveId = customer.ExecutiveId;
                }
                if (city != null)
                {
                    cust.Cityid = city.Cityid;
                    cust.City = city.CityName;
                }
                cust.ShopName = customer.ShopName;
                cust.BAGPSCoordinates = customer.BAGPSCoordinates;
                cust.SAGPSCoordinates = customer.SAGPSCoordinates;
                cust.RefNo = customer.RefNo;
                cust.Mobile = customer.Mobile;
                cust.Emailid = customer.Emailid;
                cust.Familymember = customer.Familymember;
                if (customer.imei != null)
                {
                    cust.imei = customer.imei;
                }
                cust.LastModifiedBy = customer.LastModifiedBy;
                cust.Active = customer.Active;               
                cust.ClusterId = customer.ClusterId;
                cust.lat = customer.lat;
                cust.lg = customer.lg;

                Customers.Attach(cust);
                this.Entry(cust).State = EntityState.Modified;
                this.SaveChanges();
                return cust;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteCustomer(int id)
        {
            try
            {
                Customer cust = Customers.Where(x => x.CustomerId == id && x.Deleted == false).FirstOrDefault();
                cust.Deleted = true;
                cust.Active = false;
                Customers.Attach(cust);
                this.Entry(cust).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //.............End Customer...................//
        public IEnumerable<Project> AllProjects
        {
            get
            {
                if (Projects.AsEnumerable().Count() > 0)
                {
                    return Projects.AsEnumerable();
                }
                else
                {
                    List<Project> project = new List<Project>();
                    return project.AsEnumerable();
                }
            }
        }
        public IEnumerable<Project> AllProjectsbyCompanyId(int cmpid)
        {
            return Projects.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }
        public Project AddProject(Project project)
        {
            List<Project> projects = Projects.Where(c => c.ProjectName.Trim().Equals(project.ProjectName.Trim())).ToList();
            Project objProject = new Project();
            if (projects.Count == 0)
            {
                project.CreatedBy = project.CreatedBy;
                project.CreatedDate = indianTime;
                project.UpdatedDate = indianTime;
                Projects.Add(project);
                int id = this.SaveChanges();
                return project;
            }
            else
            {
                return objProject;
            }
        }
        public Project PutProject(Project project)
        {
            Project proj = Projects.Where(x => x.ProjectID == project.ProjectID).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = indianTime;
                proj.ProjectName = project.ProjectName;
                proj.Discription = project.Discription;
                proj.Budget = project.Budget;
                proj.StartDate = project.StartDate;
                proj.EndDate = project.EndDate;
                proj.ApproverEmail = project.ApproverEmail;
                proj.ConsultantRate = project.ConsultantRate;
                proj.EmpRate = project.EmpRate;
                proj.CustomerId = project.CustomerId;
                proj.ApproverName = project.ApproverName;
                proj.CreatedBy = project.CreatedBy;
                proj.UpdateBy = project.UpdateBy;
                Projects.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return proj;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteProject(int id)
        {
            try
            {
                Project DL = new Project();
                DL.ProjectID = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }        
        public IEnumerable<TaskType> AllTaskTypes(int compid)
        {
            return TaskTypes.Where(e => e.CompanyId == compid).AsEnumerable();
        }
        public IEnumerable<TaskType> AllTaskTypesbyCompanyId(int cmpid)
        {
            return TaskTypes.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }
        public TaskType AddTaskType(TaskType tasktype)
        {
            List<TaskType> tasktypes = TaskTypes.Where(c => c.Name.Trim().Equals(tasktype.Name.Trim()) && c.CompanyId == tasktype.CompanyId).ToList();
            TaskType objTaskType = new TaskType();
            if (tasktypes.Count == 0)
            {
                tasktype.CreateBy = tasktype.CreateBy;
                tasktype.CreatedDate = indianTime;
                tasktype.UpdatedDate = indianTime;
                TaskTypes.Add(tasktype);
                int id = this.SaveChanges();
                return tasktype;
            }
            else
            {
                return null;
            }
        }
        public TaskType PutTaskType(TaskType tasktype)
        {
            TaskType task = TaskTypes.Where(x => x.Id == tasktype.Id).FirstOrDefault();
            if (task != null)
            {
                task.UpdatedDate = indianTime;
                task.Name = tasktype.Name;
                task.Category = tasktype.Category;
                task.Desc = tasktype.Desc;
                task.UpdatedBy = tasktype.UpdatedBy;
                TaskTypes.Attach(task);
                this.Entry(task).State = EntityState.Modified;
                this.SaveChanges();
                return task;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteTaskType(int id)
        {
            try
            {
                TaskType DL = new TaskType();
                DL.Id = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Event> AllEvents(int userid)
        {
            return Events.Where(e => e.PeopleID == userid).AsEnumerable();
        }
        public Event AddEvent(Event e)
        {
            try
            {
                Events.Add(e);
                int id = this.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return e;
        }
        public Event UpdateEvent(Event e)
        {
            throw new NotImplementedException();
        }
        public bool DeleteEvent(int id)
        {
            Event e = Events.Where(c => c.Id == id).SingleOrDefault();
            if (e != null)
            {
                Entry(e).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }        
        public IEnumerable<ProjectTask> AllProjectTask
        {
            get
            {
                if (ProjectTasks.AsEnumerable().Count() > 0)
                {
                    return ProjectTasks.AsEnumerable();
                }
                else
                {
                    List<ProjectTask> projecttask = new List<ProjectTask>();
                    return projecttask.AsEnumerable();
                }
            }
        }
        public IEnumerable<ProjectTask> AllProjectTaskbyCompanyId(int cmpid)
        {
            return ProjectTasks.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }
        public ProjectTask AddProjectTask(ProjectTask projecttask)
        {
            List<ProjectTask> projecttasks = ProjectTasks.Where(c => c.TaskId.Equals(projecttask.TaskId)).ToList();
            Project prj = Projects.Where(x => x.ProjectID == projecttask.ProjectID).Select(x => x).FirstOrDefault();
            People ppl = Peoples.Where(x => x.PeopleID == projecttask.PeopleID && x.Deleted == false).Select(x => x).FirstOrDefault();
            Customer cust = Customers.Where(x => x.CustomerId == projecttask.CustomerId && x.Deleted == false).Select(x => x).FirstOrDefault();
            ProjectTask objProjectTask = new ProjectTask();

            if (projecttasks.Count == 0)
            {
                projecttask.CreatedBy = projecttask.CreatedBy;
                projecttask.CreatedDate = indianTime;
                projecttask.UpdatedDate = indianTime;
                projecttask.ProjectName = prj.ProjectName;
                projecttask.CustomerName = cust.Name;
                projecttask.PeopleID = ppl.PeopleID;
                ProjectTasks.Add(projecttask);
                int id = this.SaveChanges();
                return projecttask;
            }
            else
            {
                return objProjectTask;
            }

        }
        public ProjectTask PutProjectTask(ProjectTask objCust)
        {
            ProjectTask proj = ProjectTasks.Where(x => x.TaskId == objCust.TaskId).FirstOrDefault();
            Project prj = Projects.Where(x => x.ProjectID == objCust.ProjectID).Select(x => x).FirstOrDefault();
            Customer cust = Customers.Where(x => x.CustomerId == objCust.CustomerId && x.Deleted == false).Select(x => x).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = indianTime;
                proj.TaskTypeId = objCust.TaskTypeId;
                proj.AllocatedHours = objCust.AllocatedHours;
                proj.Priority = objCust.Priority;
                proj.Name = objCust.Name;
                proj.StartDate = objCust.StartDate;
                proj.EndDate = objCust.EndDate;
                proj.CustomerId = objCust.CustomerId;
                proj.ProjectID = objCust.ProjectID;
                proj.Assignee = objCust.Assignee;
                proj.ProjectName = prj.ProjectName;
                proj.CustomerName = cust.Name;
                proj.Discription = objCust.Discription;
                proj.CreatedBy = objCust.CreatedBy;
                proj.UpdatedDate = indianTime;
                proj.UpdateBy = objCust.UpdateBy;
                ProjectTasks.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return objCust;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteProjectTask(int id)
        {
            try
            {
                ProjectTask DL = new ProjectTask();
                DL.TaskId = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<Role> AllRoles(int compid)
        {
            if (UserRole.AsEnumerable().Count() > 0)
            {
                return UserRole.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<Role> role = new List<Role>();
                return role.AsEnumerable();
            }
        }
        public Role AddRole(Role role)
        {
            List<Role> roles = UserRole.Where(c => c.RoleTitle.Trim().Equals(role.RoleTitle.Trim())).ToList();
            Role objrole = new Role();
            if (roles.Count == 0)
            {
                role.CreatedBy = role.CreatedBy;
                role.CreatedDate = indianTime;
                role.UpdatedDate = indianTime;
                UserRole.Add(role);
                int id = this.SaveChanges();
                return role;
            }
            else
            {
                return objrole;
            }
        }
        public Role PutRoles(Role objrole)
        {

            Role roles = UserRole.Where(x => x.RoleId == objrole.RoleId).FirstOrDefault();
            if (roles != null)
            {
                roles.UpdatedDate = indianTime;
                roles.RoleTitle = objrole.RoleTitle;
                roles.CreatedBy = objrole.CreatedBy;
                roles.CreatedDate = objrole.CreatedDate;
                roles.UpdateBy = objrole.UpdateBy;

                UserRole.Attach(roles);
                this.Entry(roles).State = EntityState.Modified;
                this.SaveChanges();
                return objrole;
            }
            else
            {
                return objrole;
            }
        }
        public bool DeleteRole(int id)
        {
            try
            {
                Role DL = new Role();
                DL.RoleId = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<People> AllPeoples(int compid)
        {
            if (Peoples.AsEnumerable().Count() > 0)
            {
                List<People> person = new List<People>();
                person = Peoples.Where(e => e.CompanyID == compid && e.Deleted == false).ToList();
                return person.AsEnumerable();
            }
            else
            {
                List<People> people = new List<People>();
                return people.AsEnumerable();
            }
        }
        public IEnumerable<People> AllPeoplesDep(string dep)
        {
            if (Peoples.AsEnumerable().Count() > 0)
            {
                List<People> person = new List<People>();
                person = Peoples.Where(e => e.Department.Trim().ToLower()==dep.Trim().ToLower() && e.Deleted == false).ToList();
                return person.AsEnumerable();
            }
            else
            {
                List<People> people = new List<People>();
                return people.AsEnumerable();
            }
        }
        public People AddPeople(People people)
        {
            List<People> peoples = Peoples.Where(c => c.Email.Trim().Equals(people.Email.Trim()) && c.Deleted == false).ToList();

            People objPeople = new People();
            if (peoples.Count == 0)
            {
                people.DisplayName = people.PeopleFirstName + " " + people.PeopleLastName;

                people.CreatedBy = people.CreatedBy;
                people.CreatedDate = indianTime;
                people.UpdatedDate = indianTime;
                Peoples.Add(people);
                int id = this.SaveChanges();
                return people;
            }
            else
            {
                return objPeople;
            }
        }
        public People AddPeoplebyAdmin(People people)
        {
            List<People> peoples = Peoples.Where(c => c.Email.Trim().Equals(people.Email.Trim())&& c.Mobile == people.Mobile && c.Deleted == false).ToList();
            State state = States.Where(x => x.Stateid == people.Stateid && x.Deleted == false).Select(x => x).SingleOrDefault();
            City city = Cities.Where(x => x.Cityid == people.Cityid && x.Deleted == false).Select(x => x).SingleOrDefault();
            People objPeople = new People();
            if (peoples.Count == 0)
            {                
                people.DisplayName = people.PeopleFirstName + " " + people.PeopleLastName;
                people.state = state.StateName;
                people.city = city.CityName;
                people.CreatedBy = "Admin";
                people.CreatedDate = indianTime;
                people.UpdatedDate = indianTime;
                people.Active = true;
                Peoples.Add(people);
                int id = this.SaveChanges();
                return people;
            }
            else
            {
                return null;
            }
        }
        public People PutPeople(People objCust)
        {
            People proj = Peoples.Where(x => x.PeopleID == objCust.PeopleID && x.Deleted == false).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = indianTime;
                proj.PeopleFirstName = objCust.PeopleFirstName;
                proj.PeopleLastName = objCust.PeopleLastName;
                proj.Email = objCust.Email;
                proj.Mobile = objCust.Mobile;
                proj.Department = objCust.Department;
                proj.BillableRate = objCust.BillableRate;
                proj.CostRate = objCust.CostRate;
                proj.Permissions = objCust.Permissions;
                proj.ImageUrl = objCust.ImageUrl;
                proj.DisplayName = objCust.PeopleFirstName + " " + objCust.PeopleLastName;
                proj.CreatedBy = objCust.CreatedBy;
                proj.CreatedDate = objCust.CreatedDate;
                proj.UpdateBy = objCust.UpdateBy;
                proj.Warehouseid = objCust.Warehouseid;
                proj.Skcode = objCust.Skcode;
                Peoples.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return objCust;
            }
            else
            {
                return objCust;
            }
        }
        public bool DeletePeople(int id)
        {
            try
            {
                People proj = Peoples.Where(x => x.PeopleID == id && x.Deleted == false).FirstOrDefault();
                proj.Deleted = true;
                Peoples.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public People PutPeoplebyAdmin(People objCust)
        {
            People proj = Peoples.Where(x => x.PeopleID == objCust.PeopleID && x.Deleted == false).FirstOrDefault();
            State state = States.Where(x => x.Stateid == objCust.Stateid && x.Deleted == false).Select(x => x).SingleOrDefault();
            City city = Cities.Where(x => x.Cityid == objCust.Cityid && x.Deleted == false).Select(x => x).SingleOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = indianTime;
                proj.PeopleFirstName = objCust.PeopleFirstName;
                proj.PeopleLastName = objCust.PeopleLastName;
                proj.Email = objCust.Email;
                proj.Password = objCust.Password;
                try
                {
                    proj.state = state.StateName;
                    proj.city = city.CityName;
                }
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                }
                proj.Mobile = objCust.Mobile;
                proj.Department = objCust.Department;
                proj.BillableRate = objCust.BillableRate;
                proj.CostRate = objCust.CostRate;
                proj.Permissions = objCust.Permissions;
                proj.ImageUrl = objCust.ImageUrl;
                proj.DisplayName = objCust.PeopleFirstName + " " + objCust.PeopleLastName;
                proj.CreatedBy = objCust.CreatedBy;
                proj.CreatedDate = objCust.CreatedDate;
                proj.UpdateBy = objCust.UpdateBy;
                proj.EmailConfirmed = objCust.EmailConfirmed;
                proj.Warehouseid = objCust.Warehouseid;
                proj.Active = objCust.Active;
                Peoples.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return objCust;
            }
            else
            {
                return objCust;
            }
        }
        
        public IEnumerable<Company> AllCompanies
        {
            get { return Companies.AsEnumerable(); }
        }
        
        public Company GetCompanybyCompanyId(int id)
        {
            Company p = this.Companies.Where(c => c.Id == id).SingleOrDefault();
            if (p != null)
            {
            }
            else
            {
                p = new Company();
            }
            return p;
        }
        public Company AddCompany(Company company)
        {
            List<Company> cmp = Companies.Where(c => c.Name.Trim().Equals(company.Name.Trim())).ToList();
            if (cmp.Count == 0)
            {
                company.CreatedBy = "System";
                company.CreatedDate = indianTime;
                company.UpdatedDate = indianTime;
                Companies.Add(company);
                int id = this.SaveChanges();
                return company;
            }
            else
            {
                return cmp[0];
            }
        }
        public Company PutCompany(Company company)
        {
            Company proj = Companies.Where(x => x.Id == company.Id).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = indianTime;
                proj.AlertDay = company.AlertDay;
                proj.AlertTime = company.AlertTime;
                proj.FreezeDay = company.FreezeDay;
                proj.TFSUrl = company.TFSUrl;
                proj.TFSUserId = company.TFSUserId;
                proj.TFSPassword = company.TFSPassword;
                proj.LogoUrl = company.LogoUrl;
                proj.Address = company.Address;
                proj.CompanyName = company.CompanyName;
                proj.contactinfo = company.contactinfo;
                proj.currency = company.currency;
                proj.dateformat = company.dateformat;
                proj.fiscalyear = company.fiscalyear;
                proj.startweek = company.startweek;
                proj.timezone = company.timezone;
                proj.Webaddress = company.Webaddress;
                Companies.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return proj;
            }
            else
            {
                return null;
            }
        }
        public bool DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }
        public bool CompanyExists(string companyName)
        {
            List<Company> cmp = Companies.Where(c => c.Name.Trim().Equals(companyName.Trim())).ToList();
            Company objCompany = new Company();
            if (cmp.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public IEnumerable<AssetsCategory> AllAssetsCategory(int compid)
        {
            if (AssetsCategorys.AsEnumerable().Count() > 0)
            {
                return AssetsCategorys.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<AssetsCategory> assetscategory = new List<AssetsCategory>();
                return assetscategory.AsEnumerable();
            }
        }
        public AssetsCategory AddAssetsCategory(AssetsCategory assetscategory)
        {
            List<AssetsCategory> assetscategorys = AssetsCategorys.Where(c => c.AssetCategoryName.Trim().Equals(assetscategory.AssetCategoryName.Trim())).ToList();
            AssetsCategory objAssetsCategory = new AssetsCategory();
            if (assetscategorys.Count == 0)
            {
                assetscategory.CreatedBy = assetscategory.CreatedBy;
                assetscategory.CreatedDate = indianTime;
                assetscategory.UpdatedDate = indianTime;
                AssetsCategorys.Add(assetscategory);
                int id = this.SaveChanges();
                return assetscategory;
            }
            else
            {
                return objAssetsCategory;
            }
        }
        public AssetsCategory PutAssetsCategory(AssetsCategory objAssetCat)
        {
            AssetsCategory assetscategorys = AssetsCategorys.Where(x => x.AssetCategoryId == objAssetCat.AssetCategoryId).FirstOrDefault();
            if (assetscategorys != null)
            {
                assetscategorys.UpdatedDate = indianTime;
                assetscategorys.AssetCategoryName = objAssetCat.AssetCategoryName;
                assetscategorys.Discription = objAssetCat.Discription;

                assetscategorys.CreatedBy = objAssetCat.CreatedBy;
                assetscategorys.CreatedDate = objAssetCat.CreatedDate;
                assetscategorys.UpdateBy = objAssetCat.UpdateBy;

                AssetsCategorys.Attach(assetscategorys);
                this.Entry(assetscategorys).State = EntityState.Modified;
                this.SaveChanges();
                return objAssetCat;
            }
            else
            {
                return objAssetCat;
            }
        }
        public bool DeleteAssetsCategory(int id)
        {
            try
            {
                AssetsCategory DL = new AssetsCategory();
                DL.AssetCategoryId = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //.........CustomerCategory Start...................................//
        public IEnumerable<CustomerCategory> AllCustomerCategory(int compid)
        {
            if (CustomerCategorys.AsEnumerable().Count() > 0)
            {
                return CustomerCategorys.Where(p => p.CompanyId == compid && p.Deleted == false).AsEnumerable();
            }
            else
            {
                List<CustomerCategory> customercategory = new List<CustomerCategory>();
                return customercategory.AsEnumerable();
            }
        }

        public CustomerCategory AddCustomerCategory(CustomerCategory Cust)
        {
            List<CustomerCategory> customercategorys = CustomerCategorys.Where(c => c.CustomerCategoryName.Trim().Equals(Cust.CustomerCategoryName.Trim()) && c.Deleted == false).ToList();
            CustomerCategory objCustomerCategory = new CustomerCategory();
            if (customercategorys.Count == 0)
            {
                Cust.CreatedBy = Cust.CreatedBy;
                Cust.CreatedDate = indianTime;
                Cust.UpdatedDate = indianTime;
                CustomerCategorys.Add(Cust);
                int id = this.SaveChanges();
                return Cust;
            }
            else
            {     
                return objCustomerCategory;
            }
        }

        public CustomerCategory PutCustomerCategory(CustomerCategory Cust)
        {

            CustomerCategory customercategorys = CustomerCategorys.Where(x => x.CustomerCategoryId == Cust.CustomerCategoryId && x.Deleted == false).FirstOrDefault();
            if (customercategorys != null)
            {
                customercategorys.UpdatedDate = indianTime;
                customercategorys.CustomerCategoryName = Cust.CustomerCategoryName;

                customercategorys.CreatedBy = Cust.CreatedBy;
                customercategorys.CreatedDate = Cust.CreatedDate;
                customercategorys.UpdateBy = Cust.UpdateBy;

                CustomerCategorys.Attach(customercategorys);
                this.Entry(customercategorys).State = EntityState.Modified;
                this.SaveChanges();
                return Cust;
            }
            else
            {
                return Cust;
            }
        }

        public bool DeleteCustomerCategory(int id)
        {
            try
            {
                CustomerCategory customercategorys = CustomerCategorys.Where(x => x.CustomerCategoryId == id && x.Deleted == false).FirstOrDefault();
                customercategorys.Deleted = true;
                CustomerCategorys.Attach(customercategorys);
                this.Entry(customercategorys).State = EntityState.Modified;
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //..........CustomerCategory Start..................................//
        public IEnumerable<Assets> AllAssets(int compid)
        {
            if (Assetss.AsEnumerable().Count() > 0)
            {
                return Assetss.Where(p => p.CompanyId == compid).AsEnumerable();
            }
            else
            {
                List<Assets> assets = new List<Assets>();
                return assets.AsEnumerable();
            }
        }
        public Assets AddAssets(Assets assets)
        {
            List<Assets> assetss = Assetss.Where(c => c.AssetId.Equals(assets.AssetId)).ToList();
            AssetsCategory cat = AssetsCategorys.Where(x => x.AssetCategoryId == assets.AssetCategoryId).Select(x => x).FirstOrDefault();
            Assets objAssets = new Assets();
            if (assetss.Count == 0)
            {
                assets.CreatedBy = assets.CreatedBy;
                assets.CreatedDate = indianTime;
                assets.UpdatedDate = indianTime;
                assets.AssetCategoryName = cat.AssetCategoryName;
                Assetss.Add(assets);
                int id = this.SaveChanges();
                
                return assets;
            }
            else
            {
                return objAssets;
            }
        }
        public Assets PutAssets(Assets objAsset)
        {
            Assets assetss = Assetss.Where(x => x.AssetId == objAsset.AssetId).FirstOrDefault();
            AssetsCategory cat = AssetsCategorys.Where(x => x.AssetCategoryId == objAsset.AssetCategoryId).Select(x => x).FirstOrDefault();
            if (assetss != null)
            {
                assetss.UpdatedDate = indianTime;
                assetss.AssetCategoryId = objAsset.AssetCategoryId;
                assetss.AssetCategoryName = cat.AssetCategoryName;
                assetss.ModelNumber = objAsset.ModelNumber;
                assetss.PurchaseDate = objAsset.PurchaseDate;
                assetss.WarrantyPeriod = objAsset.WarrantyPeriod;
                assetss.VendorContactNo = objAsset.VendorContactNo;
                assetss.VendorAddress = objAsset.VendorAddress;
                assetss.FileUrl = objAsset.FileUrl;
                assetss.SerialNumber = objAsset.SerialNumber;
                assetss.PONumber = objAsset.PONumber;
                assetss.AssetCost = objAsset.AssetCost;
                assetss.CreatedBy = objAsset.CreatedBy;
                assetss.CreatedDate = objAsset.CreatedDate;
                assetss.UpdateBy = objAsset.UpdateBy;
                assetss.LastOwnership = objAsset.LastOwnership;
                assetss.LastUseDate = objAsset.LastUseDate;
                Assetss.Attach(assetss);
                this.Entry(assetss).State = EntityState.Modified;
                this.SaveChanges();
                return objAsset;
            }
            else
            {
                return objAsset;
            }
        }
        public bool DeleteAssets(int id)
        {
            try
            {
                Assets DL = new Assets();
                DL.AssetId = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Leave> AllLeaves(int compid)
        {
            return Leaves.Where(e => e.CompanyId == compid).AsEnumerable();
        }
        public Leave AddLeave(Leave leave)
        {
            leave.CreatedBy = leave.CreatedBy;
            leave.CreatedDate = indianTime;
            leave.UpdatedDate = indianTime;
            Leaves.Add(leave);
            int id = this.SaveChanges();
            return leave;
        }
        public Leave PutLeave(Leave objCust)
        {
            Leave lve = Leaves.Where(x => x.LeaveID == objCust.LeaveID).FirstOrDefault();
            if (lve != null)
            {
                lve.UpdatedDate = indianTime;
                lve.EmployeeName = objCust.EmployeeName;
                lve.Department = objCust.Department;
                lve.CellNo = objCust.CellNo;
                lve.Reason = objCust.Reason;
                lve.ReasonForAppDec = objCust.ReasonForAppDec;
                lve.IsApprove = objCust.IsApprove;
                lve.Email = objCust.Email;
                lve.CreatedDate = objCust.CreatedDate;
                lve.UpdateBy = objCust.UpdateBy;
                lve.LeaveType = objCust.LeaveType;
                Leaves.Attach(lve);
                this.Entry(lve).State = EntityState.Modified;
                this.SaveChanges();
                return objCust;
            }
            else
            {
                return objCust;
            }
        }
        public bool DeleteLeave(int id)
        {
            try
            {
                Leave DL = new Leave();
                DL.LeaveID = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public IEnumerable<TravelRequest> AllTravelRequestByCompId(int compid)
        {
            return TravelRequests.Where(e => e.CompanyId == compid).AsEnumerable();
        }
        public IEnumerable<TravelRequest> AllTravelrequestByUserId(int cmpid)
        {
            return TravelRequests.Where(c => c.PersonId == cmpid).AsEnumerable();
        }
        public TravelRequest AddTravelRequest(TravelRequest travelrequest)
        {
            travelrequest.CreatedDate = indianTime;
            travelrequest.UpdatedDate = indianTime;
            TravelRequests.Add(travelrequest);
            int id = this.SaveChanges();
            return travelrequest;
        }
        public TravelRequest PutTravelRequest(TravelRequest travelrequest)
        {
            TravelRequest req = TravelRequests.Where(x => x.Id == travelrequest.Id).FirstOrDefault();
            if (req != null)
            {
                req.UpdatedDate = indianTime;
                req.AirRequired = travelrequest.AirRequired;
                req.ArrivalCity = travelrequest.ArrivalCity;
                req.DepartingCity = travelrequest.DepartingCity;
                req.ArrivalDate = travelrequest.ArrivalDate;
                req.DepartingDate = travelrequest.DepartingDate;
                req.CompanyId = travelrequest.CompanyId;
                req.PersonId = travelrequest.PersonId;
                req.TransportRequired = travelrequest.TransportRequired;
                req.HotelRequired = travelrequest.HotelRequired;
                req.Details = travelrequest.Details;
                req.ReasonForAppDec = travelrequest.ReasonForAppDec;
                req.IsApprove = travelrequest.IsApprove;
                TravelRequests.Attach(req);
                this.Entry(req).State = EntityState.Modified;
                this.SaveChanges();
                return req;
            }
            else
            {
                return req;
            }
        }
        public bool DeleteTravelRequest(int id)
        {
            try
            {
                TravelRequest DL = new TravelRequest();
                DL.Id = id;
                Entry(DL).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public TravelRequest GetTravelRequestById(int id)
        {
            return TravelRequests.Where(c => c.Id == id).SingleOrDefault();
        }
        
        public IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate)
        {
            List<Event> events = Events.Where(e => e.EventDate <= endDate && e.EventDate >= startDate).ToList();
            return events.AsEnumerable();
        }
        public IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate, int compid)
        {
            List<Event> events = Events.Where(e => e.EventDate <= endDate && e.EventDate >= startDate && e.CompanyId == compid).ToList();
            return events.AsEnumerable();
        }
        public IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate, int userid, int compid)
        {
            List<Event> events = Events.Where(e => e.EventDate <= endDate && e.EventDate >= startDate && e.PeopleID == userid).ToList();
            return events.AsEnumerable();
        }

        public Event UpdateEventByViewModel(WeekEventViewModel model, string d, int userid, int compid)
        {
            Event e = new Event();
            switch (d)
            {
                case "d1":
                    {
                        e = this.Events.Where(c => c.Id == model.d1EventId).SingleOrDefault();
                        e.Hours = model.d1;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.TaskId = model.taskid;
                        e.ProjectId = model.projectid;
                        Project proj = this.Projects.Where(p => p.ProjectID == model.projectid).SingleOrDefault();
                        e.ClientId = proj.CustomerId;
                        e.ProjectName = model.projectname;
                        break;
                    }

                case "d2":
                    {
                        e = this.Events.Where(c => c.Id == model.d2EventId).SingleOrDefault();
                        e.Hours = model.d2;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                case "d3":
                    {
                        e = this.Events.Where(c => c.Id == model.d3EventId).SingleOrDefault();
                        e.Hours = model.d3;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                case "d4":
                    {
                        e = this.Events.Where(c => c.Id == model.d4EventId).SingleOrDefault();
                        e.Hours = model.d4;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                case "d5":
                    {
                        e = this.Events.Where(c => c.Id == model.d5EventId).SingleOrDefault();
                        e.Hours = model.d5;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                case "d6":
                    {
                        e = this.Events.Where(c => c.Id == model.d6EventId).SingleOrDefault();
                        e.Hours = model.d6;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                case "d7":
                    {
                        e = this.Events.Where(c => c.Id == model.d7EventId).SingleOrDefault();
                        e.Hours = model.d7;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Events.Attach(e);
            this.Entry(e).State = EntityState.Modified;
            this.SaveChanges();
            return e;
        }
        public Event UpdateEventByViewModel(DayEventViewModel model, string d, int userid, int compid)
        {
            Event e = new Event();
            switch (d)
            {
                case "d1":
                    {
                        e = this.Events.Where(c => c.Id == model.d1EventId).SingleOrDefault();
                        e.Hours = model.d1;
                        e.UpdatedDate = indianTime;
                        e.TaskType = model.tasktypeid;
                        e.ProjectId = model.projectid;
                        e.ProjectName = model.projectname;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Events.Attach(e);
            this.Entry(e).State = EntityState.Modified;
            this.SaveChanges();
            return e;
        }        
        public People getPersonIdfromEmail(string email)
        {
            People ps = new People();
            ps = Peoples.Where(x => x.Deleted == false).Where(p => p.Email.Trim().Equals(email.Trim())).SingleOrDefault();
            int id = 0;
            if (ps != null)
            {
                id = ps.PeopleID;
            }
            return ps;
        }
        public Customer GetClientforProjectId(int projId)
        {
            Project project = Projects.Where(p => p.ProjectID == projId).SingleOrDefault();
            Customer client = Customers.Where(p => p.CustomerId == project.CustomerId).Where(x => x.Deleted == false).SingleOrDefault(); ;
            return client;
        }
        public TaskType GetTaskTypeById(int id)
        {
            TaskType tt = TaskTypes.Where(p => p.Id == id).SingleOrDefault();
            return tt;
        }        
        public List<People> GetPeoplebyCompanyId(int id)
        {
            List<People> p = this.Peoples.Where(c => c.CompanyID == id).Where(x => x.Deleted == false).ToList();
            if (p != null)
            {
            }
            else
            {
                p = new List<People>();
            }
            return p;
        }
        public People GetPeoplebyId(int id)
        {
            People p = this.Peoples.Where(c => c.PeopleID == id).Where(x => x.Deleted == false).SingleOrDefault();
            if (p != null)
            {
            }
            else
            {
                p = new People();
            }
            return p;
        }
        public List<ProjectTask> AllProjectTaskByuserId(int userid)
        {
            List<ProjectTask> p = this.ProjectTasks.Where(c => c.PeopleID == userid && c.Completed == false).ToList();
            if (p != null)
            {
            }
            else
            {
                p = new List<ProjectTask>();
            }
            return p;
        }
        public ProjectTask GetProjectTaskById(int id)
        {
            ProjectTask p = this.ProjectTasks.Where(c => c.TaskId == id && c.Completed == false).SingleOrDefault();
            return p;
        }

        public AllInvoice AddInvoice(AllInvoice invoice)
        {
            List<AllInvoice> inv = invoices.Where(c => c.id.Equals(invoice.id)).ToList();
            Customer cust = Customers.Where(x => x.CustomerId == invoice.CustomerId && x.Deleted == false).Select(x => x).FirstOrDefault();
            AllInvoice obj = new AllInvoice();
            if (inv.Count == 0)
            {
                invoice.lastdate = indianTime;
                try
                {
                    invoice.Customer = cust.Name;
                    invoices.Add(invoice);
                    int id = this.SaveChanges();
                    return invoice;
                }
                catch
                {
                }
            }
            else
            {
                return null;
            }

            return obj;
        }
        public InvoiceRow AddInvoiceDetail(InvoiceRow e)
        {
            try
            {
                InvoiceRows.Add(e);
                int id = this.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return e;
        }
        public IEnumerable<Project> AllActiveProjectsbyCompanyId(int cmpid)
        {
            return Projects.Where(c => c.CompanyId == cmpid && c.Status.ToLower().Trim().Equals("active")).AsEnumerable();
        }
        public List<ClientProject> GetAllClientProject()
        {
            List<ClientProject> ClientProjects = new List<ClientProject>();
            try
            {
                foreach (var a in Projects)
                {
                    ClientProject cli = new ClientProject();
                    cli.Id = a.CustomerId;
                    cli.Name = a.ProjectName;
                    ClientProjects.Add(cli);
                }
                return ClientProjects;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }
        }

        public BaseCategory AddBaseCategory(BaseCategory basecat)
        {
            List<BaseCategory> bcat = BaseCategoryDb.Where(c => c.Deleted == false && c.BaseCategoryName.Trim().Equals(basecat.BaseCategoryName.Trim())).ToList();
            BaseCategory objcat = new BaseCategory();
            if (bcat.Count == 0)
            {
                basecat.CreatedDate = indianTime;
                basecat.UpdatedDate = indianTime;
                basecat.IsActive = true;
                basecat.Deleted = false;
                BaseCategoryDb.Add(basecat);
                this.SaveChanges();

                basecat.LogoUrl = "http://ec2-34-208-118-110.us-west-2.compute.amazonaws.com:8888/../../images/basecatimages/" + basecat.BaseCategoryId + ".jpg";
                BaseCategoryDb.Attach(basecat);
                Entry(basecat).State = EntityState.Modified;
                int id = this.SaveChanges();
                return basecat;
            }
            else
            {
                return objcat;
            }
        }
        public PurchaseOrderMasterRecived AddPurchaseOrderMasterRecived(PurchaseOrderMasterRecived pom)
        {
            pom.CreationDate = indianTime;
            PurchaseOrderMasterRecivedes.Add(pom);
            int id = this.SaveChanges();
            return pom;
        }

        public OrderDispatchedMaster AddOrderDispatchedMaster(OrderDispatchedMaster dm)
        {
            var OD = OrderDispatchedMasters.Where(x => x.OrderId == dm.OrderId).SingleOrDefault();
            var OMT = DbOrderMaster.Where(x => x.OrderId == dm.OrderId).SingleOrDefault();           
            if (OD == null)
            {
                dm.Status = "Ready to Dispatch";
                dm.CreatedDate = indianTime;
                dm.OrderedDate = OMT.CreatedDate;
                OrderDispatchedMasters.Add(dm);
                int id = this.SaveChanges();

                foreach (var a in OMT.orderDetails)
                {
                    a.Status = "Ready to Dispatch";
                    DbOrderDetails.Attach(a);
                    this.Entry(a).State = EntityState.Modified;
                    this.SaveChanges();
                }
                return dm;
            }
            else { return null; }
        }

        public OrderDispatchedDetails AddOrderDispatchedDetails(OrderDispatchedDetails dd)
        {
            if (dd.OrderDispatchedDetailsId > 0)
            {
                dd.CreatedDate = indianTime;
                OrderDispatchedDetailss.Attach(dd);
                this.Entry(dd).State = EntityState.Modified;
                int id = this.SaveChanges();
            }
            else {
                dd.CreatedDate = indianTime;
                OrderDispatchedDetailss.Add(dd);
                int id = this.SaveChanges();
            }          
            return dd;
        }
        
        public ReturnOrderDispatchedDetails AddReturnOrderDispatchedDetails (ReturnOrderDispatchedDetails dd)
        {
            dd.CreatedDate = indianTime;
            ReturnOrderDispatchedDetailsDb.Add(dd);
            int id = this.SaveChanges();
            return dd;
        }
        public IEnumerable<OrderDispatchedDetails> AllPOrderDispatchedDetails(int i)
        {
            return OrderDispatchedDetailss.Where(c => c.OrderId == i).AsEnumerable();

        }
        public IEnumerable<FinalOrderDispatchedDetails> AllFOrderDispatchedDetails(int i)
        {
            return FinalOrderDispatchedDetailsDb.Where(c => c.OrderId == i).AsEnumerable();
        }
        // Filter data for Report
        public List<OrderDispatchedDetailsFinalController.filtered> AllFOrderDispatchedReportDetails(DateTime datefrom, DateTime dateto)
        {
            List<FinalOrderDispatchedDetails> filterlist2 = new List<FinalOrderDispatchedDetails>();
            filterlist2 = FinalOrderDispatchedDetailsDb.Where(x => x.OrderDate > datefrom && x.OrderDate < dateto).ToList();
            var uniqdatelist = filterlist2.GroupBy(od => od.OrderDate).Select(g => g.First()).ToList();
            List<OrderDispatchedDetailsFinalController.filtered> newlist = new List<OrderDispatchedDetailsFinalController.filtered>();
            foreach (var date in uniqdatelist)
            {
                OrderDispatchedDetailsFinalController.filtered fdata = new OrderDispatchedDetailsFinalController.filtered();
                fdata.OrderDate = date.OrderDate;
                fdata.lst = new List<FinalOrderDispatchedDetails>();
                var sumtotal = 0.0;
                var sumtax = 0.0;
                var sumdisc = 0.0;
                var sumtaxafter = 0.0;
                foreach (var order in filterlist2)
                {
                    if (order.OrderDate.ToString("MM/dd/yyyy") == date.OrderDate.ToString("MM/dd/yyyy"))
                    {
                        sumtotal = sumtotal + order.TotalAmt;
                        sumtax = sumtax + order.TaxAmmount;
                        sumtaxafter = sumtaxafter + order.FinalTaxAmountAfter;
                        sumdisc = sumdisc + order.AmtWithoutTaxDisc;
                        order.FinalTaxAmount = sumtax;
                        order.SumtotalPrice = sumtotal;
                        fdata.lst.Add(order);
                    }
                }
                fdata.priceTotaltotal = sumtotal;
                fdata.TaxAftertotal = sumtaxafter;
                fdata.Taxtotal = sumtax;                
                newlist.Add(fdata);
            }
            return newlist;
        }
        // filter report        
        public List<OrderDispatchedMaster> AllFOrderDispatchedDeliveryDetails(DateTime datefrom, DateTime dateto)
        {
            List<OrderDispatchedMaster> filterlist2 = new List<OrderDispatchedMaster>();
            filterlist2 = OrderDispatchedMasters.Where(x => x.CreatedDate > datefrom &&  x.CreatedDate < dateto).ToList();
            var uniqdatelist = filterlist2.GroupBy(od => od.DboyMobileNo).Select(g => g.First()).ToList();
            return uniqdatelist;
        }
        public List<OrderDispatchedMaster> AllFOrderDispatchedDeliveryBoyDetails(DateTime datefrom, DateTime dateto, string DboyName)
        {
            List<OrderDispatchedMaster> filterlist2 = new List<OrderDispatchedMaster>();
            filterlist2 = OrderDispatchedMasters.Where(x =>  x.CreatedDate > datefrom && x.CreatedDate < dateto && x.DboyName== DboyName).ToList();            
            return filterlist2;
        }
        #region
        //Message-------------------------------------------------------------------------------------------
        public IEnumerable<Message> GetAllMessage()
        {
            if (DbMessage.AsEnumerable().Count() > 0)
            {
                List<Message> message = new List<Message>();
                message = DbMessage.ToList();
                return message.AsEnumerable();
            }
            else
            {
                List<Message> message = new List<Message>();
                return message.AsEnumerable();
            }
        }
        public Message GetMessagebyId(int id)
        {
            Message message = this.DbMessage.Where(c => c.MessageId == id).SingleOrDefault();
            if (message != null)
            {
                return null;
            }
            else
            {
                message = new Message();
            }
            return message;
        }
        public Message AddMessage(Message message)
        {
            List<Message> messageList = DbMessage.Where(c => c.MessageId.Equals(message.MessageId)).ToList();
            if (messageList.Count == 0)
            {
                message.CreatedDate = indianTime;
                message.UpdatedDate = indianTime;
                DbMessage.Add(message);
                int id = this.SaveChanges();
                return message;
            }
            else
            {
                Message Messageobj = new Message();
                return Messageobj;
            }
        }
        public Message PutMessage(Message obj)
        {
            logger.Info("put Message: ");
            try
            {
                Message message = DbMessage.Where(x => x.MessageId == obj.MessageId).FirstOrDefault();
                if (message != null)
                {
                    message.MessageType = obj.MessageType;
                    message.MessageTitle = obj.MessageTitle;
                    message.MessageText = obj.MessageText;

                    message.UpdatedDate = indianTime;
                    DbMessage.Attach(message);
                    this.Entry(message).State = EntityState.Modified;
                    this.SaveChanges();
                    return obj;
                }
                else
                {
                    logger.Error("This Message is not Found int put " + obj.MessageTitle);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Message " + ex.Message);
            }
            return null;
        }
        public bool DeleteMessage(int id)
        {
            try
            {
                Message message = DbMessage.Where(x => x.MessageId == id).SingleOrDefault();

                DbMessage.Attach(message);
                Entry(message).State = EntityState.Deleted;
                SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }
        #endregion

        public List<PurchaseOrderMaster> addPurchaseOrderMaster(List<TempPO> OBJ)
        {
            if (OBJ != null)
            {
                foreach (var x in OBJ)
                {
                    PurchaseOrderMaster pm = new PurchaseOrderMaster();
                    pm.SupplierId = x.SupplierId.GetValueOrDefault();
                    pm.SupplierName = x.SupplierName;
                    pm.CreationDate = indianTime;
                    pm.Status = "pending";
                    pm.Acitve = true;
                    DPurchaseOrderMaster.Add(pm);
                    int id = this.SaveChanges();
                    foreach (var a in x.Purchases)
                    {
                        var item = itemMasters.Where(z => z.PurchaseSku == a.PurchaseSku).FirstOrDefault();
                        PurchaseOrderDetail pd = new PurchaseOrderDetail();
                        pd.PurchaseOrderId = pm.PurchaseOrderId;
                        pd.ItemId = a.ItemId.GetValueOrDefault();
                        pd.ItemName = item.itemname;
                        pd.TotalQuantity = int.Parse(a.qty.ToString());
                        pd.CreationDate = indianTime;
                        pd.Status = "ordered";
                        pm.Warehouseid = a.WareHouseId;
                        pm.WarehouseName = a.WareHouseName;
                        pd.MOQ = item.PurchaseMinOrderQty;
                        pd.Price =Convert.ToDouble(a.Price);
                        pd.Warehouseid = a.WareHouseId;
                        pd.WarehouseName = a.WareHouseName;
                        pd.SupplierId = a.SupplierId.GetValueOrDefault();
                        pd.SupplierName = a.Supplier;
                        pd.TotalQuantity = Convert.ToInt32(a.qty);
                        pd.PurchaseName = a.name;
                        pd.PurchaseSku = a.PurchaseSku;
                        pd.ConversionFactor =Convert.ToInt16(a.conversionfactor);
                        pd.PurchaseQty = a.finalqty;

                        DPurchaseOrderDeatil.Add(pd);
                        int idd = this.SaveChanges();
                    }                   
                }
            }
            return null;
        }
        public CurrentStock addCurrentStock(CurrentStock stock)
        {
            DbCurrentStock.Add(stock);
            int id = this.SaveChanges();
            return null;
        }

        //for Manual update currentstock
        public CurrentStockHistory PutCurrentStock(CurrentStockHistory CurrentStockHistory)
        {

            CurrentStockHistory csht = CurrentStockHistoryDb.Where(x => x.StockId == CurrentStockHistory.StockId && x.Deleted == false).FirstOrDefault();
            CurrentStock cst = DbCurrentStock.Where(x => x.StockId == CurrentStockHistory.StockId && x.Deleted == false).FirstOrDefault();
            if (csht != null)
            {
                csht.updationDate = indianTime;
                csht.CurrentInventory = CurrentStockHistory.CurrentInventory;
                csht.TotalInventory = CurrentStockHistory.CurrentInventory;

                if (cst.CurrentInventory < csht.CurrentInventory)
                {
                    csht.ManualInventoryIn = csht.CurrentInventory - cst.CurrentInventory;
                }
            else {
                    csht.ManualInventoryIn = csht.CurrentInventory - cst.CurrentInventory;
                }
                csht.ManualReason = CurrentStockHistory.ManualReason;

                csht.CreationDate = indianTime;
                CurrentStockHistoryDb.Add(csht);
                int id = this.SaveChanges();

                //CurrentStockHistoryDb.Attach(csht);
                //this.Entry(csht).State = EntityState.Modified;
                //this.SaveChanges();

               // CurrentStock cst = DbCurrentStock.Where(x => x.StockId == CurrentStockHistory.StockId && x.Deleted == false).FirstOrDefault();
                if (cst != null) {
                    cst.UpdatedDate = indianTime;
                    cst.CurrentInventory = CurrentStockHistory.CurrentInventory;
                    DbCurrentStock.Attach(cst);
                    this.Entry(cst).State = EntityState.Modified;
                    this.SaveChanges();
                }
               
            }
            else
            {
                return null;
            }
            return CurrentStockHistory;
        }






        #region
        public List<Slider> GetAllSlider()
        {
            if (SliderDb.AsEnumerable().Count() > 0)
            {
                List<Slider> SliderList = new List<Slider>();
                SliderList = SliderDb.ToList();
                return SliderList;
            }
            else
            {
                List<Slider> SliderList = new List<Slider>();
                return SliderList;
            }
        }
        public Slider GetBySliderId(int id)
        {
            Slider Slider = this.SliderDb.Where(c => c.SliderId == id).SingleOrDefault();
            if (Slider != null)
            {
                return null;
            }
            else
            {
                Slider = new Slider();
            }
            return Slider;
        }
        public Slider AddSlider(Slider slider)
        {
            List<Slider> sliderList = SliderDb.Where(c => c.SliderId.Equals(slider.SliderId)).ToList();
            if (sliderList.Count == 0)
            {
                slider.CreatedDate = indianTime;
                slider.UpdatedDate = indianTime;
                SliderDb.Add(slider);
                int id = this.SaveChanges();
                return slider;
            }
            else
            {
                Slider objslider = new Slider();
                return objslider;
            }
        }
        public Slider PutSlider(Slider obj)
        {
            logger.Info("put Slider: ");
            try
            {
                Slider slider = SliderDb.Where(x => x.SliderId == obj.SliderId).FirstOrDefault();
                if (slider != null)
                {
                    slider.Type = obj.Type;
                    slider.Pic = obj.Pic;
                    slider.UpdatedDate = indianTime;
                    SliderDb.Attach(slider);
                    this.Entry(slider).State = EntityState.Modified;
                    this.SaveChanges();
                    return obj;
                }
                else
                {
                    logger.Error("This Slider is not Found int put " + obj.SliderId);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Slider " + ex.Message);
            }
            return null;
        }
        public bool DeleteSlider(int id)
        {
            logger.Info("delete Slider auth");
            try
            {
                Slider Slider = SliderDb.Where(x => x.SliderId == id).SingleOrDefault();
                SliderDb.Attach(Slider);
                Entry(Slider).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("error in deleting slider " + ex);
                return false;
            }
        }       
        #endregion
        public IEnumerable<ReturnOrderDispatchedDetails> AllReturnOrderDispatchedDetails(int i)
        {
            return ReturnOrderDispatchedDetailsDb.Where(c => c.OrderId == i).AsEnumerable();
        }

        public OrderDispatchedMaster UpdateOrderDispatchedMaster(OrderDispatchedMaster om)
        {
            logger.Info("put OrderDispatch Master: ");
            try
            {               
                if (om != null)
                {
                    int count = 1;
                    om.ReDispatchCount = om.ReDispatchCount + count;

                    om.UpdatedDate = indianTime;
                    OrderDispatchedMasters.Attach(om);
                    this.Entry(om).State = EntityState.Modified;
                    this.SaveChanges();
                    return om;
                }
                else
                {
                    logger.Error("This Slider is not Found int put " + om.OrderDispatchedMasterId);
                    return om;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put Order Dispatch master " + ex.Message);
            }
            return null;
        }
        public FinalOrderDispatchedMaster AddFinalOrderDispatchedMaster(FinalOrderDispatchedMaster final)
        {
            logger.Info("add Final Order Dispatch Master: ");
            try
            {
                // for ordermaster master status change
                OrderMaster om = DbOrderMaster.Where(x => x.OrderId == final.OrderId && x.Deleted == false).FirstOrDefault();
                om.Status = "sattled";
                om.DiscountAmount = final.DiscountAmount;
                om.ShortAmount = final.ShortAmount;
                om.ShortAmount = final.ShortAmount;
                om.UpdatedDate = indianTime;
                DbOrderMaster.Attach(om);
                Entry(om).State = EntityState.Modified;
                SaveChanges();

                // order dispatched master status change
                OrderDispatchedMaster ox = OrderDispatchedMasters.Where(x => x.OrderId == final.OrderId && x.Deleted == false).FirstOrDefault();
                ox.Status = "sattled";
                ox.DiscountAmount = final.DiscountAmount;
                ox.UpdatedDate = indianTime;
                OrderDispatchedMasters.Attach(ox);
                Entry(ox).State = EntityState.Modified;
                SaveChanges();
                try
                {
                    if (final.ShortAmount > 0)
                    {
                        ShortSetttle obj = new ShortSetttle();
                        obj.Status = "sattled";
                        obj.OrderId = final.OrderId;
                        obj.CustomerId = final.CustomerId;
                        obj.CustomerName = final.CustomerName;
                        obj.DboyName = final.DboyName;
                        obj.DboyMobileNo = final.DboyMobileNo;
                        obj.Warehouseid = final.Warehouseid;
                        obj.WarehouseName = final.WarehouseName;
                        obj.ShortAmount = final.ShortAmount;
                        obj.ShortReason = final.ShortReason;
                        obj.DiscountAmount = final.DiscountAmount;
                        obj.GrossAmount = final.GrossAmount;
                        obj.CreatedDate = indianTime;
                        ShortSetttleDb.Add(obj);
                        int id = this.SaveChanges();
                    }
                }
                catch
                {
                }
                if (final != null)
                {
                    final.UpdatedDate = indianTime;
                    final.Status = "sattled";
                    FinalOrderDispatchedMasterDb.Attach(final);
                    this.Entry(final).State = EntityState.Added;
                    this.SaveChanges();
                    return final;
                }
                else
                {
                    logger.Error("This final is not Found int add " + final.FinalOrderDispatchedMasterId);
                    return final;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in final Order Dispatch master " + ex.Message);
            }
            return null;
        }     
        public IEnumerable<BillPramotion> AllBillPramtion()
        {
            return BillPramotions.Where(c => c.Deleted == false).AsEnumerable();
        }
        public BillPramotion AddBillPramtion(BillPramotion pramtion)
        {
            List<BillPramotion> bcat = BillPramotions.Where(c => c.Deleted == false && c.title.Trim().Equals(pramtion.title.Trim())).ToList();
            BaseCategory objcat = new BaseCategory();
            if (bcat.Count == 0)
            {
              
                pramtion.CreatedDate = indianTime;
                pramtion.UpdatedDate = indianTime;
                BillPramotions.Add(pramtion);
                int id = this.SaveChanges();
                return pramtion;
            }
            else
            {
                return bcat[0];
            }
        }
        public BillPramotion PutBillPramtion(BillPramotion pramtion)
        {
            BillPramotion bcat = BillPramotions.Where(c => c.PramotionId == pramtion.PramotionId).SingleOrDefault();
            if (bcat != null)
            {
                bcat.title = pramtion.title;
                bcat.Description = pramtion.Description;
                bcat.StartDate = pramtion.StartDate;
                bcat.EndDate = pramtion.EndDate;
                bcat.UpdatedDate = indianTime;
                return bcat;
            }
            else
            {
                BillPramotion pr = new BillPramotion();
                return pr;
            }
        }
        public bool DeleteBillPramtion(int id)
        {
            try
            {
                BillPramotion bcat = BillPramotions.Where(c => c.PramotionId == id).SingleOrDefault();
                if (bcat != null)
                {
                    BillPramotions.Attach(bcat);
                    this.Entry(bcat).State = EntityState.Modified;
                    this.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public List<SubCategory> PramotionalBrand(int warehouseid)
        {
            List<SubCategory> subcat = new List<SubCategory>();
            subcat = SubCategorys.Where(w => w.Deleted == false && w.IsPramotional == true).ToList();
            return subcat;
        }

        #region
        public List<DeviceNotification> GetAllNotificationByCustomerId()
        {
            if (NotificationByDeviceIdDb.AsEnumerable().Count() > 0)
            {
                List<DeviceNotification> td = new List<DeviceNotification>();
                td = DeviceNotificationDb.ToList();
                return td;
            }
            else
            {
                List<DeviceNotification> td = new List<DeviceNotification>();
                return td;
            }
        }
        #endregion

        #region
        public List<DeviceNotification> GetAllNotification()
        {
            if (NotificationDb.AsEnumerable().Count() > 0)
            {
                List<DeviceNotification> td = new List<DeviceNotification>();
                td = DeviceNotificationDb.ToList();
                return td;
            }
            else
            {
                List<DeviceNotification> td = new List<DeviceNotification>();
                return td;
            }
        }

        public Notification AddNotification(Notification notify)
        {
            var dddd = NotificationDb.Where(c => c.Id.Equals(notify.Id)).ToList();
            List<Notification> notifyList = NotificationDb.Where(c => c.Id.Equals(notify.Id)).ToList();

            if (notifyList.Count == 0)
            {
                // To get distinct list of notification save send-time of notification 
                notify.NotificationTime = indianTime;
                NotificationDb.Add(notify);
                int id = this.SaveChanges();
                return notify;

            }
            else
            {
                Notification objdt = new Notification();
                return objdt;
            }
        }

        #endregion
        
        #region
        public List<DeviceNotification> GetAllDeviceNotification()
        {
            if (DeviceNotificationDb.AsEnumerable().Count() > 0)
            {
                List<DeviceNotification> DeviceNotificationList = new List<DeviceNotification>();
                DeviceNotificationList = DeviceNotificationDb.ToList();
                return DeviceNotificationList;
            }
            else
            {
                List<DeviceNotification> DeviceNotificationList = new List<DeviceNotification>();
                return DeviceNotificationList;
            }
        }


        public bool GetAllDeviceNotification(string RegId, string imei)
        {
            DeviceNotification data1 = DeviceNotificationDb.Where(r => r.DeviceId == RegId).FirstOrDefault();

            var data = DeviceNotificationDb.Where(r => r.DeviceId == RegId).FirstOrDefault();
            if (data == null)
            {
                DeviceNotification devicenotification = new DeviceNotification();
                devicenotification.DeviceId = RegId;
                //devicenotification.CreatedDate = indianTime;
                //devicenotification.UpdatedDate = indianTime;
                DeviceNotificationDb.Add(devicenotification);
                int id = this.SaveChanges();

                return true;
            }

            else
            {
                //data.UpdatedDate = indianTime;
                DeviceNotificationDb.Attach(data);
                this.Entry(data).State = EntityState.Modified;
                this.SaveChanges();

                return true;
            }
        }

        public DeviceNotification GetByDeviceNotificationId(int id)
        {
            DeviceNotification deviceNotifications = this.DeviceNotificationDb.Where(c => c.Id == id).SingleOrDefault();
            if (deviceNotifications != null)
            {
                return null;
            }
            else
            {
                deviceNotifications = new DeviceNotification();
            }
            return deviceNotifications;
        }
        public DeviceNotification AddDeviceNotification(DeviceNotification devicenotification)
        {
            List<DeviceNotification> devicenotificationList = DeviceNotificationDb.Where(c => c.Id.Equals(devicenotification.Id)).ToList();
            if (devicenotificationList.Count == 0)
            {
                //devicenotification.CreatedDate = indianTime;
                //devicenotification.UpdatedDate = indianTime;
                DeviceNotificationDb.Add(devicenotification);
                int id = this.SaveChanges();
                return devicenotification;
            }
            else
            {
                DeviceNotification objDeviceNotification = new DeviceNotification();
                return objDeviceNotification;
            }
        }
        public DeviceNotification PutDeviceNotification(DeviceNotification obj)
        {
            logger.Info("put DeviceNotification: ");
            try
            {
                DeviceNotification deviceNotification = DeviceNotificationDb.Where(x => x.Id == obj.Id).FirstOrDefault();
                if (deviceNotification != null)
                {
                    deviceNotification.DeviceId = obj.DeviceId;
                    //deviceNotification.CreatedDate = indianTime;
                    //deviceNotification.UpdatedDate = indianTime;
                    DeviceNotificationDb.Attach(deviceNotification);
                    this.Entry(deviceNotification).State = EntityState.Modified;
                    this.SaveChanges();
                    return obj;
                }
                else
                {
                    logger.Error("This DeviceId is not Found int put " + obj.DeviceId);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in put DeviceNotification " + ex.Message);
            }
            return null;
        }
        public bool DeleteDeviceNotification(int id)
        {
            try
            {
                DeviceNotification deviceNotifications = DeviceNotificationDb.Where(x => x.Id == id).SingleOrDefault();
                DeviceNotificationDb.Attach(deviceNotifications);
                Entry(deviceNotifications).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region
        public List<GroupNotification> GetAllGroupNotification()
        {
            if (GroupNotificationDb.AsEnumerable().Count() > 0)
            {
                List<GroupNotification> StoreList = new List<GroupNotification>();
                StoreList = GroupNotificationDb.ToList();
                return StoreList;
            }
            else
            {
                List<GroupNotification> StoreList = new List<GroupNotification>();
                return StoreList;
            }
        }

        public GroupNotification AddGroupNotification(GroupNotification store)
        {
            List<GroupNotification> storeList = GroupNotificationDb.Where(c => c.Id.Equals(store.Id)).ToList();
            if (storeList.Count == 0)
            {
                // store.GroupCreationTime = indianTime;

                GroupNotificationDb.Add(store);
                int id = this.SaveChanges();
                return store;

            }
            else
            {
                GroupNotification objdt = new GroupNotification();
                return objdt;
            }
        }
        //public GroupNotification PutGroupNotification(GroupNotification obj)
        //{
        //    logger.Info("put Store: ");
        //    try
        //    {
        //        GroupNotification store = SourceDb.Where(x => x.SourceId == obj.SourceId).FirstOrDefault();
        //        if (store != null)
        //        {
        //            store.SourceName = obj.SourceName;
        //            store.Address = obj.Address;
        //            store.ContactNumber = obj.ContactNumber;
        //            store.Email = obj.Email;


        //            SourceDb.Attach(store);
        //            this.Entry(store).State = EntityState.Modified;
        //            this.SaveChanges();
        //            return obj;
        //        }
        //        else
        //        {
        //            logger.Error("This Slider is not Found int put " + obj.SourceId);
        //            return obj;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in put Slider " + ex.Message);
        //    }
        //    return null;
        //}
        public bool DeleteGroupNotification(int id)
        {
            logger.Info("delete Slider auth");
            try
            {
                GroupNotification store = GroupNotificationDb.Where(x => x.Id == id).SingleOrDefault();
                GroupNotificationDb.Attach(store);
                Entry(store).State = EntityState.Deleted;
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Info("error in deleting Store " + ex);
                return false;
            }
        }
        #endregion
        #region get order data order settle page
        public PaggingData AllDispatchedOrderMasterPaging(int list, int page, string DBoyNo, DateTime? datefrom, DateTime? dateto, int? OrderId)
        {
            List<OrderDispatchedMaster> newdata = new List<OrderDispatchedMaster>();

            if (OrderDispatchedMasters.AsEnumerable().Count() > 0)
            {
                if (DBoyNo == "all" && datefrom == null && OrderId == null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
                else if (DBoyNo == "all" && datefrom == null && OrderId != null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.Deleted == false && x.Status == "Delivered" && x.OrderId == OrderId).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }

                else if (DBoyNo == "all" && datefrom != null && OrderId != null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Delivered" && x.OrderId == OrderId) && (x.OrderedDate > datefrom && x.OrderedDate < dateto)).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
                else if (DBoyNo != "all" && datefrom == null && OrderId == null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.DboyMobileNo == DBoyNo && x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
                else if (DBoyNo != "all" && datefrom != null && OrderId != null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.OrderedDate > datefrom && x.OrderedDate < dateto && x.DboyMobileNo == DBoyNo && x.Deleted == false && x.OrderId == OrderId && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
                else if (DBoyNo == "all" && datefrom != null && OrderId == null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.OrderedDate > datefrom && x.OrderedDate < dateto && x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }


                else if (DBoyNo == "all" && datefrom != null && dateto != null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Delivered") && (x.OrderedDate > datefrom && x.OrderedDate < dateto)).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
                else if (DBoyNo != "all" && datefrom != null && OrderId == null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.OrderedDate > datefrom && x.OrderedDate < dateto && x.DboyMobileNo == DBoyNo && x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
                else if (DBoyNo != "all" && datefrom == null && dateto == null)
                {
                    newdata = OrderDispatchedMasters.Where(x => x.DboyMobileNo == DBoyNo && x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
                }
            }
            else
            {
                var orders = OrderDispatchedMasters.OrderByDescending(x => x.OrderId).AsEnumerable();
            }
            PaggingData obj = new PaggingData();
            obj.total_count = DbOrderMaster.Count();
            obj.ordermaster = newdata;
            return obj;
        }
        #endregion
        //public PaggingData AllDispatchedOrderMasterPaging(int list, int page,string DBoyNo, DateTime? datefrom, DateTime? dateto)
        //{
        //    List<OrderDispatchedMaster> newdata = new List<OrderDispatchedMaster>();

        //    if (OrderDispatchedMasters.AsEnumerable().Count() > 0)
        //    {
        //        if (DBoyNo == "all" && datefrom == null )
        //        {
        //            newdata = OrderDispatchedMasters.Where(x => x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
        //        }
        //        else if (DBoyNo == "all" && datefrom != null && dateto != null)
        //        {
        //            newdata = OrderDispatchedMasters.Where(x => x.Deleted == false && (x.Status == "Delivered") && (x.CreatedDate > datefrom && x.CreatedDate < dateto)).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
        //        }
        //        else if (DBoyNo != "all" && datefrom != null && dateto != null)
        //        {
        //            newdata = OrderDispatchedMasters.Where(x =>x.CreatedDate> datefrom && x.CreatedDate< dateto && x.DboyMobileNo == DBoyNo && x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
        //        }
        //        else if (DBoyNo != "all" && datefrom == null && dateto == null)
        //        {
        //            newdata = OrderDispatchedMasters.Where(x =>x.DboyMobileNo == DBoyNo && x.Deleted == false && x.Status == "Delivered").OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
        //        }
        //    }
        //    else
        //    {
        //        var orders = OrderDispatchedMasters.OrderByDescending(x => x.OrderId).AsEnumerable();
        //    }
        //    PaggingData obj = new PaggingData();
        //    obj.total_count = DbOrderMaster.Count();
        //    obj.ordermaster = newdata;
        //    return obj;
        //}

        public PaggingData AllItemMasterForPaging(int list, int page , string warehouse)
        {
            PaggingData obj = new PaggingData();
            obj.total_count = itemMasters.Where(x => x.WarehouseName == warehouse).Count();
            obj.ordermaster = itemMasters.AsEnumerable().Where(x=>x.WarehouseName == warehouse && x.Deleted==false).Skip((page - 1) * list).Take(list).ToList(); 
            return obj;
        }


        public DamageOrderMaster GetDOrderMaster(int orderid)
        {
            var orders = DamageOrderMasterDB.Where(x => x.DamageOrderId == orderid).SingleOrDefault();
            foreach (var d in orders.DamageorderDetails)
            {
                var Damageinventory = DamageStockDB.Where(k => k.Deleted != true && k.ItemNumber == d.itemNumber && k.Warehouseid == d.Warehouseid).FirstOrDefault();
                ItemMaster master = itemMasters.Where(c => c.ItemId == d.ItemId).SingleOrDefault();

                if (Damageinventory != null)
                {
                    if (Damageinventory.ItemNumber == d.itemNumber)
                    {
                       // d.DamageStock = Damageinventory.CurrentInventory;
                    }
                }
                else
                {
                  //  d.CurrentStock = 0;
                }
            }
            return orders;
        }



        public OrderMaster GetOrderMaster(int orderid)
        {
            var orders = DbOrderMaster.Where(x => x.OrderId == orderid).SingleOrDefault();           
            foreach (var d in orders.orderDetails)
            {
                var currentinventory = DbCurrentStock.Where(k => k.Deleted != true && k.ItemNumber == d.itemNumber && k.Warehouseid == d.Warehouseid).FirstOrDefault();
                ItemMaster master = itemMasters.Where(c => c.ItemId == d.ItemId).SingleOrDefault();

                if (currentinventory != null)
                {
                    if (currentinventory.ItemNumber == d.itemNumber)
                    {
                        d.CurrentStock = currentinventory.CurrentInventory;
                    }
                }
                else
                {
                        d.CurrentStock = 0;                    
                }                
            }
            return orders;
        }



        public PaggingData_st AllItemHistory(int list, int page, int StockId)
        {
            List<CurrentStockHistory> newdata = new List<CurrentStockHistory>();
            var listOrders = CurrentStockHistoryDb.Where(x => x.Deleted == false && x.StockId == StockId).OrderByDescending(x => x.CreationDate).Skip((page - 1) * list).Take(list).ToList();
            newdata = listOrders;
            PaggingData_st obj = new PaggingData_st();
            obj.total_count = CurrentStockHistoryDb.Where(x => x.Deleted == false && x.StockId == StockId).Count();
            obj.ordermaster = newdata;
            return obj;
        }



        public PaggingData AllOrderMaster(int list, int page)
        {
            List<OrderMaster> newdata = new List<OrderMaster>();

            var listOrders = DbOrderMaster.Where(x => x.Deleted == false).OrderByDescending(x => x.OrderId).Skip((page - 1) * list).Take(list).ToList();
            newdata = listOrders;
            PaggingData obj = new PaggingData();
            obj.total_count = DbOrderMaster.Count();
            obj.ordermaster = newdata;
            return obj;
            

        }



        public PaggingData AllDOrderMaster(int list, int page)
        {
            List<DamageOrderMaster> newdata = new List<DamageOrderMaster>();

            var listOrders = DamageOrderMasterDB.Where(x => x.Deleted == false).OrderByDescending(x => x.DamageOrderId).Skip((page - 1) * list).Take(list).ToList();
            newdata = listOrders;
            PaggingData obj = new PaggingData();
            obj.total_count = DbOrderMaster.Count();
            obj.ordermaster = newdata;
            return obj;
        }

        public List<OrderMaster> searchorderbycustomer(DateTime? start, DateTime? end, int OrderId, string Skcode, string ShopName, string Mobile, string status)
        {
            List<OrderMaster> newdata = new List<OrderMaster>();

            if ((Mobile != null) && start != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                             && (a.Customerphonenum.Contains(Mobile))).OrderByDescending(x => x.OrderId).ToList();                
                return listOrders;
            }
            else if ((Skcode != null) && start != null)
            {

                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                             && a.Skcode.Contains(Skcode)).OrderByDescending(x => x.OrderId).ToList();
                return listOrders;
            }
            else if ((ShopName != null) && start != null)
            {
                var listOrders = DbOrderMaster.Where(a=> a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                             && a.ShopName.Contains(ShopName)).OrderByDescending(x => x.OrderId).ToList();

                return listOrders;
            }
            else if ((OrderId != 0) && start != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                          && a.OrderId.Equals(OrderId)).OrderByDescending(x => x.OrderId).ToList();
                
                return listOrders;
            }
            else if ((status != null) && start != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end
                             && a.Status.Equals(status)).OrderByDescending(x => x.OrderId).ToList();

                return listOrders;
            }
            else if (Mobile != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.Customerphonenum.Contains(Mobile)).OrderByDescending(x => x.OrderId).ToList();
                
                return listOrders;
            }
            else if (Skcode != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.Skcode.Contains(Skcode)).OrderByDescending(x => x.OrderId).ToList();

                return listOrders;
            }
            else if (ShopName != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.ShopName.Contains(ShopName)).OrderByDescending(x => x.OrderId).ToList();
                
                return listOrders;
            }
            else if (OrderId != 0)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.OrderId.Equals(OrderId)).OrderByDescending(x => x.OrderId).ToList();
                
                return listOrders;
            }
            else if (status != null)
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.Status.Equals(status)).OrderByDescending(x => x.OrderId).ToList();
                            
                return listOrders;
            }
            else
            {
                var listOrders = DbOrderMaster.Where(a => a.Deleted == false && a.CreatedDate >= start && a.CreatedDate <= end).OrderByDescending(x => x.OrderId).ToList();
                
                return listOrders;
            }
        }
        public Customer GetCustomerbyId(string Mobile)
        {
            Customer customer = Customers.Where(c => c.Mobile == Mobile).SingleOrDefault();
            if (customer != null)
            {
                return customer;
            }
            else
            {
                customer = new Customer();
            }
            return customer;
        }
        //Anil 16-01 edit
        public PurchaseOrderDetailRecived AddPurchaseOrderDetailsRecived(PurchaseOrderDetailRecived pd, int count)
        {
            PurchaseOrderDetailRecived podr = PurchaseOrderRecivedDetails.Where(x => x.PurchaseOrderDetailId == pd.PurchaseOrderDetailId).SingleOrDefault();
            if (podr == null)
            {
                ItemMaster itm = itemMasters.Where(y => y.PurchaseSku.Trim() == pd.PurchaseSku.Trim()).FirstOrDefault();
                var item = DbCurrentStock.Where(x => x.ItemNumber == itm.Number && x.Warehouseid == pd.Warehouseid).SingleOrDefault();
                if (item != null)
                {
                    var QtyReciv = pd.QtyRecived1.GetValueOrDefault();                    
                    if (QtyReciv != 0)
                    {
                       
                        CurrentStockHistory Oss = new CurrentStockHistory();
                        if (item!= null)
                        {
                            Oss.StockId = item.StockId;
                            Oss.ItemNumber = item.ItemNumber;
                            Oss.ItemName = item.ItemName;
                            Oss.OdOrPoId = pd.PurchaseOrderId;
                            Oss.CurrentInventory = item.CurrentInventory;
                            Oss.InventoryIn = Convert.ToInt32(QtyReciv);
                            Oss.TotalInventory = item.CurrentInventory + Convert.ToInt32(QtyReciv);
                            Oss.WarehouseName = item.WarehouseName;
                            Oss.CreationDate = indianTime;
                            CurrentStockHistoryDb.Add(Oss);
                            int id = this.SaveChanges();
                        }
                        //else {
                        //    Oss.StockId = item.StockId;
                        //    Oss.ItemNumber = item.ItemNumber;
                        //    Oss.ItemName = item.ItemName;
                        //    Oss.CurrentInventory = item.CurrentInventory;
                        //    Oss.InventoryIn = Convert.ToInt32(QtyReciv);
                        //    Oss.TotalInventory = item.CurrentInventory + Convert.ToInt32(QtyReciv);
                        //    Oss.WarehouseName = item.WarehouseName;
                        //    Oss.CreationDate = DateTime.Now;
                        //    CurrentStockHistoryDb.Attach(Oss);
                        //    this.Entry(Oss).State = EntityState.Modified;
                        //    this.SaveChanges();
                        //}

                        item.CurrentInventory = item.CurrentInventory + Convert.ToInt32(QtyReciv);
                        this.UpdateCurrentStock(item);                        
                    }
                    else {
                        pd.Price1 = 0;
                    }
                    pd.CreationDate = indianTime;
                    pd.QtyRecived2 = 0;
                    pd.QtyRecived3 = 0;
                    pd.QtyRecived4 = 0;
                    pd.QtyRecived5 = 0;

                    var amt = pd.QtyRecived1 * pd.Price1;
                    if (pd.dis1 != 0 && pd.dis1 != null)
                    {
                        pd.PriceRecived = ((amt * 100) / (100 + pd.dis1)).GetValueOrDefault();
                    }
                    else
                    {
                        pd.PriceRecived = amt.GetValueOrDefault();
                    }

                    if (pd.TotalQuantity > (pd.QtyRecived1))
                    {
                        pd.TotalAmountIncTax = amt.GetValueOrDefault();
                        pd.Status = "Partial Received";
                        PurchaseOrderRecivedDetails.Add(pd);
                        int id = this.SaveChanges();
                    }
                    else
                    {
                        if (pd.Status != "Received")
                        {
                            pd.TotalAmountIncTax = amt.GetValueOrDefault();
                            pd.Status = "Received";
                            PurchaseOrderRecivedDetails.Add(pd);
                            int id = this.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                ItemMaster itm = itemMasters.Where(y => y.PurchaseSku.Trim() == pd.PurchaseSku.Trim()).FirstOrDefault();
                var item = DbCurrentStock.Where(x => x.ItemNumber == itm.Number && x.Warehouseid == pd.Warehouseid).SingleOrDefault();
                if (item != null)
                {
                    var QtyReciv = 0;
                    if (count == 2 && podr.QtyRecived2 == 0)
                    {
                        QtyReciv = pd.QtyRecived2.GetValueOrDefault();
                        var amt = QtyReciv * pd.Price2;
                        if (pd.dis2 != 0 && pd.dis2 != null)
                        {
                            podr.dis2 = pd.dis2;
                            podr.PriceRecived += ((amt * 100) / (100 + pd.dis2)).GetValueOrDefault();
                        }
                        else
                        {
                            podr.PriceRecived += amt.GetValueOrDefault(); 
                        }
                    }
                    else if (count == 3 && podr.QtyRecived3 == 0)
                    {
                        QtyReciv = pd.QtyRecived3.GetValueOrDefault();
                        var amt = QtyReciv * pd.Price3;
                        if (pd.dis3 != 0 && pd.dis3 != null)
                        {
                            podr.dis3 = pd.dis3;
                            podr.PriceRecived += ((amt * 100) / (100 + pd.dis3)).GetValueOrDefault();
                        }
                        else
                        {
                            podr.PriceRecived += amt.GetValueOrDefault();
                        }
                    }
                    else if (count == 4 && podr.QtyRecived4 == 0)
                    {
                        QtyReciv = pd.QtyRecived4.GetValueOrDefault();
                        var amt = QtyReciv * pd.Price4;
                        if (pd.dis4 != 0 && pd.dis4 != null)
                        {
                             podr.PriceRecived += ((amt * 100) / (100 + pd.dis4)).GetValueOrDefault();
                        }
                        else
                        {
                            podr.PriceRecived += amt.GetValueOrDefault();
                        }
                    }
                    else if (count == 5 && podr.QtyRecived5 == 0)
                    {
                        QtyReciv = pd.QtyRecived5.GetValueOrDefault();
                        var amt = QtyReciv * pd.Price5;
                        if (pd.dis5 != 0 && pd.dis5 != null)
                        {
                            podr.dis5 = pd.dis5;
                            podr.PriceRecived += ((amt * 100) / (100 + pd.dis5)).GetValueOrDefault();
                        }
                        else
                        {
                            podr.PriceRecived += amt.GetValueOrDefault();
                        }
                    }
                    else if (count == 1 && podr.QtyRecived1 == 0)
                    {
                        QtyReciv = pd.QtyRecived1.GetValueOrDefault();
                        var amt = QtyReciv * pd.Price1;
                        if (pd.dis1 != 0 && pd.dis1 != null)
                        {
                            podr.dis1 = pd.dis1;
                            podr.PriceRecived = ((amt * 100) / (100 + pd.dis1)).GetValueOrDefault();
                        }
                        else
                        {
                            podr.PriceRecived = amt.GetValueOrDefault();
                        }
                    }
                    if (QtyReciv != 0)
                    {


                        CurrentStockHistory Oss = new CurrentStockHistory();
                        if (item != null)
                        {
                            Oss.StockId = item.StockId;
                            Oss.ItemNumber = item.ItemNumber;
                            Oss.ItemName = item.ItemName;
                            Oss.CurrentInventory = item.CurrentInventory;
                            Oss.OdOrPoId = pd.PurchaseOrderId;
                            Oss.InventoryIn = Convert.ToInt32(QtyReciv);
                            Oss.TotalInventory = item.CurrentInventory + Convert.ToInt32(QtyReciv);
                            Oss.WarehouseName = item.WarehouseName;
                            Oss.CreationDate = indianTime;
                            CurrentStockHistoryDb.Add(Oss);
                            int id = this.SaveChanges();
                        }
                        item.CurrentInventory = item.CurrentInventory + Convert.ToInt32(QtyReciv);
                        this.UpdateCurrentStock(item);

                        var irconfirm = IR_ConfirmDb.Where(x => x.PurchaseOrderId == podr.PurchaseOrderId && x.ItemId == podr.ItemId).SingleOrDefault();
                        if (irconfirm != null) {
                            irconfirm.QtyRecived += QtyReciv;
                            IR_ConfirmDb.Attach(irconfirm);
                            this.Entry(irconfirm).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }

                    podr.TotalAmountIncTax = pd.TotalAmountIncTax;
                    podr.TaxAmount = pd.TaxAmount;
                    podr.QtyRecived1 = pd.QtyRecived1;
                    podr.QtyRecived2 = pd.QtyRecived2;
                    podr.QtyRecived3 = pd.QtyRecived3;
                    podr.QtyRecived4 = pd.QtyRecived4;
                    podr.QtyRecived5 = pd.QtyRecived5;
                    double? PriceRecived = 0.00;
                    if (count == 1 && pd.Price1 != null)
                    {
                        if(podr.QtyRecived1 > 0)
                            podr.Price1 = pd.Price1;
                        PriceRecived += pd.QtyRecived1 * pd.Price1;
                    }
                    if (count == 2 && pd.Price2 != null)
                    {
                        if (podr.QtyRecived2 > 0)
                            podr.Price2 = pd.Price2;
                        PriceRecived += pd.QtyRecived2 * pd.Price2;
                    }
                    if (count == 3 && pd.Price3 != null)
                    {
                        if (podr.QtyRecived3 > 0)
                            podr.Price3 = pd.Price3;
                        PriceRecived += pd.QtyRecived3 * pd.Price3;
                    }
                    if (count == 4 && pd.Price4 != null)
                    {
                        if (podr.QtyRecived4 > 0)
                            podr.Price4 = pd.Price4;
                        PriceRecived += pd.QtyRecived4 * pd.Price4;
                    }
                    if (count == 5 && pd.Price5 != null)
                    {
                        if (podr.QtyRecived5 > 0)
                            podr.Price5 = pd.Price5;
                        PriceRecived += pd.QtyRecived5 * pd.Price5;
                    }
                    if (pd.TotalQuantity > (pd.QtyRecived1 + pd.QtyRecived2 + pd.QtyRecived3 + pd.QtyRecived4 + pd.QtyRecived5))
                    {
                        podr.TotalAmountIncTax += PriceRecived.GetValueOrDefault();
                        podr.Status = "Partial Received";
                        PurchaseOrderRecivedDetails.Add(podr);
                        this.Entry(podr).State = EntityState.Modified;
                        this.SaveChanges();
                    }
                    else
                    {
                        if (pd.Status != "Received")
                        {
                            podr.TotalAmountIncTax += PriceRecived.GetValueOrDefault();
                            podr.Status = "Received";
                            PurchaseOrderRecivedDetails.Add(podr);
                            this.Entry(podr).State = EntityState.Modified;
                            this.SaveChanges();
                        }
                    }
                }
            }
            return pd;
        }
        public string skcode()
        {
            var customer = Customers.OrderByDescending(e => e.Skcode).FirstOrDefault();
            var skcode = "";
            if (customer != null)
            { int i = 1;
                bool flag = false;
                while (flag == false) { 
                    var skList = customer.Skcode.Split('K');
                    var skint = Convert.ToInt32(skList[1]) + i;
                    skcode = (skList[0] + "K" + Convert.ToString(skint)).Trim();
                    List<Customer> check = Customers.Where(s => s.Skcode.Trim().ToLower() == skcode.Trim().ToLower()).ToList();
                    if (check.Count == 0)
                    {
                        flag = true;
                        return skcode;
                    }
                    else {
                        i = i + 1;
                    }
                } 
            }
                      
            return skcode;
        }

        public DeliveryCharge AddUpdateDeliveryCharge(DeliveryCharge del)
        {
            DeliveryCharge delivery = new DeliveryCharge();
            delivery = DeliveryChargeDb.Where(x=> x.warhouse_Id == del.warhouse_Id).FirstOrDefault();
            Warehouse w = Warehouses.Where(x => x.Warehouseid == del.warhouse_Id).FirstOrDefault();
            Cluster c = Clusters.Where(x => x.ClusterId == del.cluster_Id).FirstOrDefault();
            if (delivery == null)
            {
                if (w != null)
                {
                    del.warhouse_Id = w.Warehouseid;
                    del.warhouse_Name = w.WarehouseName;
                }
                if (c != null)
                {
                    del.cluster_Id = c.ClusterId;
                    del.cluster_Name = c.ClusterName;
                }
                DeliveryChargeDb.Add(del);
                int id = this.SaveChanges();
                return del;
            }
            else
            {
                if (w != null)
                {
                    delivery.warhouse_Id = w.Warehouseid;
                    delivery.warhouse_Name = w.WarehouseName;
                }
                if (c != null)
                {
                    delivery.cluster_Id = c.ClusterId;
                    delivery.cluster_Name = c.ClusterName;
                }
                delivery.min_Amount = del.min_Amount;
                delivery.max_Amount = del.max_Amount;
                delivery.del_Charge = del.del_Charge;
                delivery.min_Amount = del.min_Amount;               
                delivery.IsActive = true;

                DeliveryChargeDb.Attach(delivery);
                this.Entry(delivery).State = EntityState.Modified;
                this.SaveChanges();
                return delivery;
            }           
        }
        public List<ReturnOrderDispatchedDetails> add(List<ReturnOrderDispatchedDetails> po)
        {
            throw new NotImplementedException();
        }
        public void AddOrderMaster(OrderMaster item)
        {
            throw new NotImplementedException();
        }
        public object getCustomerbyid(object id)
        {
            throw new NotImplementedException();
        }

        public bool AllOrderMasterspriority(int time)
        {
            //var dateNow = DateTime.Now.AddDays(-1);
            //var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 20, 00, 00);

            var date = DateTime.Now.AddHours(-time);
            var result = DbOrderMaster.Where(x => x.CreatedDate <= date && x.Status == "Pending").ToList().OrderByDescending(x => x.orderDetails.Count());
            foreach (var a in result)
            {
                var flag = false;
                foreach (var o in a.orderDetails)
                {

                    var cs = DbCurrentStock.Where(x => x.ItemNumber == o.itemNumber).SingleOrDefault();
                    if (o.qty > cs.CurrentInventory)
                    {
                        flag = true;
                        break;
                    }

                }
                if (flag == false)
                {
                    a.Status = "Ready to Dispatch";
                    DbOrderMaster.Attach(a);
                    this.Entry(a).State = EntityState.Modified;
                    this.SaveChanges();

                    OrderDispatchedMaster odm = new OrderDispatchedMaster();
                    odm.OrderId = a.OrderId;
                    odm.Status = a.Status;
                    odm.CompanyId = a.CompanyId;
                    odm.SalesPersonId = a.SalesPersonId;
                    odm.SalesPerson = a.SalesPerson;
                    odm.SalesMobile = a.SalesMobile;
                    odm.CustomerId = a.CustomerId;
                    odm.CustomerName = a.CustomerName;
                    odm.ShopName = a.ShopName;
                    odm.Skcode = a.Skcode;
                    odm.invoice_no = a.invoice_no;
                    odm.CustomerCategoryId = a.CustomerCategoryId;
                    odm.CustomerCategoryName = a.CustomerCategoryName;
                    odm.CustomerType = a.CustomerType;
                    odm.Customerphonenum = a.Customerphonenum;
                    odm.BillingAddress = a.BillingAddress;
                    odm.ShippingAddress = a.ShippingAddress;
                    odm.comments = a.comments;
                    odm.deliveryCharge = a.deliveryCharge.GetValueOrDefault();
                    odm.TotalAmount = a.TotalAmount;
                    odm.GrossAmount = a.GrossAmount;
                    odm.DiscountAmount = a.DiscountAmount;
                    odm.TaxAmount = a.TaxAmount;
                    odm.SGSTTaxAmmount = a.SGSTTaxAmmount;
                    odm.CGSTTaxAmmount = a.CGSTTaxAmmount;
                    odm.CityId = a.CityId;
                    odm.Warehouseid = a.Warehouseid;
                    odm.WarehouseName = a.WarehouseName;
                    odm.active = a.active;
                    odm.OrderedDate = a.CreatedDate;
                    odm.CreatedDate = a.CreatedDate;
                    odm.Deliverydate = a.Deliverydate;
                    odm.UpdatedDate = indianTime;
                    odm.Deleted = false;
                    odm.DivisionId = a.DivisionId;
                    odm.ClusterId = a.ClusterId;
                    odm.ClusterName = a.ClusterName;
                    odm.WalletAmount = a.WalletAmount;
                    odm.RewardPoint = a.RewardPoint;
                    odm.OrderTakenSalesPersonId = a.OrderTakenSalesPersonId;
                    odm.OrderTakenSalesPerson = a.OrderTakenSalesPerson;
                    odm.Tin_No = a.Tin_No;
                    try
                    {
                        var cust = Customers.Where(c => c.CustomerId == odm.CustomerId).SingleOrDefault();
                        if (cust != null)
                        {
                            odm.lg = cust.lg;
                            odm.lat = cust.lat;
                        }
                    }
                    catch (Exception ex) { }
                    try
                    {
                        var delb = Peoples.Where(c => c.PeopleID == 3).SingleOrDefault();
                        if (delb != null)
                        {
                            odm.DboyName = delb.DisplayName;
                            odm.DboyMobileNo = delb.Mobile;
                        }
                    }
                    catch (Exception ex) { }


                    OrderDispatchedMasters.Add(odm);
                    this.SaveChanges();

                    foreach (var o in a.orderDetails)
                    {
                        o.Status = "Ready to Dispatch";
                        DbOrderDetails.Attach(o);
                        this.Entry(o).State = EntityState.Modified;
                        this.SaveChanges();

                        OrderDispatchedDetails odd = new OrderDispatchedDetails();
                        odd.OrderDispatchedMasterId = odm.OrderDispatchedMasterId;
                        odd.OrderDetailsId = o.OrderDetailsId;
                        odd.OrderId = o.OrderId;
                        odd.Status = o.Status;
                        odd.CustomerId = o.CustomerId;
                        odd.CustomerName = o.CustomerName;
                        odd.City = o.City;
                        odd.Mobile = o.Mobile;
                        odd.OrderDate = o.OrderDate;
                        odd.CompanyId = o.CompanyId; ;
                        odd.CityId = o.CityId;
                        odd.SizePerUnit = o.SizePerUnit;
                        odd.Warehouseid = o.Warehouseid;
                        odd.WarehouseName = o.WarehouseName;
                        odd.CategoryName = o.CategoryName;
                        odd.isDeleted = false;
                        odd.ItemId = o.ItemId;
                        odd.Itempic = o.Itempic;
                        odd.itemname = o.itemname;
                        odd.itemcode = o.itemcode;
                        odd.Barcode = o.Barcode;
                        odd.price = o.price;
                        odd.UnitPrice = o.UnitPrice;
                        odd.Purchaseprice = o.Purchaseprice;
                        odd.MinOrderQty = o.MinOrderQty;
                        odd.MinOrderQtyPrice = o.MinOrderQtyPrice;
                        odd.qty = o.qty;
                        // for new calculation
                        odd.Noqty = o.Noqty;
                        odd.AmtWithoutTaxDisc = o.AmtWithoutTaxDisc;
                        odd.AmtWithoutAfterTaxDisc = o.AmtWithoutAfterTaxDisc;
                        odd.TotalAmountAfterTaxDisc = o.TotalAmountAfterTaxDisc;
                        odd.NetAmmount = o.NetAmmount;
                        odd.DiscountPercentage = o.DiscountPercentage;
                        odd.DiscountAmmount = o.DiscountAmmount;
                        odd.NetAmtAfterDis = o.NetAmtAfterDis;
                        odd.TaxPercentage = o.TaxPercentage;
                        odd.TaxAmmount = o.TaxAmmount;
                        odd.SGSTTaxPercentage = o.SGSTTaxPercentage;
                        odd.SGSTTaxAmmount = o.SGSTTaxAmmount;
                        odd.CGSTTaxPercentage = o.CGSTTaxPercentage;
                        odd.CGSTTaxAmmount = o.CGSTTaxAmmount;
                        odd.TotalAmt = o.TotalAmt;
                        odd.itemNumber = o.itemNumber;
                        odd.CreatedDate = o.CreatedDate;
                        odd.UpdatedDate = indianTime;
                        odd.Deleted = false;

                        OrderDispatchedDetailss.Add(odd);
                        this.SaveChanges();
                        //here code currentstock
                        var csd = DbCurrentStock.Where(x => x.ItemNumber == odd.itemNumber).SingleOrDefault();
                        if (csd != null)
                        {

                               CurrentStockHistory Oss = new CurrentStockHistory();
                            if (csd != null)
                            {
                                Oss.StockId = csd.StockId;
                                Oss.ItemNumber = csd.ItemNumber;
                                Oss.ItemName = csd.ItemName;
                                Oss.CurrentInventory = csd.CurrentInventory;
                                Oss.OdOrPoId = odd.OrderId;
                                Oss.InventoryOut = Convert.ToInt32(odd.qty);
                                Oss.TotalInventory = Convert.ToInt32(csd.CurrentInventory - odd.qty);
                                Oss.WarehouseName = csd.WarehouseName;
                                Oss.CreationDate = DateTime.Now;
                                CurrentStockHistoryDb.Add(Oss);
                                int id = this.SaveChanges();
                            }

                            csd.CurrentInventory = Convert.ToInt32(csd.CurrentInventory - odd.qty);
                            UpdateCurrentStock(csd);


                        }
                      
                    }
                }
            }
            return true;
        }
        //Api CurrencyStock By Status

        public IEnumerable<CurrencyHistory> AllStockCurrencys(string Stock_status)
        {
            if (CurrencyHistoryDB.AsEnumerable().Count() > 0)
            {

                return CurrencyHistoryDB.Where(p => p.Deleted == false).AsEnumerable();

            }
            else
            {
                List<CurrencyHistory> StockCurrency = new List<CurrencyHistory>();
                return StockCurrency.AsEnumerable();
            }
        }
        public IEnumerable<CurrencyHistory> AllStockCurrencysHistory(string Stock_status)
        {
            if (CurrencyHistoryDB.AsEnumerable().Count() > 0)
            {

                return CurrencyHistoryDB.Where(p => p.Deleted == false).AsEnumerable();

            }
            else
            {
                List<CurrencyHistory> StockCurrency = new List<CurrencyHistory>();
                return StockCurrency.AsEnumerable();
            }
        }
        public IEnumerable<CurrencyHistory> TotalStockCurrencys(int id)
        {
            if (CurrencyHistoryDB.AsEnumerable().Count() > 0)
            {

                return CurrencyHistoryDB.Where(p => p.CurrencyHistoryid == id && p.Deleted == false).AsEnumerable();

            }
            else
            {
                List<CurrencyHistory> StockCurrencyy = new List<CurrencyHistory>();
                return StockCurrencyy.AsEnumerable();
            }
        }


        public IEnumerable<CurrencyBankSettle> AllBankStockCurrencys()
        {
            if (CurrencyBankSettleDB.AsEnumerable().Count() > 0)
            {

                return CurrencyBankSettleDB.Where(p =>p.Deleted == false).AsEnumerable();

            }
            else
            {
                List<CurrencyBankSettle> BankStockCurrency = new List<CurrencyBankSettle>();
                return BankStockCurrency.AsEnumerable();
            }
        }
       

        public IEnumerable<CurrencyBankSettle> Imagegetview(int id)
        {
            if (CurrencyBankSettleDB.AsEnumerable().Count() > 0)
            {

                return CurrencyBankSettleDB.Where(p => p.CurrencyBankSettleId == id && p.Deleted == false).AsEnumerable();

            }
            else
            {
                List<CurrencyBankSettle> StockCurrencyy1 = new List<CurrencyBankSettle>();
                return StockCurrencyy1.AsEnumerable();
            }
        }
        public IEnumerable<CheckCurrency> AllStockCurrencyscheck(string status)
        {
            if (CheckCurrencyDB.AsEnumerable().Count() > 0)
            {

                return CheckCurrencyDB.Where(p => p.Deleted == false).AsEnumerable();

            }
            else
            {
                List<CheckCurrency> Stockcheck = new List<CheckCurrency>();
                return Stockcheck.AsEnumerable();
            }
        }
        //public IEnumerable<DBoyCurrency> Alldueamount(string status, int PeopleID)
        //{
        //    if (DBoyCurrencyDB.AsEnumerable().Count() > 0)
        //    {

        //        return DBoyCurrencyDB.Where(p => p.PeopleId == PeopleID && p.Dueamountstatus == status).AsEnumerable();

        //    }
        //    else
        //    {
        //        List<DBoyCurrency> due = new List<DBoyCurrency>();
        //        return due.AsEnumerable();
        //    }
        //}
    }
}