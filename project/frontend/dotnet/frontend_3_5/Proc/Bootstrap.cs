using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using log4net;
using log4net.Config;

using frontend_3_5.BizTalk;

namespace frontend_3_5.Proc
{
    class Bootstrap
    {
        private const string EMEX_OPTIONS = "emex-options.xml";
        private const string EMEX_OPTIONS_TEMPLATE = EMEX_OPTIONS + ".template";

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private static Bootstrap _instance = null;
        private Settings settings   = null;
        private Talker talker       = null;

        public static Bootstrap Instance()
        {
            if (_instance == null)
                _instance = new Bootstrap();

            return _instance;
        }

        public Settings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public Talker Talker
        {
            get
            {
                return this.talker;
            }
        }

        public void configureLog()
        {
            // configure log
            XmlConfigurator.Configure(new System.IO.FileInfo("frontend_appender.xml"));
            log.Info("EmEx Frontend logger configured.");
        }

        public void configure()
        {
            // try to open settings file
            if (File.Exists( EMEX_OPTIONS ))
            {
                this.settings = new Settings( EMEX_OPTIONS );
            }
            else
            {
                if (File.Exists(EMEX_OPTIONS_TEMPLATE))
                    File.Copy(EMEX_OPTIONS_TEMPLATE, EMEX_OPTIONS);
                this.settings = new Settings(EMEX_OPTIONS);
            }
        }

        public void start()
        {
            this.talker = new Talker(this.settings);
        }
    }
}
