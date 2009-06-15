using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;

namespace soap_test_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing ...");
            
            localhost.soaplite.EmployeeHandlerService ehs = new soap_test_1.localhost.soaplite.EmployeeHandlerService();
            ehs.Url = "http://localhost:9002";
            
            string test = "hello cworld";
            ehs.@new(@test);
            ehs.getName();

            //Console.WriteLine( );
            Console.ReadKey();
        }
    }
}
