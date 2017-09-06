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

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/WarehouseCategoryUpload")]
    public class WarehouseCategoryUploadController : ApiController
    {
        [HttpPost]
        public void UploadFile()
        {
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

        //                public static bool ScanForText(Worksheet worksheet, object search,
        //                       out int columnIndex, out int rowIndex)
        //{
        //    string searchText = search.ToString().ToLower();
        //    for (int i = 1; i <= 100; i++)
        //    {
        //        for (rowIndex = 1, columnIndex = i; rowIndex <= i; rowIndex++, columnIndex--)
        //            if (worksheet.Cells[rowIndex, columnIndex].Value != null &&
        //                worksheet.Cells[rowIndex, columnIndex].Value.ToString().
        //                  ToLower() == searchText)
        //                return true;
        //    }
        //    columnIndex = -1; rowIndex = -1;
        //    return false;
        //}
        //public WorksheetReader(Excel.Worksheet worksheet)
        //{
        //    _worksheet = worksheet;
        //    _headerColumns = valueValidators.Select
        //          (i => new HeaderTextColumn(i.HeaderText, i.Required)).ToArray();
        //    _valueValidators = valueValidators;
        //}
        //private bool ScanForHeaders(int rowIndex)
        //{
        //    int counter = 0;
        //    for (var columnIndex = 1; columnIndex < 100; columnIndex++)
        //    {
        //        if (_worksheet.Cells[rowIndex, columnIndex].Value != null)
        //        {
        //            Excel.Range cell = _worksheet.Cells[rowIndex, columnIndex];
        //            string cellText = cell.Value.ToString().ToLower();
        //            var headerColumn = _headerColumns.FirstOrDefault
        //                      (i => i.CellText.ToLower() == cellText);

        //            if (headerColumn != null)
        //            {
        //                headerColumn.SetColumn(cell);
        //                //No need to proceed further if found all titles
        //                if (_headerColumns.Count() == ++counter) break;
        //            }
        //        }
        //    }
        //    return _headerColumns.All(i => !i.Required || i.ColumnIndex > 0);
        //}
        //private string HeadersErrorMessage()
        //{
        //    IEnumerable<string> names = _headerColumns.Where(
        //          i => i.Required && i.ColumnIndex == 0).Select(i => i.CellText);
        //    return string.Format(
        //      "The following required headers are missing from the header row:{0} {1}.",
        //      Environment.NewLine, string.Join(", ", names));
        //}
                        try
                        {
                            for (int iRowIdx = 1; iRowIdx <= sheet.LastRowNum; iRowIdx++)  //  iRowIdx = 0; HeaderRow
                            {
                                rowData = sheet.GetRow(iRowIdx);

                                if (rowData != null)
                                {
                                    WarehouseCategory whcategory   = new WarehouseCategory();

                                    whcategory.CompanyId = 1; // compid;
                                    cellData = rowData.GetCell(12);

                                   
                                    try
                                    {
                                        cellData = rowData.GetCell(0);
                                        if (cellData != null)
                                        {
                                            whcategory.WhCategoryid = int.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(1);
                                        if (cellData != null)
                                        {
                                            whcategory.Warehouseid = int.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }

                                    try
                                    {
                                        cellData = rowData.GetCell(2);
                                        if (cellData != null)
                                        {
                                            whcategory.WarehouseName = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(3);
                                        if (cellData != null)
                                        {
                                            whcategory.Stateid = int.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(4);
                                        if (cellData != null)
                                        {
                                            whcategory.State = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }

                                    try
                                    {
                                        cellData = rowData.GetCell(5);
                                        if (cellData != null)
                                        {
                                            whcategory.Cityid = int.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }

                                    try
                                    {
                                        cellData = rowData.GetCell(6);
                                        if (cellData != null)
                                        {
                                            whcategory.City = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(7);
                                        if (cellData != null)
                                        {
                                            whcategory.Categoryid = int.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(8);
                                        if (cellData != null)
                                        {
                                            whcategory.CategoryName = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(9);
                                        whcategory.IsVisible = bool.Parse(cellData.ToString());
                                                                              
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(10);
                                        if (cellData != null)
                                        {
                                            whcategory.SortOrder = int.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(11);
                                        if (cellData != null)
                                        {
                                            whcategory.Discription = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(13);
                                        if (cellData != null)
                                        {
                                            whcategory.CreatedDate = DateTime.Parse(cellData.ToString()) ;
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(14);
                                        if (cellData != null)
                                        {
                                            whcategory.UpdatedDate = DateTime.Parse(cellData.ToString());
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(15);
                                        if (cellData != null)
                                        {
                                            whcategory.CreatedBy = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }
                                    try
                                    {
                                        cellData = rowData.GetCell(16);
                                        if (cellData != null)
                                        {
                                            whcategory.UpdateBy = cellData == null ? "" : cellData.ToString();
                                        }
                                    }
                                    catch (Exception ex) { }


                                    context.Addwarehousecatxl(whcategory);

                                   
                                }
                            }
                            // _UpdateStatus = true;
                        }
                        catch (Exception ex)
                        {
                            //  logger.Error("Error loading URL for " + URL + "\n\n" + ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);

                        }
                    }
                 
                    var FileUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
          
                    httpPostedFile.SaveAs(FileUrl);
                }
            }
        }
    }

}
