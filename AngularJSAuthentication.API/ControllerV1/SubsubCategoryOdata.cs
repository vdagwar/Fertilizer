using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;

namespace AngularJSAuthentication.API.ControllerV1
{
    public class SubsubCategoryOdataController : ODataController
    {
        AuthContext authContext = new AuthContext();
        [EnableQuery]
        public IQueryable<SubsubCategory> Get()
        {
            return authContext.SubsubCategorys;
        }
    }
}
