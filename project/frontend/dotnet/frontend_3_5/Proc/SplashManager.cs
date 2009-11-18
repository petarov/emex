using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace frontend_3_5.Proc
{
    class SplashManager
    {
        private static SplashManager _instance = null;
        private frmSplash splashForm = null;
        private Thread thread = null;

        private SplashManager()
        {
        }

        public static SplashManager Instance()
        {
            if (_instance == null)
                _instance = new SplashManager();

            return _instance;
        }

        public static void showSplash()
        {
            frmSplash splashForm = new frmSplash();
            splashForm.ShowDialog();
        }

        public void show()
        {
            // show splash
            thread = new Thread(new ThreadStart(SplashManager.showSplash));
            thread.Start();
            Thread.Sleep(600);
        }

        public void close()
        {
            Thread.Sleep(500);
            try
            {
                if ( thread != null )
                    thread.Abort();
            }
            catch (ThreadAbortException tae)
            {
            }
        }

    }
}
