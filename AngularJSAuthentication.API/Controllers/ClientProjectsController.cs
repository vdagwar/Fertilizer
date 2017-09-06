﻿using AngularJSAuthentication.Model;
using AngularJSAuthentication.Model.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using NLog;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/ClientProject")]
    public class ClientProjectsController : ApiController
    {

        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // [Authorize]
        [Route("")]
        public IEnumerable<Project> Get()
        {
            logger.Info("strart Client Project: ");
            try
            {
                //return Helper.CreateProjects().AsEnumerable();
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
                logger.Info("End  Client Project: ");
                return context.AllActiveProjectsbyCompanyId(compid);

            }
            catch (Exception ex)
            {
                logger.Error("Error in Client Project " + ex.Message);
              
                return null;
            }
        }
    }
}
