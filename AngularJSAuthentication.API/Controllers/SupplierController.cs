using AngularJSAuthentication.Model;
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
    [RoutePrefix("api/Suppliers")]
    public class SupplierController : ApiController
    {
        iAuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        [Route("")]
        public IEnumerable<Supplier> Get()
        {
            logger.Info("start Supplier: ");
            List<Supplier> ass = new List<Supplier>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid=0;
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
                ass = context.AllSupplier().ToList() ;
                logger.Info("End  Supplier: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Supplier " + ex.Message);
                logger.Info("End  Supplier: ");
                return null;
            }
        }

        [Authorize]
        [Route("")]
        public Supplier Get(string id)
        {
            logger.Info("start Supplier: ");
            Supplier sup = new Supplier();
            AuthContext context = new AuthContext();
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
                int supplierid = Convert.ToInt32(id);
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);

                sup=context.Suppliers.Where(x => x.SupplierId == supplierid).SingleOrDefault();
                logger.Info("End  Supplier: ");
                return sup;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Supplier " + ex.Message);
                logger.Info("End  Supplier: ");
                return null;
            }
        }

        [ResponseType(typeof(Supplier))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Supplier add(Supplier supplier)
        {
            logger.Info("start supplier: ");
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                int compid = 0, userid=0;
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
                supplier.CompanyId = compid;
                if (supplier == null)
                {
                    throw new ArgumentNullException("supplier");
                }
                logger.Info("User ID : {0} , Company Id : {1}", compid, userid);
                context.AddSupplier(supplier);
                logger.Info("End  addsupplier: ");
                return supplier;
            }
            catch (Exception ex)
            {
                logger.Error("Error in addsupplier " + ex.Message);
                logger.Info("End  addsupplier: ");
                return null;
            }
        }
        
        [ResponseType(typeof(Supplier))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public Supplier Put(Supplier supplier)
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
                return  context.PutSupplier(supplier);
            }
            catch
            {
                return null;
            }
        }
        
        [ResponseType(typeof(Supplier))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("start deleteSupplier: ");
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
                context.DeleteSupplier(id);
                logger.Info("End  delete Supplier: ");
            }
            catch (Exception ex)
            {

                logger.Error("Error in deleteSupplier " + ex.Message);
            }
        }

        [Authorize]
        [Route("search")]
        public IEnumerable<Supplier> GetSupplier(string key)
        {
            logger.Info("start Supplier: ");
            List<Supplier> ass = new List<Supplier>();
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
                AuthContext context = new AuthContext();
                ass = context.Suppliers.Where(s=>s.SUPPLIERCODES.Contains(key) || s.Name.Contains(key) && s.Deleted == false).ToList();
                logger.Info("End  Supplier: ");
                return ass;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Supplier " + ex.Message);
                logger.Info("End  Supplier: ");
                return null;
            }
        }
    }
}