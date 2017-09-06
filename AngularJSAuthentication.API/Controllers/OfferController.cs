using GenricEcommers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using NLog;
using GenricEcommers;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/offer")]
    public class OfferController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        [Route("")]
        public IEnumerable<Offer> Get()
        {
            logger.Info("start Offer: ");
            List<Offer> OfferList = new List<Offer>();
            try
            {
               
                OfferList = context.GetAllOffer().ToList();
                logger.Info("End  Offer: ");
                return OfferList;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Offer " + ex.Message);
                logger.Info("End  Offer: ");
                return null;
            }
        }

        [Route("")]
        public Offer Get(int id)
        {
            logger.Info("start single User: ");
            Offer offer = new Offer();
            try
            {
                logger.Info("in user");

                offer = context.GetOfferbyId(id);
                logger.Info("End Get coupon by id: " + offer.OfferId);
                return offer;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get coupon by id " + ex.Message);
                logger.Info("End  single coupon: ");
                return null;
            }
        }

        [ResponseType(typeof(Offer))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Offer add(Offer offer)
        {
            logger.Info("Add offer: ");
            try
            {
                if (offer == null)
                {
                    throw new ArgumentNullException("offer");
                }

                context.AddOffer(offer);
                logger.Info("End  Add offer: ");
                 return offer;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add offer " + ex.Message);

                return null;
            }
        }

        //[ResponseType(typeof(Coupon))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public Offer Put(Offer Offer)
        {
            try
            {
                return context.PutOffer(Offer);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Put Coupon " + ex.Message);
                return null;
            }
        }


        //[ResponseType(typeof(Coupon))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("DELETE Remove: ");
            try
            {
                context.DeleteOffer(id);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Remove offer " + ex.Message);

            }
        }  
    }
}
