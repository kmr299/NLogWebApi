using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;

namespace NLogWebApiConfig
{
    public static class ConfigUtils
    {
        public static void ConfigNLog(this HttpConfiguration config)
        {
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
            config.Services.Replace(typeof(ITraceWriter), new NLogTraceWriter());
        }
    }
}