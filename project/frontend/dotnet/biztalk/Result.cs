using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

namespace biztalk
{
    public class Result
    {
        [JsonProperty]
        public string desc { get; set; }

        [JsonProperty]
        public int code { get; set; }
        
        [JsonProperty]
        public System.Collections.Generic.List<Hashtable> return_ { get; set; }
    }
}
