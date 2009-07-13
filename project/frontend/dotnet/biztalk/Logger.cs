using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

namespace biztalk
{
    class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void init()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("biztalk_appender.xml"));
            log.Info("EmEx Biztalk logger configured.");
        }
    }
}
