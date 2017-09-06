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
    [RoutePrefix("api/Slider")]
    public class SliderController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();


        [Route("")]
        public IEnumerable<Slider> Get()
        {
            logger.Info("start Slider: ");
            List<Slider> Sliders = new List<Slider>();
            try
            {
                Sliders = context.GetAllSlider().ToList();
                logger.Info("End  Slider: ");
                return Sliders;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Slider " + ex.Message);
                logger.Info("End  Slider: ");
                return null;
            }
        }


        [Route("")]

        public Slider Get(int id)
        {
            logger.Info("start single Slider: ");
            Slider slider = new Slider();
            try
            {
                logger.Info("in Slider");

                slider = context.GetBySliderId(id);
                logger.Info("End Get Slider by  id: " + slider.Type);
                return slider;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get Slider by Slider id " + ex.Message);
                logger.Info("End  single Slider: ");
                return null;
            }
        }

        [ResponseType(typeof(Slider))]
        [Route("")]
        [AcceptVerbs("POST")]
        public Slider add(Slider slider)
        {
            logger.Info("Add Slider: ");
            try
            {
                if (slider == null)
                {
                    throw new ArgumentNullException("Slider");
                }
                context.AddSlider(slider);
                logger.Info("End  Add Slider: ");
                return slider;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add Slider " + ex.Message);

                return null;
            }
        }

        //[ResponseType(typeof(Slider))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public Slider Put(Slider slider)
        {
            try
            {
                return context.PutSlider(slider);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Put Slider " + ex.Message);
                return null;
            }
        }


        //[ResponseType(typeof(Groups))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public void Remove(int id)
        {
            logger.Info("DELETE Remove: ");
            try
            {
                context.DeleteSlider(id);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Remove Slider " + ex.Message);

            }
        }


    }
}



