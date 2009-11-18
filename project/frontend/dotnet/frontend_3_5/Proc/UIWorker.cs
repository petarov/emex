using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace frontend_3_5.Proc
{
    class UIWorker
    {
        private static UIWorker _instance = null;

        public static UIWorker Instance()
        {
            if (_instance == null)
                _instance = new UIWorker();

            return _instance;
        }

        
    }
}
