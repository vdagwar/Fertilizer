using AngularJSAuthentication.API.Models;
using AngularJSAuthentication.API.Results;
using AngularJSAuthentication.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using NLog;
using System.Text;
using System.IO;

namespace AngularJSAuthentication.API.Controllers
{
    public class Sms
    {
        public bool sendOtp(string mn, string msg)
        {
            string authKey = "100498AhbWDYbtJT56af33e3";

            //Multiple mobiles numbers separated by comma
            string mobileNumber = mn; //"9999999";
                                      //Sender ID,While using route4 sender id should be 6 characters long.
                                      //string senderId = "Moreye";
            string senderId = "SHOPKI";
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode(msg);

            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", authKey);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", senderId);
            sbPostData.AppendFormat("&route={0}", "4");
            sbPostData.AppendFormat("&country={0}", "91");
            try
            {
                //Call Send SMS API
                //string sendSMSUri = "http://sms.o2technology.in/api/sendhttp.php";
                string sendSMSUri = "http://bulksms.newrise.in/api/sendhttp.php";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            return true;
        }
    }
}