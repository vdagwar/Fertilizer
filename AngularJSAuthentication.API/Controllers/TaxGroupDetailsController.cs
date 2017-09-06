﻿using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/TaxGroupDetails")]
    public class TaxGroupDetailsController : ApiController
    {
        iAuthContext context = new AuthContext();
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Authorize]
        [Route("")]
        public IEnumerable<TaxGroupDetails> Gettaxgrpdetails(string id)
        {
            logger.Info("start : ");
            List<TaxGroupDetails> ass = new List<TaxGroupDetails>();
            try
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

                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                int idd = Int32.Parse(id);
                ass = context.AlltaxgroupDetails(idd).ToList();
                logger.Info("End  : ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in OrderDetails " + ex.Message);
                logger.Info("End  OrderDetails: ");
                return null;
            }
        }

        [ResponseType(typeof(TaxGroupDetails))]
        [Route("")]
        [AcceptVerbs("POST")]
        public IEnumerable<TaxGroupDetails> Post([FromBody]IEnumerable<TaxGroupDetails> pList)
        {
            //int j = pList.Count();
            //j = j - 1;
            //var id = pList.Last();
            //pList = pList.Take(pList.Count() - 1);
            foreach (TaxGroupDetails model in pList)
            {
                
                TaxGroupDetails e = new TaxGroupDetails();
                
                e.TaxgrpDetailID = model.TaxgrpDetailID;
                e.GruopID = model.GruopID;
                e.TaxID = model.TaxID;
                e.CreatedDate = indianTime;
                e.UpdatedDate = indianTime;
               // e.CompanyId = 2;
                context.AddTaxGRPDetail(e);


            }
            return pList;
        }



    }

  
}



