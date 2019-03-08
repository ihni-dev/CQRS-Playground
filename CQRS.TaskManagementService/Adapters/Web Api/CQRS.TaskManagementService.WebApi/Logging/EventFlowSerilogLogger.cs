using System;
using EventFlow.Logs;
using Serilog;
using Serilog.Events;

namespace CQRS.TaskManagementService.WebApi.Logging
{
    public class EventFlowSerilogLogger : EventFlow.Logs.Log
    {
        private readonly ILogger _logger;
        
        public EventFlowSerilogLogger(ILogger logger)
        {
            _logger = logger;
            IsVerboseEnabled = true;
            IsInformationEnabled = true;
            IsDebugEnabled = true;
        }

        protected override bool IsVerboseEnabled { get; }
        protected override bool IsInformationEnabled { get; }
        protected override bool IsDebugEnabled { get; }
        
        public override void Write(LogLevel logLevel, string format, params object[] args)
        {
            _logger.Write(ParseLogLevel(logLevel), format, args);
        }

        public override void Write(LogLevel logLevel, Exception exception, string format, params object[] args)
        {
            _logger.Write(ParseLogLevel(logLevel), exception, format, args);
        }

        private LogEventLevel ParseLogLevel(LogLevel logLevel)
        {
            try
            {
                return Enum.Parse<LogEventLevel>(Enum.GetName(typeof(LogLevel), logLevel));
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "Serilog log levels do not match EventFlow log levels.");
                return LogEventLevel.Fatal;
            }
        }
    }
}