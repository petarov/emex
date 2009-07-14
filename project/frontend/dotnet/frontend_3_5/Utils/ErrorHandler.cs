using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using biztalk;

namespace frontend_3_5.Utils
{
    class ErrorHandler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Exception ex;

        public ErrorHandler(Exception ex)
        {
            this.ex = ex;
        }

        public static void checkBizResult( biztalk.Result result )
        {
            if ( result.code != 0 )
            {
                throw new Exception( string.Format("Error={0} / Message={1}",
                    result.code, result.desc) );
            }
        }

        public void Error()
        {
            if ( log.IsErrorEnabled )
                log.Error("Error Handler says => " + ex.ToString());

            MessageBox.Show( ex.ToString(), 
                "Error in Application", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error );
        }

        //public void Exclamation()
        //{
        //}
    }
}
