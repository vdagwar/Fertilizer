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
    [RoutePrefix("api/demand")]
    public class DemandController : ApiController
    {
        AuthContext context = new AuthContext();

        [Route("")]
        public IList<DemandDetails> Get()
        {
            try
            {
                var demand = (from a in context.dbDemandDetails  select a).ToList();
                return demand;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ResponseType(typeof(DemandDetails))]
        [Route("")]
        [AcceptVerbs("POST")]
        public DemandMaster Post(List<DemandDetails> demand)
        {
            try
            {
                DemandMaster dm = new DemandMaster();
                dm.demand = demand;
                context.Adddemand(dm);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
