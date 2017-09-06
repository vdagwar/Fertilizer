using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using NLog;
using System.Security.Claims;
namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/IRUpload")]    
    public class aIRUploadpostController : ApiController
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        [HttpPost]
        [Route("")]
        public void UploadFile()
        {
            logger.Info("start logo upload");
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {                    
                    var httpPostedFile = HttpContext.Current.Request.Files["file"];
                    if (httpPostedFile != null)
                    {
                        var LogoUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/IRImages"), httpPostedFile.FileName);
                        httpPostedFile.SaveAs(LogoUrl);
                        logger.Info("End image upload: success");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in  logo upload " + ex.Message);
            }
        }
    }
}