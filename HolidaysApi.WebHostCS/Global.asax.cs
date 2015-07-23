using System.Web.Http;
using Aklefdal.Holidays.HttpApi;

namespace HolidaysApi.WebHostCS
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;
            Infrastructure.Configure(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new SetCacheHeadersHandler());
        }
    }
}
