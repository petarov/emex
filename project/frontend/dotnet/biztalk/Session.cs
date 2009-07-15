using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using log4net;
using log4net.Config;
using System.Collections;
using Newtonsoft.Json;

namespace biztalk
{
    public class Session
    {
        private const int TIMEOUT   = 60000;
        private const string SCHEME = "http";
        private const string USER_AGENT = "EmEx FrontEnd Biztalk";

        public enum RequestType
        {
            RT_GET = 0,
            RT_POST
        };

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Logger logger = new Logger();
        private string server;
        private int port = 80;
        private WebProxy proxy = null;
        private string userMail = string.Empty;

        public Session( string server, int port, string userMailAddress )
        {
            logger.init();
            this.server = server;
            this.port = port;
            this.userMail = userMailAddress;

            log.Debug(String.Format("Create session with server={0}, port={1}", server, port));
        }

        public Session(string server, int port, string userMailAddress, string proxyhost, int proxyport)
            : this(server, port, userMailAddress)
        {
            proxy = new WebProxy(proxyhost, proxyport);
            log.Debug(String.Format("Added session proxy - host={0}, port={1}", proxyhost, proxyport));
        }

        public string UserMail
        {
            get
            {
                return this.userMail;
            }
        }

        public Uri Url
        {
            get
            {
                UriBuilder uribuilder = new UriBuilder(SCHEME, this.server, this.port);
                return uribuilder.Uri;
            }
        }

        public WebProxy Proxy
        {
            get
            {
                return this.proxy;
            }
        }

        private string _getParamsFromHash(Hashtable args)
        {
            string q = string.Empty;
            foreach (string key in args.Keys)
            {
                string val = Convert.ToString( args[key] );
                q += key + "=" + val + "&";
            }
            return q;
        }

        private string _getResponseData(HttpWebResponse resp)
        {
            if ( log.IsDebugEnabled )
                log.Debug("Response description: " + resp.StatusDescription + " .Getting response data ...");

            StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string content = sr.ReadToEnd();
            sr.Close();
            resp.Close();

            return content;
        }

        public string request(RequestType type, string resource, params string[] args)
        {
            //Note: arguments length must be even number !
            Hashtable hargs = new Hashtable(args.Length);
            for (int i = 0; i < args.Length / 2; i += 2)
            {
                hargs[args[i]] = args[i + 1];
            }
            return this.request(type, resource, hargs);
        }

        public string request(RequestType type, string resource, Hashtable args)
        {
            HttpWebRequest req = null;

            if (RequestType.RT_GET == type)
            {
                Uri myUri = new Uri(this.Url, resource + "?" + _getParamsFromHash(args));
                log.Debug("GET " + myUri.ToString());

                req = (HttpWebRequest)System.Net.WebRequest.Create(myUri);
                req.Proxy = this.proxy;
                req.Method = "GET";
                req.ContentType = "text/plain";
            }
            else if (RequestType.RT_POST == type)
            {
                Uri myUri = new Uri(this.Url, resource);
                log.Debug("POST " + myUri.ToString());

                req = (HttpWebRequest)System.Net.WebRequest.Create(myUri);
                req.Proxy = this.proxy;
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                byte[] buffer = Encoding.UTF8.GetBytes(_getParamsFromHash(args));
                Stream stream = req.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
            }
            else
            {
                log.Error("Invalid RequestType passed!");
                throw new WebException("Invalid RequestType passed!");
            }

            req.KeepAlive = true;
            req.Timeout = TIMEOUT;
            req.UserAgent = USER_AGENT;

            return _getResponseData( (HttpWebResponse)req.GetResponse() );
        }

        public Result JSON2Result(string json)
        {
            Result res = (Result)JsonConvert.DeserializeObject(json, typeof(Result));
            return res;
        }

        public Hashtable JSON2Hash(string json)
        {
            Hashtable h = (Hashtable)JsonConvert.DeserializeObject(json, typeof(Hashtable));
            return h;
        }

    }
}
