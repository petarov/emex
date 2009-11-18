using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace json_test_1
{
    class BinaryData
    {
        [JsonProperty]
        public string filename { get; set; }

        [JsonProperty]
        public int size { get; set; }

        [JsonProperty]
        public string data { get; set; }

    }
}
