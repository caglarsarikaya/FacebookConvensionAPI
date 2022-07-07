using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator.Models
{
    public class MainBodyParams
    {
        [JsonProperty(PropertyName = "data")]
        [Required]
        public List<ServerEventParams> Data { get; set; }


        [JsonProperty(PropertyName = "test_event_code", NullValueHandling = NullValueHandling.Ignore)]
        public string TestCode { get; set; }
    }
}
