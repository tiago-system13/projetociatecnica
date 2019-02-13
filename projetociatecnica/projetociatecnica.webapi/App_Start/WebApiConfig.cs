using System.Web.Http;

namespace projetociatecnica.webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            config.MapHttpAttributeRoutes();
        }
    }
}