using System;
using System.Net.Http;
using System.Web.Http.Tracing;
using Newtonsoft.Json;
using NLog;

namespace NLogWebApiConfig
{
    public class NLogTraceWriter : ITraceWriter
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private LogLevel ToNLogLevel(TraceLevel level)
        {
            switch (level)
            {
                case TraceLevel.Off:
                    return LogLevel.Off;
                case TraceLevel.Debug:
                    return LogLevel.Debug;
                case TraceLevel.Info:
                    return LogLevel.Info;
                case TraceLevel.Warn:
                    return LogLevel.Warn;
                case TraceLevel.Error:
                    return LogLevel.Error;
                case TraceLevel.Fatal:
                    return LogLevel.Fatal;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var traceRecord = new TraceRecord(request, category, level);

            var message =
                   $"| Category: {traceRecord.Category} "+
                   $"\n| Kind: {traceRecord.Kind} "+
                   $"\n| Level: {traceRecord.Level} "+
                   $"\n| Message: {traceRecord.Message} "+
                   $"\n| Operation: {traceRecord.Operation} "+
                   $"\n| Operator: {traceRecord.Operator} "+
                   $"\n| Properties: {ToJson(traceRecord.Properties)} "+
                   $"\n| Request: {traceRecord.Request.RequestToString()} "+
                   $"\n| RequestId: {traceRecord.RequestId} "+
                   $"\n| Status: {traceRecord.Status} "+
                   $"\n| Timestamp: {traceRecord.Timestamp}";

            if(traceRecord.Exception != null)
            _logger.Log(ToNLogLevel(level), traceRecord.Exception, message);
            else
                _logger.Log(ToNLogLevel(level), message);
        }

        public string ToJson(object obj)
        {
            var jsonConfig = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            return JsonConvert.SerializeObject(obj, jsonConfig);
        }
    }
}
