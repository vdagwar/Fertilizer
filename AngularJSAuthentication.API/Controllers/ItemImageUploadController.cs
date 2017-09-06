using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/itemimageupload")]    
    public class ItemImageUploadController : ApiController
    {    
        
        [HttpPost]
        public void UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["file"];

                if (httpPostedFile != null)
                {
                    // Validate the uploaded image(optional)

                    // Get the complete file path
                    var FileUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/images/itemimages"), httpPostedFile.FileName);
                    //var ImageUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
                    //string physicalPath = ("~/images/" + ImageName);
                    //httpPostedFile.SaveAs(physicalPath);
                    //customer newRecord = new customer();
                    //newRecord.username = customer.username;
                    ////.......saving picture url......
                    //newRecord.picture = physicalPath;

                    //// Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(FileUrl);
                }
            }
        }       
    }
}
