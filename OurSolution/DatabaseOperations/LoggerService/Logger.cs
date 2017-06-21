using System;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LoggerService
{
    public enum LogLevel
    {
        Debug = 1,
        Error,
        Fatal,
        Info,
        Warning
    }

    public static class Logger
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Logger));

        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public static void WriteLog(LogLevel level, string message, Exception ex = null)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    if (ex == null)
                        logger.Debug(message);
                    else
                        logger.Debug(message, ex);
                    break;

                case LogLevel.Error:
                    if (ex == null)
                        logger.Error(message);
                    else
                        logger.Error(message, ex);
                    break;

                case LogLevel.Fatal:
                    if (ex == null)
                        logger.Fatal(message);
                    else
                        logger.Fatal(message, ex);
                    break;

                case LogLevel.Info:
                    if (ex == null)
                        logger.Info(message);
                    else
                        logger.Info(message, ex);
                    break;

                case LogLevel.Warning:
                    if (ex == null)
                        logger.Warn(message);
                    else
                        logger.Warn(message, ex);
                    break;
            }
        }
    }
}
