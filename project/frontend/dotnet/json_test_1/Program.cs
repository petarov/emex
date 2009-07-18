using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace json_test_1
{
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

        public void test_1()
        {
            string url = "http://127.0.0.1:8080/json-test-1";
            Console.WriteLine("Getting from {0}", url);
            string str = getJSONString(url);
            Console.WriteLine("JSON String: {0}", str);

            Console.WriteLine("Deserialize into Hash ...");
            Hashtable h = (Hashtable)JsonConvert.DeserializeObject(str, typeof(Hashtable));
            Console.WriteLine(h["name"]);
            Console.WriteLine(h["age"]);
            Console.WriteLine(h["likes"]);

            Console.WriteLine("Deserialize into Object ...");
            Person p = (Person)JsonConvert.DeserializeObject(str, typeof(Person));
            Console.WriteLine(p.name);
            Console.WriteLine(p.age);
            Console.WriteLine(p.likes[0]);
        }

        public void test_2()
        {
            string url = "http://127.0.0.1:8080/json-test-2";
            Console.WriteLine("Getting from {0}", url);
            string str = getJSONString(url);
            //Console.WriteLine("JSON String: {0}", str);

            Console.WriteLine("Deserialize into Hash ...");
            Hashtable h = (Hashtable)JsonConvert.DeserializeObject(str, typeof(Hashtable));
            Console.WriteLine(h["filename"]);
            Console.WriteLine(h["size"]);
            //Console.WriteLine(h["data"]);

            Console.WriteLine("Deserialize into Object ...");
            BinaryData b = (BinaryData)JsonConvert.DeserializeObject(str, typeof(BinaryData));
            Console.WriteLine(b.filename);
            Console.WriteLine(b.size);
            Console.WriteLine("--JSON LEN: " + b.data.Length);
            Console.WriteLine("--DECODED FROM B64 - LEN: " + Convert.FromBase64String(b.data).Length);

            // write sample bin file to check agains binary data
            System.IO.FileStream fs = new System.IO.FileStream(b.filename, System.IO.FileMode.Create);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            //bw.Write(Encoding.ASCII.GetBytes(b.data));
            bw.Write( Convert.FromBase64String(b.data) );
            bw.Close();
            fs.Close();
            
        }

        public void start()
        {
            Console.WriteLine("Testing ...");
            
            try
            {
                test_1();
                test_2();
            }
            catch (WebException wex)
            {
                Console.WriteLine("Web Exception: " + wex.ToString());
                Console.WriteLine();
                Console.WriteLine("NOTE: DID YOU START THE PERL SCRIPT HTTP SERVER ? (test.http/server_1.pl)");
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
