using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace descarga_ciec_sdk.src.Models
{
    public class Autenticación
    {
        [JsonProperty(PropertyName = "user")]
        public User user { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public string Error { get; set; }
    }
}
