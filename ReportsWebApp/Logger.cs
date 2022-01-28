using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace ReportsWebApp
{
    public static class Logger
    {
        private static ILog log = LogManager.GetLogger("LOGGER");

        public static ILog Log
        {
            get { return log; }
        }
    }
}