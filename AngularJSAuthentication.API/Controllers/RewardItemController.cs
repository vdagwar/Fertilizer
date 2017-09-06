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
using System.Data.Entity;

namespace AngularJSAuthentication.API.Controllers
{
    [RoutePrefix("api/RewardItem")]
    public class RewardItemController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Authorize]
        [Route("GetAll")]
        public IEnumerable<RewardItems> Get()
        {
            logger.Info("start News: ");
            List<RewardItems> List = new List<RewardItems>();
            try
            {
                List = context.RewardItemsDb.Where(r => r.IsDeleted == false).ToList();
                logger.Info("End  News: ");
                return List;
            }
            catch (Exception ex)
            {
                logger.Error("Error in News " + ex.Message);
                logger.Info("End  News: ");
                return null;
            }
        }

        [Route("")]
        public IEnumerable<RewardItems> GetMobile()
        {
            logger.Info("start News: ");
            List<RewardItems> List = new List<RewardItems>();
            try
            {
                List = context.RewardItemsDb.Where(r =>r.IsActive == true && r.IsDeleted == false).OrderByDescending(o=> o.rPoint).ToList();
                logger.Info("End  News: ");
                return List;
            }
            catch (Exception ex)
            {
                logger.Error("Error in News " + ex.Message);
                logger.Info("End  News: ");
                return null;
            }
        }        
        [Route("")]
        public RewardItems Get(int id)
        {
            logger.Info("start single News: ");
            RewardItems News = new RewardItems();
            try
            {
                logger.Info("in News");

                News = context.RewardItemsDb.Where(x=> x.rItemId == id && x.IsDeleted == false).SingleOrDefault();
                logger.Info("End Get News by item id: ");
                return News;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get News by item id " + ex.Message);
                logger.Info("End  single News: ");
                return null;
            }
        }
        [Route("")]
        [AcceptVerbs("POST")]
        public RewardItems add(RewardItems news)
        {
            logger.Info("Add News: ");
            try
            {
                news.CreateDate = indianTime;
                news.IsActive = true;
                news.IsDeleted = false;
                context.RewardItemsDb.Add(news);
                context.SaveChanges();
                return news;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add News " + ex.Message);
                return null;
            }
        }
        [Route("")]
        [AcceptVerbs("PUT")]
        public RewardItems Put(RewardItems News)
        {
            try
            {
                RewardItems comp = context.RewardItemsDb.Where(x => x.rItemId == News.rItemId).FirstOrDefault();
                if (comp != null)
                {
                    comp.rName = News.rName;
                    comp.rPoint = News.rPoint;
                    comp.rItem = News.rItem;
                    comp.Description = News.Description;
                    comp.ImageUrl = News.ImageUrl;
                    comp.IsDeleted = News.IsDeleted;
                    comp.IsActive = News.IsActive;
                    context.RewardItemsDb.Attach(comp);
                    context.Entry(comp).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return comp;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Put News " + ex.Message);
                return null;
            }
        }
        [Route("")]
        [AcceptVerbs("Delete")]
        public string Remove(int id)
        {
            logger.Info("DELETE Remove: ");
            try
            {
                RewardItems comp = context.RewardItemsDb.Where(x => x.rItemId == id).SingleOrDefault();
                if (comp != null)
                {
                    context.RewardItemsDb.Remove(comp);
                    context.SaveChanges();
                    return "success";
                }
                else
                {
                    return "Record doen't exist";
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in Remove News " + ex.Message);
                return "error";
            }
        }
    }
}