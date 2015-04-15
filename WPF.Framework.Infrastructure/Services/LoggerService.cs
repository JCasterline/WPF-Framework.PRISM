using System;
using Microsoft.Practices.Prism.Logging;
using NLog;

namespace WPF.Framework.Infrastructure.Services
{
    public class LoggerService : ILoggerFacade
    {
        private readonly Logger _logger;

        public LoggerService(Type type)
        {
            _logger = LogManager.GetLogger(type.FullName);
        }

        public LoggerService(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    _logger.Debug(message);
                    break;
                case Category.Info:
                    _logger.Info(message);
                    break;
                case Category.Warn:
                    _logger.Warn(message);
                    break;
                case Category.Exception:
                    _logger.Error(message);
                    break;
                default:
                    _logger.Info(message);
                    break;
            }
        }
    }
}
