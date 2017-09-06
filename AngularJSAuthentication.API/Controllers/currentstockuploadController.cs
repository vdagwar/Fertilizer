
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
    [RoutePrefix("api/currentstockupload")]
    public class currentstockuploadController : ApiController
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
        string msg , msgitemname;
        string strJSON = null;
        string col0, col1, col2, col3, col4;
        [HttpPost]
        public string UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                logger.Info("start current stock Upload Exel File: ");
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
                            List<CurrentStock> currentstkcollection = new List<CurrentStock>();
                            for (int iRowIdx = 0; iRowIdx <= sheet.LastRowNum; iRowIdx++)  //  iRowIdx = 0; HeaderRow
                            {
                                if (iRowIdx == 0)
                                {
                                    rowData = sheet.GetRow(iRowIdx);

                                    if (rowData != null)
                                    {
                                        string field = string.Empty;
                                        field = rowData.GetCell(0).ToString();
                                        if (field != "ItemNumber")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(1).ToString();
                                        if (field != "StockId")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(2).ToString();
                                        if (field != "ItemName")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(3).ToString();
                                        if (field != "CurrentInventory")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }                                        
                                        field = rowData.GetCell(4).ToString();
                                        if (field != "WarehouseName")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                    }
                                }
                                else
                                {
                                    rowData = sheet.GetRow(iRowIdx);

                                    cellData = rowData.GetCell(0);
                                    rowData = sheet.GetRow(iRowIdx);
                                    if (rowData != null)
                                    {
                                        CurrentStock currntstk = new CurrentStock();
                                        try
                                        {
                                            int cstid;
                                            cellData = rowData.GetCell(0);
                                            col0 = cellData == null ? "" : cellData.ToString();
                                            currntstk.ItemNumber = col0.Trim();
                                            logger.Info("ItemNumber :" + currntstk.ItemNumber);

                                            cellData = rowData.GetCell(1);
                                            col1 = cellData == null ? "" : cellData.ToString();
                                            if ((col1 == null) || (col1 == ""))
                                            {
                                                cstid = 0;
                                            }
                                            else
                                            {
                                                cstid = Convert.ToInt32(col1);

                                            }
                                            currntstk.StockId = cstid;
                                           
                                            cellData = rowData.GetCell(2);
                                            col2 = cellData == null ? "" : cellData.ToString();
                                            currntstk.ItemName = col2.Trim();

                                            cellData = rowData.GetCell(3);
                                            col3 = cellData == null ? "" : cellData.ToString();
                                            currntstk.CurrentInventory = Convert.ToInt32(col3);

                                            cellData = rowData.GetCell(4);
                                            col4 = cellData == null ? "" : cellData.ToString();
                                            currntstk.WarehouseName = col4.Trim();

                                            currentstkcollection.Add(currntstk);

                                        }
                                        catch (Exception ex)
                                        {
                                            msgitemname = ex.Message;
                                            logger.Error("Error adding customer in collection " + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace + currntstk.ItemName);
                                        }
                                    }
                                }
                            }
                            context.Addcurrentstock(currentstkcollection);
                            string m = "save collection";
                            logger.Info(m);
                        }
                        catch (Exception ex)
                        {
                             logger.Error("Error loading  for\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
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