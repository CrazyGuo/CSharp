using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Reflection;

namespace Study.Webs.EayUI
{
    public class LogOuts
    {
        public static void Debug(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("log4a");
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public static void Error(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("log4net");
            log4net.ILog log = log4net.LogManager.GetLogger("log4a");
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Fatal(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("log4a");
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }

        }

        public static void Info(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("log4net");
            log4net.ILog log = log4net.LogManager.GetLogger("log4a");
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }

        }

        public static void Warn(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("log4a");
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
    }
}