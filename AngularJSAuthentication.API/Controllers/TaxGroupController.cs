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
    [RoutePrefix("api/TaxGroup")]
    public class TaxGroupController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<TaxGroup> Get()
        {
            logger.Info("start TaxGroup: ");
            List<TaxGroup> ass = new List<TaxGroup>();
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
                ass = context.AllTaxGroup(compid).ToList();
                logger.Info("End  TaxGroup: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in TaxGroup " + ex.Message);
                logger.Info("End  TaxGroup: ");
                return null;
            }
        }


        [ResponseType(typeof(TaxGroup))]
        [Route("")]
        [AcceptVerbs("POST")]
        public TaxGroup add(TaxGroup item)
        {
            logger.Info("start add TaxGroup: ");
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
                item.CompanyId = compid;
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddTaxGroup(item);
                logger.Info("End  Add TaxGroup: ");
                return item;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add TaxGroup " + ex.Message);
                logger.Info("End  Add TaxGroup: ");
                return null;
            }
        }

        [ResponseType(typeof(TaxGroup))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public TaxGroup Put(TaxGroup item)
        {
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
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                return context.PutTaxGroup(item);
            }
            catch
            {
                return null;
            }
        }


        [ResponseType(typeof(TaxGroup))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start delete TaxGroup: ");
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
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.DeleteTaxGroup(id);
                logger.Info("End  delete TaxGroup: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in delete TaxGroup " + ex.Message);


            }
        }
    }
}



