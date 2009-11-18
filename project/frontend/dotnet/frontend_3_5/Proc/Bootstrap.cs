using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using log4net;
using log4net.Config;

using biztalk;
using frontend_3_5.BizTalk;
using frontend_3_5.Utils;
using frontend_3_5.Proc;

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

        public void configure()
        {
            // configure log
            XmlConfigurator.Configure(new System.IO.FileInfo("frontend_appender.xml"));
            log.Info("EmEx Frontend logger configured.");

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

            // [DIALOG] configure frontend - to - backend
            if ( ! Bootstrap.Instance().Settings.IsConfigured )
            {
                frmWizGeneral wizGeneral = new frmWizGeneral();
                if (DialogResult.Cancel == wizGeneral.ShowDialog())
                    throw new Exception("EmEx cannot start without configuration!");
            }

            // [DIALOG] configure Account
            if ( ! Bootstrap.Instance().Settings.IsAccountConfigured )
            {
                frmWizAccount wizAccount = new frmWizAccount();
                if (DialogResult.Cancel == wizAccount.ShowDialog())
                    throw new Exception("EmEx cannot start without configuring an account!");

                SplashManager.Instance().show();

                Bootstrap.Instance().Settings.Reload();
                Bootstrap.Instance().start();

                // register user
                Hashtable bizSettings = wizAccount.AccountInfo;
                Result res = Bootstrap.Instance().Talker.RegisterUser(bizSettings);
                ErrorHandler.checkBizResult(res);

                // rebuild mailbox
                res = Bootstrap.Instance().Talker.BuildMbox();
                ErrorHandler.checkBizResult(res);
            }
            else
            {
                SplashManager.Instance().show();
                Bootstrap.Instance().start();
            }

            // stop splash
            SplashManager.Instance().close();
        }

        public void start()
        {
            this.talker = new Talker(this.settings);
        }
    }
}
