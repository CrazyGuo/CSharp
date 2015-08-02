using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IBatisQueryGenerator
{
    public class LoggingHelper
    {
        private const String _errLogFilePath = @"C:\log.txt";

        public static void Writelog(string file, String message)
        {
            StreamWriter sw = new StreamWriter(file, false);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }
    }
}
