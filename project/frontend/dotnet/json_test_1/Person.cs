using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace json_test_1
{
    public class Person
    {
        [JsonProperty]
        public string name { get; set; }

        [JsonProperty]
        public int age { get; set; }

        [JsonProperty]
        public string[] likes { get; set; }
    }
}
