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
using GenricEcommers.Models;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/BanksettleApi")]
    public class BankSettleController : ApiController
    {
        iAuthContext context = new AuthContext();
        AuthContext db = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        

        //[Route(" ")]
        //[AcceptVerbs("PUT")]
        //public CurrencyBankSettle Put(CurrencyBankSettle obj)
        //{
        //    return null;
        //    //try
        //    //{
        //    //    return db.BankCurrencyPut(obj);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    logger.Error("Error in Put News " + ex.Message);
        //    //    return null;
        //    //}
        //}

       

    }
}




