using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace json_test_1
{
    class Person
    {
        string name;
        int age;
        string[] likes;
    }

    class Program
    {
        public string getJSONString(string url)
        {
            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = "GET";
            req.KeepAlive = true;
            req.ContentType = "text/plain";
            req.Timeout = 3000;

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Console.WriteLine("Response info: " + res.StatusDescription);
            System.IO.StreamReader sr = new System.IO.StreamReader(res.GetResponseStream());
            string content = sr.ReadToEnd();
            sr.Close();
            res.Close();
            return content;
        }

        public void start()
        {
            Console.WriteLine("Testing ...");
            string url = "http://127.0.0.1:8080/json-test-1";

            try
            {
                Console.WriteLine("Getting from {0}", url);
                string str = getJSONString(url);

                Person h = (Person)JavaScriptConvert.DeserializeObject(str, typeof(Person));
                //Console.WriteLine(h["name"]);
                Console.WriteLine("OUTPUT: {0}", str);
                
            }
            catch (WebException wex)
            {
                Console.WriteLine("Web Exception: " + wex.ToString());
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.start();
        }
    }
}
