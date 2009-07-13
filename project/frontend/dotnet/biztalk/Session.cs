using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using log4net;
using log4net.Config;
using System.Collections;


namespace biztalk
{
    public class Session
    {
        private const int TIMEOUT = 5000;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string server;
        private Logger logger = new Logger();

        public Session( string server )
        {
            logger.init();
            this.server = server;
        }

        public string request( string resource, Hashtable args )
        {
            string url;

            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = "GET";
            req.KeepAlive = true;
            req.ContentType = "text/plain";
            req.Timeout = TIMEOUT;

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            log.Debug("Response info: " + res.StatusDescription);

            System.IO.StreamReader sr = new System.IO.StreamReader(res.GetResponseStream());
            string content = sr.ReadToEnd();
            sr.Close();
            res.Close();
            return content;
        }

    }
}
