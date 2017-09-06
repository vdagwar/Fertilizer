using AngularJSAuthentication.Model;
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
    [RoutePrefix("api/WarehouseSupplierUpload")]
    public class WarehouseSupplierUploadController : ApiController
    {
        [HttpPost]
        public string UploadFile()
        {
            string msg;
            string strJSON = null;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
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
                            for (int iRowIdx = 0; iRowIdx <= sheet.LastRowNum; iRowIdx++)  //  iRowIdx = 0; HeaderRow
                            {
                                if (iRowIdx == 0)
                                {
                                    rowData = sheet.GetRow(iRowIdx);
                                    
                                    if (rowData != null)
                                    {
                                        string col0, col1, col2, col3, col4, col5, field;
                                        col0 = "Whsupid"; col1 = "StateName";
                                        col2 = "CityName"; col3 = "WarehouseName";
                                        col4 = "SupplierName"; col5 = "Active";
                                        cellData = rowData.GetCell(0);
                                        field = cellData.ToString();
                                        if (col0 == field)
                                        {
                                            cellData = rowData.GetCell(1);
                                            field = cellData.ToString();
                                            if (col1 == field)
                                            {
                                                cellData = rowData.GetCell(2); field = cellData.ToString();
                                                if (col2 == field)
                                                {
                                                    cellData = rowData.GetCell(3); field = cellData.ToString();
                                                    if (col3 == field)
                                                    {
                                                        cellData = rowData.GetCell(4); field = cellData.ToString();
                                                        if (col4 == field)
                                                        {
                                                            cellData = rowData.GetCell(5); field = cellData.ToString();
                                                            if (col5 == field) { }
                                                            else { JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(field); return strJSON; }
                                                        }
                                                        else { JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(field); return strJSON; }
                                                    }
                                                    else { JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(field); return strJSON;}
                                                }
                                                else { JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(field);return strJSON; }
                                            }
                                            else { JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(field); return strJSON; }
                                        }//end check col
                                        else { msg = "Your Hesder are not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }
                                      }//oute if
                                    }
                                else
                                {
                                    rowData = sheet.GetRow(iRowIdx);
                                     string obj;
                                    if (rowData != null)
                                    {
                                        WarehouseSupplier ws = new WarehouseSupplier();
                                        cellData = rowData.GetCell(0);
                                        int id = Convert.ToInt32(cellData.ToString());
                                        WarehouseSupplier warehsup = context.DbWarehouseSupplier.Where(s => s.Whsupid.Equals(id)).Select(s => s).FirstOrDefault();
                                        if (warehsup != null)
                                        {
                                            ws.Whsupid = id;
                                            ws.CompanyId = 1; // compid;
                                            cellData = rowData.GetCell(9);

                                            try
                                            {
                                                cellData = rowData.GetCell(1);
                                                obj = cellData.ToString();
                                                State State = context.States.Where(x => x.StateName == obj).Where(x => x.Deleted == false).Select(x => x).FirstOrDefault();
                                                if (State != null)
                                                {
                                                    ws.Stateid = State.Stateid;
                                                    ws.StateName = State.StateName;
                                                }
                                                else { msg = "Your " + obj + " Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }
                                                cellData = rowData.GetCell(2);
                                                obj = cellData.ToString();
                                                City city = context.Cities.Where(x => x.CityName == obj).Select(x => x).FirstOrDefault();
                                                if (city != null)
                                                {
                                                    ws.Cityid = city.Cityid;
                                                    ws.CityName = city.CityName;
                                                }
                                                else { msg = "Your" + obj + " Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }

                                                cellData = rowData.GetCell(3);
                                                obj = cellData == null ? "" : cellData.ToString();
                                                Warehouse WareHouse = context.Warehouses.Where(x => x.WarehouseName == obj).Select(x => x).FirstOrDefault();
                                                if (WareHouse != null)
                                                {
                                                    ws.Warehouseid = WareHouse.Warehouseid;
                                                    ws.WarehouseName = WareHouse.WarehouseName;
                                                }
                                                else { msg = "Your" + obj + "Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }
                                                cellData = rowData.GetCell(4);
                                                obj = cellData == null ? "" : cellData.ToString();
                                                Supplier Suppplier = context.Suppliers.Where(x => x.Name == obj).Select(x => x).FirstOrDefault();
                                                if (Suppplier != null)
                                                {
                                                    ws.SupplierId = Suppplier.SupplierId;
                                                    ws.SupplierName = Suppplier.Name;
                                                }
                                                else
                                                {
                                                    msg = "Your" + obj + "Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON;

                                                }
                                                cellData = rowData.GetCell(5);
                                                ws.Active = Convert.ToBoolean(cellData == null ? "" : cellData.ToString());

                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                            context.PutWarehouseSupplierExcel(ws);
                                        }
                                        else
                                        {


                                            ws.CompanyId = 1; // compid;
                                            cellData = rowData.GetCell(9);

                                            try
                                            {
                                                cellData = rowData.GetCell(1);
                                                obj = cellData.ToString();
                                                State State = context.States.Where(x => x.StateName == obj).Where(x => x.Deleted == false).Select(x => x).FirstOrDefault();
                                                if (State != null)
                                                {
                                                    ws.Stateid = State.Stateid;
                                                    ws.StateName = State.StateName;
                                                }
                                                else { msg = "Your " + obj + " Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }
                                                cellData = rowData.GetCell(2);
                                                obj = cellData.ToString();
                                                City city = context.Cities.Where(x => x.CityName == obj).Select(x => x).FirstOrDefault();
                                                if (city != null)
                                                {
                                                    ws.Cityid = city.Cityid;
                                                    ws.CityName = city.CityName;
                                                }
                                                else { msg = "Your" + obj + " Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }

                                                cellData = rowData.GetCell(3);
                                                obj = cellData == null ? "" : cellData.ToString();
                                                Warehouse WareHouse = context.Warehouses.Where(x => x.WarehouseName == obj).Select(x => x).FirstOrDefault();
                                                if (WareHouse != null)
                                                {
                                                    ws.Warehouseid = WareHouse.Warehouseid;
                                                    ws.WarehouseName = WareHouse.WarehouseName;
                                                }
                                                else { msg = "Your" + obj + "Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON; }
                                                cellData = rowData.GetCell(4);
                                                obj = cellData == null ? "" : cellData.ToString();
                                                Supplier Suppplier = context.Suppliers.Where(x => x.Name == obj).Select(x => x).FirstOrDefault();
                                                if (Suppplier != null)
                                                {
                                                    ws.SupplierId = Suppplier.SupplierId;
                                                    ws.SupplierName = Suppplier.Name;
                                                }
                                                else
                                                {
                                                    msg = "Your" + obj + "Does not Exist"; JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize(msg); return strJSON;

                                                }
                                                cellData = rowData.GetCell(5);
                                                ws.Active = Convert.ToBoolean(cellData == null ? "" : cellData.ToString());

                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                            context.AddWarehouseSupplierExcel(ws);
                                        }
                                    }
                                }
                             }//end for
                            // _UpdateStatus = true;
                           }
                        catch (Exception ex)
                        {
                            

                        }
                    }
                 
                    var FileUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
          
                    httpPostedFile.SaveAs(FileUrl);
                }
            }
            msg = "Sucesss";
           
            return msg;
        }
    }

}
