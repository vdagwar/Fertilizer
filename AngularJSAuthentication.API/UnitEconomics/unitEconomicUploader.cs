
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
    [RoutePrefix("api/UnitEconomicupload")]
    public class unitEconomicUploadController : ApiController
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        AuthContext context = new AuthContext();

        string msg , msgitemname;
        string strJSON = null;
        string col0, col1, col2, col3, col4, col5, col6, col7, col8, col9;
        [HttpPost]
        public string UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                logger.Info("start current stock Upload Exel File: ");
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
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        BinaryFormatter binForm = new BinaryFormatter();
                        memStream.Write(buffer, 0, buffer.Length);
                        memStream.Seek(0, SeekOrigin.Begin);
                        hssfwb = new XSSFWorkbook(memStream);
                        string sSheetName = hssfwb.GetSheetName(0);
                        ISheet sheet = hssfwb.GetSheet(sSheetName);
                        IRow rowData;
                        ICell cellData = null;
                        try
                        {
                            List<UnitEconomic> UnitEconomicCollection = new List<UnitEconomic>();
                            for (int iRowIdx = 0; iRowIdx <= sheet.LastRowNum; iRowIdx++)  //  iRowIdx = 0; HeaderRow
                            {
                                if (iRowIdx == 0)
                                {
                                    rowData = sheet.GetRow(iRowIdx);
                                    if (rowData != null)
                                    {
                                        string field = string.Empty;                                       
                                        field = rowData.GetCell(1).ToString();
                                        if (field != "Label1")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(2).ToString();
                                        if (field != "Label2")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(3).ToString();
                                        if (field != "Label3")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }                                        
                                        field = rowData.GetCell(4).ToString();
                                        if (field != "Warehouseid")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(5).ToString();
                                        if (field != "Amount")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(6).ToString();
                                        if (field != "CreatedDate")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(7).ToString();
                                        if (field != "Discription")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(8).ToString();
                                        if (field != "IsActive")
                                        {
                                            JavaScriptSerializer objJSSerializer = new JavaScriptSerializer(); strJSON = objJSSerializer.Serialize("Header Name  " + field + " does not exist..try again");
                                            return strJSON;
                                        }
                                        field = rowData.GetCell(9).ToString();
                                        if (field != "Deleted")
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
                                        UnitEconomic ue = new UnitEconomic();
                                        try
                                        {
                                            cellData = rowData.GetCell(0);
                                            col0 = cellData == null ? "" : cellData.ToString();

                                            cellData = rowData.GetCell(1);
                                            col1 = cellData == null ? "" : cellData.ToString();
                                            ue.Label1 = col1.Trim();
                                                                                       
                                            cellData = rowData.GetCell(2);
                                            col2 = cellData == null ? "" : cellData.ToString();
                                            ue.Label2 = col2.Trim();

                                            cellData = rowData.GetCell(3);
                                            col3 = cellData == null ? "" : cellData.ToString();
                                            ue.Label3 = col3.Trim();

                                            cellData = rowData.GetCell(4);
                                            col4 = cellData == null ? "" : cellData.ToString();
                                            try
                                            {
                                                ue.Warehouseid = Convert.ToInt32(col4);
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.Error(ex.Message + " in row :" + col0.Trim());
                                                break;
                                            }
                                            cellData = rowData.GetCell(5);
                                            col5 = cellData == null ? "" : cellData.ToString();
                                            try
                                            {
                                                ue.Amount = Convert.ToDouble(col5);
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.Error(ex.Message + " in row :" + col0.Trim());
                                                break;
                                            }

                                            cellData = rowData.GetCell(6);
                                            col6 = cellData == null ? "" : cellData.ToString();
                                            try
                                            {
                                                if (col6.Trim() == null || col6.Trim() == "")
                                                    ue.CreatedDate = indianTime;
                                                else
                                                    ue.CreatedDate = Convert.ToDateTime(col6);
                                            }
                                            catch (Exception ex)
                                            {
                                                logger.Error(ex.Message + " in row :" + col0.Trim());
                                                break;
                                            }

                                            cellData = rowData.GetCell(7);
                                            col7 = cellData == null ? "" : cellData.ToString();
                                            ue.Discription = col7.Trim();

                                            cellData = rowData.GetCell(8);
                                            col8 = cellData == null ? "" : cellData.ToString();
                                            if (col8.Trim().ToLower() == "false")
                                            {
                                                ue.IsActive = false;
                                            }
                                            else 
                                            {
                                                ue.IsActive = true;
                                            }

                                            cellData = rowData.GetCell(9);
                                            col9 = cellData == null ? "" : cellData.ToString();
                                            if (col9.Trim().ToLower() == "true")
                                            {
                                                ue.Deleted = true;
                                            }
                                            else
                                            {
                                                ue.Deleted = false;
                                            }

                                            UnitEconomicCollection.Add(ue);

                                        }
                                        catch (Exception ex)
                                        {
                                            msgitemname = ex.Message;
                                            logger.Error("Error adding customer in collection " + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
                                        }
                                    }
                                }
                            }
                            try
                            {
                                foreach (var o in UnitEconomicCollection)
                                {
                                    List<UnitEconomic> cst = context.UnitEconomicDb.Where(c => c.CreatedDate == o.CreatedDate && c.Label1.Trim().ToLower() == o.Label1.Trim().ToLower() && c.Label2.Trim().ToLower() == o.Label2.Trim().ToLower() &&
                                                                                        c.Label3.Trim().ToLower() == o.Label3.Trim().ToLower() && c.Warehouseid == o.Warehouseid).ToList();
                                    if (cst.Count == 0)
                                    {
                                        try
                                        {
                                            o.IsActive = true;
                                            context.UnitEconomicDb.Add(o);
                                            int id = context.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Info("error in adding new Unit Economic of date" + o.CreatedDate + ex);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            foreach (var ue in cst) { 
                                            var ueData = context.UnitEconomicDb.Where(x => x.unitId == ue.unitId).SingleOrDefault();
                                            if (ueData != null)
                                            {        
                                                    ueData.Amount = o.Amount;
                                                    ueData.Discription = o.Discription;
                                                    ueData.IsActive = o.IsActive;
                                                    ueData.Deleted = o.Deleted;
                                                    context.UnitEconomicDb.Attach(ueData);
                                                    context.Entry(ueData).State = System.Data.Entity.EntityState.Modified;
                                                    int id = context.SaveChanges();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            logger.Info("error in adding new Unit Economic of date" + o.CreatedDate + ex);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                logger.Info("error in adding currentstock collection" + ex);

                            }
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