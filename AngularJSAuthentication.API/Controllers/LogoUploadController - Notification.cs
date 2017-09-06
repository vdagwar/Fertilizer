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
using NLog;
using System.Security.Claims;



namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/logoUploadNotification")]    
    public class LogoUploadNotificationController : ApiController
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        public void UploadFile()
        {
            logger.Info("start logo upload");
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

                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files["file"];

                    if (httpPostedFile != null)
                    {
                        // Validate the uploaded image(optional)

                        // Get the complete file path
                        var LogoUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/notificationimages"), httpPostedFile.FileName);
                        //var ImageUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);
                        //string physicalPath = ("~/images/" + ImageName);
                        //httpPostedFile.SaveAs(physicalPath);
                        //customer newRecord = new customer();
                        //newRecord.username = customer.username;
                        ////.......saving picture url......
                        //newRecord.picture = physicalPath;

                        //// Save the uploaded file to "UploadedFiles" folder
                        logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

                        httpPostedFile.SaveAs(LogoUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in  logo upload " + ex.Message);
                logger.Info("End   logo upload: ");
            }
        }
    }
}
