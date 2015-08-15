using log4net;

namespace Log
{
    public class LogOuts
    {
        public static void Debug(string message)
        {
            ILog log = LogManager.GetLogger("log4Debug");
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public static void Error(string message)
        {
            ILog log = LogManager.GetLogger("log4a");
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Fatal(string message)
        {
            ILog log = LogManager.GetLogger("log4a");
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }

        }

        public static void Info(string message)
        {
            ILog log = LogManager.GetLogger("log4a");
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public static void Warn(string message)
        {
            ILog log = LogManager.GetLogger("log4a");
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
    }
}
