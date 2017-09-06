using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/AllInvoice")]
    public class AllInvoiceController : ApiController
    {
        iAuthContext context = new AuthContext();

        //[Authorize]
        //[Route("")]
        //public IEnumerable<AllInvoice> Get()
        //{
        //    //return Helper.CreateProjects().AsEnumerable();

        //    return context.AllInvoice;
        //}



        [ResponseType(typeof(AllInvoice))]
        [Route("")]
        [AcceptVerbs("POST")]
        public AllInvoice Post(AllInvoice item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            context.AddInvoice(item);

            return item;
        }



    }

  
}



