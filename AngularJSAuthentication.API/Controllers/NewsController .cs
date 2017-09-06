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
    [RoutePrefix("api/NewsApi")]
    public class NewsController : ApiController
    {
        AuthContext context = new AuthContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //[Authorize]
        [Route("")]
        public IEnumerable<News> Get()
        {
            logger.Info("start News: ");
            List<News> List = new List<News>();
            try
            {
                List = context.AllNews().ToList();
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
        public News Get(string a, string b)
        {
            logger.Info("start News: ");
            News List = new News();
            try
            {
                List = context.AllNews().Where(r => r.IsAvailable == true).SingleOrDefault();
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
        public News Get(int id)
        {
            logger.Info("start single News: ");
            News News = new News();
            try
            {
                logger.Info("in News");

                News = context.GetNewsId(id);
                logger.Info("End Get News by item id: ");// + News.NewsName);
                return News;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get News by item id " + ex.Message);
                logger.Info("End  single News: ");
                return null;
            }
        }

        [ResponseType(typeof(News))]
        [Route("")]
        [AcceptVerbs("POST")]
        public News add(News news)
        {
            logger.Info("Add News: ");
            try
            {
                if (news == null)
                {
                    throw new ArgumentNullException("News");
                }

                context.AddNews(news);
                logger.Info("End  Add News: ");
                return news;
            }
            catch (Exception ex)
            {
                logger.Error("Error in Add News " + ex.Message);

                return null;
            }

        }

        //[ResponseType(typeof(News))]
        [Route("")]
        [AcceptVerbs("PUT")]
        public News Put(News News)
        {
            try
            {
                return context.PutNews(News);
            }
            catch (Exception ex)
            {
                logger.Error("Error in Put News " + ex.Message);
                return null;
            }
        }


        ////[ResponseType(typeof(News))]
        [Route("")]
        [AcceptVerbs("Delete")]
        public string Remove(int id)
        {
            logger.Info("DELETE Remove: ");
            try
            {
                if (context.DeleteNews(id))
                {
                    return "success";
                }
                else
                {
                    return "error";
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
