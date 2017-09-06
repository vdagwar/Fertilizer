﻿using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using AngularJSAuthentication.API.Controllers;
using AngularJSAuthentication.API.ControllerV1;
using GenricEcommers.Models;
//using GenricEcommers.Models;
//using AngularJSAuthentication.API.SK.Models;

namespace AngularJSAuthentication.API
{
    /// <summary>
    /// This interface contains all defination of AuthContext Class 
    /// </summary>
    public interface iAuthContext
    {
        //IEnumerable<DBoyCurrency> Alldueamount(string status, int PeopleID);
        IEnumerable<CurrencyBankSettle> Imagegetview(int id);
        IEnumerable<CurrencyHistory> TotalStockCurrencys(int id);
        IEnumerable<CheckCurrency>AllStockCurrencyscheck(string status);
        IEnumerable<CurrencyHistory> AllStockCurrencys(string Stock_status);
        IEnumerable<CurrencyHistory> AllStockCurrencysHistory(string Stock_status);
        IEnumerable<CurrencyBankSettle> AllBankStockCurrencys();
        IEnumerable<People> AllPeoplesDep(string dep);
        string skcode();
        string deliveryIssuance(DeliveryIssuance OBJ);
        List<People> AllDBoy();
        People AddDboys(People city);
        People PutDboys(People city);
        bool DeleteDboys(int id);
        bool AllOrderMasterspriority(int time);
        List<Vehicle> AllVehicles();
        Vehicle AddVehicle(Vehicle city);
        Vehicle PutVehicle(Vehicle city);
        bool DeleteVehicle(int id);
        SalesPersonBeat Addsalesbeat(SalesPersonBeat obj);
        IList<EditPriceHistory> filteredEditPriceHistory(DateTime? start, DateTime? end, string cityid, string categoryid, string subcategoryid, string subsubcategoryid);
        IList<Customer> filteredCustomerReport(DateTime? datefrom, DateTime? dateto);
        IList<OrderDetails> OrderMonthReport(DateTime? datefrom, DateTime? dateto);
        IEnumerable<OrderMaster> AllOrderMasters { get; }
        IEnumerable<DamageOrderMaster> AllDOrderMasters { get; }

        //search Orders
        List<OrderMaster> searchorderbycustomer(DateTime? start, DateTime? end, int OrderId, string Skcode, string ShopName, string Mobile, string status);
        List<OrderHistory> getDBoyOrdersHistory(string mob, DateTime? start, DateTime? end, int dboyId);
        List<OrderDispatchedMasterDTO> getAcceptedOrders(string mob);
        OrderDispatchedMaster orderdeliveredreturn(OrderDispatchedMaster obj);
        List<OrderDispatchedMaster> changeDBoy(List<OrderDispatchedMaster> objlist,string mob);
        List<OrderDispatchedMaster> getdboysOrder(string mob);
        List<DBoyCurrency> getdboysCurrency(int PeopleID);
 
        Customer Resetpassword(Customer customer);
        Customer AllSalePersonRetailer(string srch, int id1);
        IEnumerable<OrderDispatchedMaster> AllDispatchedOrderMaster();               
        List<PurchaseOrderMaster> addPurchaseOrderMaster(List<TempPO> OBJ);
        PurchaseOrder AddPurchaseItem(PurchaseOrder poItem);
        GpsCoordinate Addgps(GpsCoordinate obj);
        PurchaseOrderDetailRecived AddPurchaseOrderDetailsRecived(PurchaseOrderDetailRecived pd, int count);
        PurchaseOrderMasterRecived AddPurchaseOrderMasterRecived(PurchaseOrderMasterRecived po);
        PurchaseOrderMaster AllPOrderDetails1(int i);
        IEnumerable<FinalOrderDispatchedDetails> AllFOrderDispatchedDetails(int i);
        OrderDispatchedMaster AddOrderDispatchedMaster(OrderDispatchedMaster dm);
        OrderDispatchedDetails AddOrderDispatchedDetails(OrderDispatchedDetails dd);
        OrderDispatchedMaster UpdateOrderDispatchedMaster(OrderDispatchedMaster om);
        IEnumerable<OrderDispatchedDetails> AllPOrderDispatchedDetails(int i);
        IEnumerable<ReturnOrderDispatchedDetails> AllReturnOrderDispatchedDetails(int i);
        CurrentStock UpdateCurrentStock(CurrentStock stock);
        OrderMaster PutOrderMaster(OrderMaster city);
        Feedback AddFeedBack(Feedback obj);
        RequestItem AddRequestItem(RequestItem obj);
        List<Favorites> AllFavorites(string mob);
        Favorites AddFavorites(Favorites Favt);
        PaggingData AllItemMasterForPaging(int list, int page, string warehouse);
        InvoiceRow AddInvoiceDetail(InvoiceRow e);       
        AllInvoice AddInvoice(AllInvoice customer);
        FinalOrderDispatchedMaster AddFinalOrderDispatchedMaster(FinalOrderDispatchedMaster final);        
        List<OrderDispatchedDetailsFinalController.filtered> AllFOrderDispatchedReportDetails(DateTime datefrom, DateTime dateto);
        List<OrderDispatchedMaster> AllFOrderDispatchedDeliveryDetails(DateTime datefrom, DateTime dateto);
        #region Customer
        People CheckPeople(string mob,string password);
        IEnumerable<Customer> AllCustomers { get; }
        IEnumerable<CustomerRegistration> Allcustomers();
        IEnumerable<Customer> AllCustomerbyCompanyId(int cmpid);
        Customer AddCustomer(Customer customer);
        Customer PutCustomer(Customer customer);
        Customer GetClientforProjectId(int projId);
        bool DeleteCustomer(int id);
        //IList<Customer> filteredCustomerMaster(string Cityid, string Warehouseid, DateTime? datefrom, DateTime? dateto);
        IList<Customer> filteredCustomerMaster(string Cityid, DateTime? datefrom, DateTime? dateto, string mobile, string skcode);
        object getCustomerbyid(object id);
        List<Customer> AddBulkcustomer(List<Customer> CustCollection);
        List<People> AddBulkpeople(List<People> CustCollection);
        #endregion
        List<OrderDispatchedMaster> AllFOrderDispatchedDeliveryBoyDetails(DateTime datefrom, DateTime dateto, string DboyName);
        Customer GetCustomerbyId(string Mobile);
        PaggingData AllOrderMaster(int list, int page);
        PaggingData_st AllItemHistory(int list, int page,int StockId);
        PaggingData AllDOrderMaster(int list, int page);
        bool DeleteOrderMaster(int id);
        IEnumerable<OrderDetails> AllOrderDetails(int i);
        IEnumerable<DamageOrderDetails> AllDOrderDetails(int i);
        IList<DemandDetailsNew> AllfilteredOrderDetails(string Cityid, string Warehouseid, DateTime datefrom, DateTime dateto);
        IList<PurchaseOrderList> AllfilteredOrderDetails2(string Cityid, string Warehouseid, DateTime? datefrom, DateTime? dateto);
        IList<OrderMaster> filteredOrderMasters1(string Warehouseid, DateTime datefrom, DateTime dateto);
        IList<OrderMaster> filteredOrderMaster(string Cityid, string Warehouseid, DateTime datefrom, DateTime dateto, string search, string status, string deliveryboy);
        OrderMaster AddOrderMaster(ShoppingCart sc);
        IEnumerable<OrderDetails> Allorddetails(int compid);
        IEnumerable<DamageOrderDetails> AllDorddetails(int compid);
        IEnumerable<PurchaseOrder> AllPurchaseOrder(int compid);
        List<PurchaseOrderList> AddPurchaseOrder(List<PurchaseOrderList> po);
        List<ReturnOrderDispatchedDetails> add(List<ReturnOrderDispatchedDetails> po);
        IEnumerable<ItemMaster> AllItemMaster();
        IEnumerable<ItemMaster> itembyid(int id);
        List<ItemMaster> AddBulkItemMaster(List<ItemMaster> itemCollection);
        ItemMaster AddItemMaster(ItemMaster itemmaster);
        List<ItemMaster> AddItemMove(List<MoveWarehouse> item, int warehid);
        ItemMaster PutItemMaster(ItemMaster itemmaster);
        ItemMaster Saveediteditem(List<ItemMaster> itemmasterList);
        bool DeleteItemMaster(int id);
        IList<ItemMaster> filteredItemMaster(string cityid, string categoryid, string subcategoryid, string subsubcategoryid);        
        People getPersonIdfromEmail(string email);
        IEnumerable<Company> AllCompanies { get; }
        Company AddCompany(Company company);
        Company PutCompany(Company company);
        Company GetCompanybyCompanyId(int id);
        bool DeleteCompany(int id);        
        bool CompanyExists(string companyName);
        IEnumerable<ProjectTask> AllProjectTask { get; }
        IEnumerable<ProjectTask> AllProjectTaskbyCompanyId(int cmpid);
        List<ProjectTask> AllProjectTaskByuserId(int userid);
        ProjectTask GetProjectTaskById(int id);
        ProjectTask AddProjectTask(ProjectTask projectTask);
        ProjectTask PutProjectTask(ProjectTask projectTask);
        bool DeleteProjectTask(int id);
        IEnumerable<TaxMaster> AllTaxMaster(int compid);
        TaxMaster AddTaxMaster(TaxMaster taxMaster);
        TaxMaster PutTaxMaster(TaxMaster taxMaster);
        bool DeleteTaxMaster(int id);
        IEnumerable<TaxGroup> AllTaxGroup(int compid);
        TaxGroup AddTaxGroup(TaxGroup taxGroup);
        TaxGroup PutTaxGroup(TaxGroup taxGroup);
        bool DeleteTaxGroup(int id);
        TaxGroupDetails AddTaxGRPDetail(TaxGroupDetails taxGroupDetails);
        IEnumerable<TaxGroupDetails> AlltaxgroupDetails(int i);
        IEnumerable<Event> AllEvents(int userid);
        IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate);
        IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate, int compid);
        IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate, int userid, int compid);
        Event AddEvent(Event e);
        Event UpdateEvent(Event e);
        bool DeleteEvent(int id);
        Event UpdateEventByViewModel(DayEventViewModel model, string d, int userid, int compid);
        Event UpdateEventByViewModel(WeekEventViewModel model, string d, int userid, int compid);
        IEnumerable<Project> AllProjects { get; }    
        IEnumerable<Project> AllProjectsbyCompanyId(int cmpid);
        IEnumerable<Project> AllActiveProjectsbyCompanyId(int cmpid);
        Project AddProject(Project project);
        Project PutProject(Project project);
        bool DeleteProject(int id);
        IEnumerable<TaskType> AllTaskTypes(int compid);
        IEnumerable<TaskType> AllTaskTypesbyCompanyId(int cmpid);
        TaskType AddTaskType(TaskType customer);
        TaskType PutTaskType(TaskType customer);
        TaskType GetTaskTypeById(int id);
        bool DeleteTaskType(int id);
        IEnumerable<Leave> AllLeaves(int compid);
        Leave AddLeave(Leave leave);
        void AddOrderMaster(OrderMaster item);
        Leave PutLeave(Leave leave);
        bool DeleteLeave(int id);
        IEnumerable<Assets> AllAssets(int compid);
        Assets AddAssets(Assets asset);
        Assets PutAssets(Assets asset);
        bool DeleteAssets(int id);
        IEnumerable<AssetsCategory> AllAssetsCategory(int compid);
        IEnumerable<ItemPramotions> AllItemPramotion();
        AssetsCategory AddAssetsCategory(AssetsCategory asset);
        AssetsCategory PutAssetsCategory(AssetsCategory asset);
        bool DeleteAssetsCategory(int id);
        IEnumerable<CustomerCategory> AllCustomerCategory(int compid);
        CustomerCategory AddCustomerCategory(CustomerCategory Cust);
        CustomerCategory PutCustomerCategory(CustomerCategory Cust);
        bool DeleteCustomerCategory(int id);
        IEnumerable<TravelRequest> AllTravelRequestByCompId(int compid);
        IEnumerable<TravelRequest> AllTravelrequestByUserId(int cmpid);
        TravelRequest AddTravelRequest(TravelRequest travelrequest);
        TravelRequest PutTravelRequest(TravelRequest travelrequest);
        TravelRequest GetTravelRequestById(int id);
        bool DeleteTravelRequest(int id);
        IEnumerable<People> AllPeoples(int compid);
        List<People> GetPeoplebyCompanyId(int id);
        People GetPeoplebyId(int id);
        People AddPeople(People people);
        People AddPeoplebyAdmin(People people);
        People PutPeople(People people);
        People PutPeoplebyAdmin(People people);
        bool DeletePeople(int id);
        IEnumerable<SubsubCategory> AllSubsubCat(int compid);
        SubsubCategory AddSubsubCat(SubsubCategory asset);
        SubsubCategory AddQuesAnsxl(SubsubCategory asset);
        SubsubCategory PutSubsubCat(SubsubCategory asset);
        bool DeleteSubsubCat(int id);
        IEnumerable<Category> AllCategory(int compid);
        Category AddCategory(Category category);
        Category PutCategory(Category category);
        bool DeleteCategory(int id);
        IEnumerable<State> Allstates(int compid);
        State AddState(State state);
        State PutState(State state);
        bool DeleteState(int id);
        IEnumerable<Role> AllRoles(int compid);
        Role AddRole(Role role);
        Role PutRoles(Role role);
        bool DeleteRole(int id);
        IEnumerable<City> AllCitys(int compid);
        IEnumerable<City> AllCity(int sid);
        City AddCity(City city);
        City PutCity(City city);
        bool DeleteCity(int id);
        IEnumerable<Warehouse> AllWarehouse(int compid);
        Warehouse AddWarehouse(Warehouse warehouse);
        Warehouse PutWarehouse(Warehouse warehouse);
        bool DeleteWarehouse(int id);
        IEnumerable<Warehouse> AllWHouseforapp();
        Cluster Addcluster(Cluster item);
        IEnumerable<Cluster> AllCluster(int compid);
        Cluster getClusterbyid(int id);
        Cluster UpdateCluster(Cluster item);
        bool DeleteCluster(int id);
        IEnumerable<WarehouseSupplier> AllWarehouseSupplier(int compid);
        WarehouseSupplier AddWarehouseSupplier(WarehouseSupplier warehouseSupplier);
        WarehouseSupplier PutWarehouseSupplier(WarehouseSupplier warehouseSupplier);
        bool DeleteWarehouseSupplier(int id);
        WarehouseCategory Addwarehousecatxl(WarehouseCategory warehousecategory);
        IEnumerable<WarehouseCategory> AllWarehouseCategory(int compid);
        IEnumerable<WarehouseCategory> AllWhCategory();
        List<WarehouseCategory> AddWarehouseCategory(List<WarehouseCategory> WarehouseCategory, string desc);
        List<WarehouseCategory> PutWarehouseCategory(List<WarehouseCategory> WarehouseCategory, string desc);
        bool DeleteWarehouseCategory(int id);
        IEnumerable<WarehouseSubCategory> AllWarehouseSubCategory(int WarehouseSubCategoryid);
        WarehouseSubCategory AddWarehouseSubCategory(WarehouseSubCategory WarehouseSubCategory);
        WarehouseSubCategory PutWarehouseSubCategory(WarehouseSubCategory WarehouseSubCategory);
        bool DeleteWarehouseSubCategory(int id);
        IEnumerable<WarehouseSubsubCategory> AllWarehouseSubsubCat(int compid);
        WarehouseSubsubCategory AddWarehouseSubsubCat(WarehouseSubsubCategory asset);
        WarehouseSubsubCategory AddWhsubsubxl(WarehouseSubsubCategory asset);
        WarehouseSubsubCategory PutWarehouseSubsubCat(WarehouseSubsubCategory asset);
        bool DeletewarehouseSubsubCat(int id);
        IEnumerable<SubCategory> AllSubCategory(int compid);
        IEnumerable<SubCategory> AllSubCategoryy(int subcat);
        SubCategory AddSubCategory(SubCategory subCategory);
        SubCategory PutSubCategory(SubCategory subCategory);
        bool DeleteSubCategory(int id);
        IEnumerable<SupplierCategory> AllSupplierCategory();
        SupplierCategory AddSupplierCategory(SupplierCategory supplierCategory);
        SupplierCategory PutSupplierCategory(SupplierCategory supplierCategory);
        bool DeleteSupplierCategory(int id);
        IEnumerable<Supplier> AllSupplier();
        List<Supplier> AddBulkSupplier(List<Supplier> supCollection);
        Supplier AddSupplier(Supplier supplier);
        Supplier PutSupplier(Supplier supplier);
        bool DeleteSupplier(int id);
        IEnumerable<BillPramotion> AllBillPramtion();        
        BillPramotion AddBillPramtion(BillPramotion pramtion);
        BillPramotion PutBillPramtion(BillPramotion pramtion);
        bool DeleteBillPramtion(int id);

        IEnumerable<ItemBrand> AllItemBrand(int compid);
        ItemBrand AddItemBrand(ItemBrand itembrand);
        ItemBrand PutItemBrand(ItemBrand itembrand);
        bool DeleteItemBrand(int id);
        IEnumerable<FinancialYear> AllFinancialYear(int compid);
        FinancialYear AddFinancialYear(FinancialYear financialYear);
        FinancialYear PutFinancialYear(FinancialYear financialYear);
        bool DeleteFinancialYear(int id);
        IEnumerable<OrderMaster> OrderMasterbymobile(string mobile);
        Warehouse getwarehousebyid(int id);
        List<SubCategory> subcategorybyWarehouse(int id);
        List<SubCategory> Updatebrands(List<SubCategory> sub);
        List<SubCategory> subcategorybyPramotion(int id);
        List<SubCategory> subcategorybycity(int id);
        List<SubCategory> PramotionalBrand(int warehouseid);
        IEnumerable<PurchaseOrderMaster> AllPOMaster();
        IEnumerable<PurchaseOrderDetail> AllPOdetails(int compid);
        IEnumerable<PurchaseOrderDetail> AllPOrderDetails(int i);
        CurrentStock GetCurrentStock(int id);


        CurrentStockHistory PutCurrentStock(CurrentStockHistory CurrentStockHistory);
        IEnumerable<CurrentStock> GetAllCurrentStock();
        IEnumerable<ItemMaster> itembystring(string itemnm);
        OrderMaster GetOrderMaster(int orderid);
        DamageOrderMaster GetDOrderMaster(int orderid);
    }
}