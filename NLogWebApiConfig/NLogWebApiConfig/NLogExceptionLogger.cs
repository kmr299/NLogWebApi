using System.Web.Http.ExceptionHandling;
using NLog;

namespace NLogWebApiConfig
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Fatal(context.Exception, context.Request.RequestToString());
        }
    }
}