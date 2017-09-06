
using AngularJSAuthentication.Model;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/customerupload")]

    public class CustomerUploadController : ApiController
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
        string msg, msgitemname;
        string strJSON = null;
        string col1, col2, col3, col4, col5, col6, col7, col8, col9, col10, col11,col12,col13,col14;
        [HttpPost]
        public string UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                logger.Info("start Item Upload Exel File: ");
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
                // Get the uploaded image from the Files collection
                System.Web.HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["file"];

                if (httpPostedFile != null)
                {
                    // Validate the uploaded image(optional)
                    byte[] buffer = new byte[httpPostedFile.ContentLength];
                    using (BinaryReader br = new BinaryReader(httpPostedFile.InputStream))
                    {
                        br.Read(buffer, 0, buffer.Length);
                    }
                    XSSFWorkbook hssfwb;
                    //   XSSFWorkbook workbook1;
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        BinaryFormatter binForm = new BinaryFormatter();
                        memStream.Write(buffer, 0, buffer.Length);
                        memStream.Seek(0, SeekOrigin.Begin);
                        hssfwb = new XSSFWorkbook(memStream);
                        string sSheetName = hssfwb.GetSheetName(0);
                        ISheet sheet = hssfwb.GetSheet(sSheetName);
                        AuthContext context = new AuthContext();
                        IRow rowData;
                        ICell cellData = null;
                        try
                        {
                            List<Customer> CustCollection = new List<Customer>();
                            for (int iRowIdx = 0; iRowIdx <= sheet.LastRowNum; iRowIdx++)  
                            {
                                if (iRowIdx == 0)
                                {
                                }
                                else
                                {
                                    rowData = sheet.GetRow(iRowIdx);
                                    cellData = rowData.GetCell(0);
                                    rowData = sheet.GetRow(iRowIdx);
                                    if (rowData != null)
                                    {
                                        Customer cust = new Customer();
                                        try
                                        {
                                            cust.CompanyId = 1;
                                            cellData = rowData.GetCell(1);
                                            col1 = cellData == null ? "" : cellData.ToString();
                                            if (col1.Trim() == "" || col1 == null || col1 == "null")
                                                break;                                      
                                            cust.Skcode = col1.Trim();

                                            cellData = rowData.GetCell(2);
                                            col2 = cellData == null ? "" : cellData.ToString();
                                            cust.ShopName = col2.Trim();

                                            cellData = rowData.GetCell(3);
                                            col3 = cellData == null ? "" : cellData.ToString();
                                            cust.Name = col3.Trim();

                                            cellData = rowData.GetCell(4);
                                            col4 = cellData == null ? "" : cellData.ToString();
                                            if (col4.Trim() == "" || col4 == null)
                                                break;
                                            cust.Mobile = col4.Trim();

                                            cellData = rowData.GetCell(5);
                                            col5 = cellData == null ? "" : cellData.ToString();
                                            cust.BillingAddress = col5.Trim();

                                            cellData = rowData.GetCell(6);
                                            col6 = cellData == null ? "" : cellData.ToString();
                                            cust.LandMark = col6.Trim();

                                            cellData = rowData.GetCell(7);
                                            col7 = cellData == null ? "" : cellData.ToString();
                                            Warehouse wh = context.Warehouses.Where(w => w.WarehouseName == col7.Trim()).FirstOrDefault();
                                            if (wh != null)
                                            {
                                                cust.Warehouseid = wh.Warehouseid;
                                                cust.City = wh.CityName;
                                                cust.WarehouseName = wh.WarehouseName;
                                            }

                                            cellData = rowData.GetCell(8);
                                            col8 = cellData == null ? "" : cellData.ToString();
                                            List<People> peoples = new List<People>();
                                            try
                                            {
                                                peoples = context.Peoples.Where(x => x.DisplayName.Trim().ToLower() == col8.Trim().ToLower()).ToList();
                                                if (peoples.Count != 0)
                                                {
                                                    cust.ExecutiveId = peoples[0].PeopleID;
                                                }
                                                else cust.ExecutiveId = 0;
                                            }
                                            catch (Exception ex) {
                                                cust.ExecutiveId = 0;
                                            }
                                            
                                            cellData = rowData.GetCell(9);
                                            col9 = cellData == null ? "" : cellData.ToString();
                                            if (col9.Trim() != "" && col9 != null)
                                            {
                                                cust.Emailid = col9.Trim();
                                            }

                                            cellData = rowData.GetCell(10);
                                            col10 = cellData == null ? "" : cellData.ToString();
                                            try {
                                                cust.ClusterId = Convert.ToInt32(col10.Trim());
                                            } catch (Exception ex) {
                                                cust.ClusterId = 1;
                                            }

                                            cellData = rowData.GetCell(11);
                                            col11 = cellData == null ? "" : cellData.ToString();
                                            if (col11.Trim() != "" && col11 != null)
                                            {
                                                cust.Day = col11.Trim();
                                            }

                                            cellData = rowData.GetCell(12);
                                            col12= cellData == null ? "" : cellData.ToString();
                                            if (col12.Trim() == "" && col12 == null)
                                                break;                                   
                                            cust.lat = Convert.ToDouble(col12.Trim());
                                           
                                            cellData = rowData.GetCell(13);
                                            col13 = cellData == null ? "" : cellData.ToString();
                                            if (col13.Trim() == "" && col13 == null)
                                                break;
                                            cust.lg = Convert.ToDouble(col13.Trim());
                                            
                                            cellData = rowData.GetCell(14);
                                            col14 = cellData == null ? "" : cellData.ToString();
                                            if (col14.Trim() != "" && col14 != null)
                                            {
                                                cust.BeatNumber = Convert.ToInt32(col14.Trim());
                                            }

                                            cust.Active = true;
                                            cust.Password = "123456";
                                            cust.CompanyId = compid;

                                            CustCollection.Add(cust);                                            
                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Error("Error adding customer in collection " + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace + cust.Name);
                                        }
                                    }
                                }
                            }
                            context.AddBulkcustomer(CustCollection);
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Error loading for \n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);

                        }
                    }
                    var FileUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

                    httpPostedFile.SaveAs(FileUrl);
                }
            }
            if (msgitemname != null)
            {
                return msgitemname;
            }
            msg = "Your Exel data is succesfully saved";
            return msg;
        }

    }
}



//using AngularJSAuthentication.Model;
//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;

//namespace AngularJSAuthentication.API.Controllers
//{
//    [RoutePrefix("api/customerupload")]
//    public class CustomerUploadController : ApiController
//    {
//        [HttpPost]
//        public void UploadFile()
//        {
//            if (HttpContext.Current.Request.Files.AllKeys.Any())
//            {
//                var identity = User.Identity as ClaimsIdentity;
//                int compid = 0, userid = 0;
//                // Access claims
//                foreach (Claim claim in identity.Claims)
//                {
//                    if (claim.Type == "compid")
//                    {
//                        compid = int.Parse(claim.Value);
//                    }
//                    if (claim.Type == "userid")
//                    {
//                        userid = int.Parse(claim.Value);
//                    }
//                }
//                // Get the uploaded image from the Files collection
//                System.Web.HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["file"];

//                if (httpPostedFile != null)
//                {
//                    // Validate the uploaded image(optional)
//                    byte[] buffer = new byte[httpPostedFile.ContentLength];

//                    using (BinaryReader br = new BinaryReader(httpPostedFile.InputStream))

//                    {

//                        br.Read(buffer, 0, buffer.Length);

//                    }
//                    XSSFWorkbook hssfwb;
//                    //   XSSFWorkbook workbook1;
//                    using (MemoryStream memStream = new MemoryStream())
//                    {
//                        BinaryFormatter binForm = new BinaryFormatter();
//                        memStream.Write(buffer, 0, buffer.Length);
//                        memStream.Seek(0, SeekOrigin.Begin);
//                        hssfwb = new XSSFWorkbook(memStream);
//                        string sSheetName = hssfwb.GetSheetName(0);
//                        ISheet sheet = hssfwb.GetSheet(sSheetName);
//                        AuthContext context = new AuthContext();
//                        IRow rowData;
//                        ICell cellData = null;
//                        try
//                        {
//                            for (int iRowIdx = 1; iRowIdx <= sheet.LastRowNum; iRowIdx++)  //  iRowIdx = 0; HeaderRow
//                            {
//                                rowData = sheet.GetRow(iRowIdx);

//                                if (rowData != null)
//                                {
//                                    Customer Cust   = new Customer();

//                                    Cust.CompanyId = 3; // compid;
//                                    cellData = rowData.GetCell(3);

//                                    try
//                                    {
//                                        Cust.Name = cellData == null ? "" : cellData.ToString();

//                                        cellData = rowData.GetCell(9);
//                                        Cust.CustomerType = cellData == null ? "" : cellData.ToString();

//                                    }
//                                    catch (Exception ex) { }

//                                    try
//                                    {
//                                        cellData = rowData.GetCell(10);
//                                        if (cellData != null)
//                                        {
//                                            Cust.BillingAddress = cellData == null ? "" : cellData.ToString();
//                                        }
//                                    }
//                                    catch (Exception ex) { }
//                                    try
//                                    {
//                                        cellData = rowData.GetCell(11);
//                                        if (cellData != null)
//                                        {
//                                            Cust.ShippingAddress = cellData == null ? "" : cellData.ToString();
//                                        }
//                                    }
//                                    catch (Exception ex) { }
//                                    try
//                                    {
//                                        cellData = rowData.GetCell(12);
//                                        if (cellData != null)
//                                        {
//                                            Cust.City = cellData == null ? "" : cellData.ToString();
//                                        }
//                                    }
//                                    catch (Exception ex) { }
//                                    try
//                                    {
//                                        cellData = rowData.GetCell(1);
//                                        if (cellData != null)
//                                        {
//                                            Cust.Name = cellData == null ? "" : cellData.ToString();
//                                        }
//                                    }
//                                    catch (Exception ex) { }
//                                    try
//                                    {
//                                        cellData = rowData.GetCell(17);
//                                        if (cellData != null)
//                                        {
//                                            Cust.Emailid = cellData == null ? "" : cellData.ToString();
//                                        }
//                                    }
//                                    catch (Exception ex) { }
//                                    try
//                                    {
//                                        cellData = rowData.GetCell(19);
//                                        if (cellData != null)
//                                        {
//                                            Cust.CustomerCategoryName = cellData == null ? "" : cellData.ToString();
//                                        }
//                                    }
//                                    catch (Exception ex) { }
//                                    try
//                                    {
//                                        cellData = rowData.GetCell(15);
//                                        Cust.RefNo = cellData == null ? "" : cellData.ToString();

//                                        cellData = rowData.GetCell(16);
//                                        Cust.OfficePhone = cellData == null ? "" : cellData.ToString();


//                                    }
//                                    catch (Exception ex) { }
//                                    context.AddCustomerExcel(Cust);


//                                }
//                            }
//                            // _UpdateStatus = true;
//                        }
//                        catch (Exception ex)
//                        {
//                            //  logger.Error("Error loading URL for " + URL + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);

//                        }
//                    }

//                    var FileUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

//                    httpPostedFile.SaveAs(FileUrl);
//                }
//            }
//        }
//    }

//}
