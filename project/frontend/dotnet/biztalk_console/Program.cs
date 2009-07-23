using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;

using biztalk;

namespace biztalk_console
{
    class Program
    {
        private const string USER = "deyan.imap@localhost";


        public void test_1()
        {
            Session bs = new Session("127.0.0.1", 8080, USER, "emex", "emex" );
            Hashtable h = new Hashtable();
            h["email"] = "deyan.imap@localhost";
            h["word"] = "message";
            //string url = "http://localhost:8080/search_email?email=deyan.imap@localhost&word=message";
            string str = bs.request(Session.RequestType.RT_GET, "search_email", h);
            //Console.WriteLine(str);

            Result bres = bs.JSON2Result(str);
            if (0 == bres.code)
            {
                Hashtable h2 = (Hashtable)bres.return_[0];
                Console.WriteLine(Convert.ToUInt32(h2["id"]));
                Console.WriteLine(Convert.ToString(h2["data"]));

            }
            Console.WriteLine(bres.desc);
        }

        public void test_2()
        {
            // SEND MAIL

            Session bs = new Session("127.0.0.1", 8080, USER, "emex", "emex");
            Hashtable h = new Hashtable();
            h["email"] = "deyan.imap@localhost";
            h["to"] = "ppetrov@localhost";
            h["subject"] = "TEST MAIL VIA Post methods";
            h["body"] = "Log4php is logging framework for PHP undergoing incubation at the Apache Software Foundation (ASF), sponsored by the Apache Logging Services project. Incubation is required of all newly accepted projects until a further review indicates that the infrastructure, communications, and decision making process have stabilized in a manner consistent with other successful ASF projects. While incubation status is not necessarily a reflection of the completeness or stability of the code, it does indicate that the project has yet to be fully endorsed by the ASF.";
            string str = bs.request(Session.RequestType.RT_POST, "send_email", h);
            Result bres = bs.JSON2Result(str);
            if (0 == bres.code)
            {
                //Hashtable h2 = (Hashtable)bres.return_[0];
                //Console.WriteLine(Convert.ToUInt32(h2["id"]));
                //Console.WriteLine(Convert.ToString(h2["data"]));

            }
            Console.WriteLine(bres.desc);
        }

        public void test_3()
        {
            Session bs = new Session("127.0.0.1", 8080, USER, "emex", "emex");
            Command cmd = new Command(bs);
            //Result bres = cmd.GetContacts();
            //if (0 == bres.code)
            //{
            //    Hashtable h2 = (Hashtable)bres.return_[0];
            //    //System.Diagnostics.Debug.WriteLine( 
            //}
            //Console.WriteLine(bres.desc);
        }

        static void Main(string[] args)
        {
            Program prg = new Program();

            try
            {
                //prg.test_1();
                prg.test_3();
            }
            catch (WebException wex)
            {
                Console.WriteLine("Web Exception: " + wex.ToString());
                Console.WriteLine();
                Console.WriteLine("NOTE: DID YOU START THE PERL SCRIPT HTTP SERVER ? (test.http/server_1.pl)");
            }

        }
    }
}
