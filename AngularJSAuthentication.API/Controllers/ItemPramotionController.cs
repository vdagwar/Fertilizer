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
    //AllItemPramotion

    [RoutePrefix("api/pramotion")]
    public class ItemPramotionController : ApiController
    {
        AuthContext context = new AuthContext();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        [Route("")]
        public IEnumerable<ItemPramotions> Get()
        {
           
            List<ItemPramotions> ass = new List<ItemPramotions>();
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

                
                ass = context.AllItemPramotion().ToList();
              
                return ass;
            }
            catch (Exception ex)
            {
                //logger.Error("Error in Item Master " + ex.Message);
                //logger.Info("End  Item Master: ");
                return null;
            }
        }

        [Route("")]
        public IEnumerable<ItemPramotions> Get(int id)
        {
            List<ItemPramotions> itempramotion = new List<ItemPramotions>();
            //var warehouse = Warehouses.Where(c => c.Mobile == mobile).AsEnumerable();
            itempramotion = (from ip in context.itempramotions where ip.Warehouseid == id && (ip.StartDate < indianTime && ip.EndDate > indianTime) select ip).ToList();
            return itempramotion;

        }

  
        [ResponseType(typeof(ItemPramotions))]
        [Route("")]
        [AcceptVerbs("POST")]
        public ItemMaster Post(ItemMaster item)
        {

            try
            {
                context.AddItemPramotion(item);
                context.AddPramotion(item);
                return item;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
