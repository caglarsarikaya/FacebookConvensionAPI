using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator.Models
{
    public class CustomerInfoParams
    {
        /// <summary>HashRequired SHA256 only small latters</summary>
        [JsonProperty(PropertyName = "em", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> EmailAddress { get; set; }

        /// <summary>HashRequired SHA256 only numbers, no space no plus no any other thing</summary>
        [JsonProperty(PropertyName = "ph", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> PhoneNumber { get; set; }

        /// <summary>HashRequired SHA256, utf8 encode for special chars, a-z only (lowercase) </summary>
        [JsonProperty(PropertyName = "fn", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> FirstName { get; set; }


        /// <summary>HashRequired SHA256, utf8 encode for special chars, a-z only (lowercase) </summary>
        [JsonProperty(PropertyName = "ln", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> LastName { get; set; }


        /// <summary> HashRequired SHA256,  YYYYMMDD  </summary>
        [JsonProperty(PropertyName = "db", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> DateOfBirth { get; set; }


        /// <summary> lowercase   f or m   </summary>
        [JsonProperty(PropertyName = "ge", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Gender { get; set; }


        /// <summary>HashRequired SHA256, utf8 encode for special chars, a-z only (lowercase) </summary>
        [JsonProperty(PropertyName = "ct", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> City { get; set; }


        /// <summary>HashRequired SHA256, utf8 encode for special chars, a-z only (lowercase)   2 char like az or ca</summary>
        [JsonProperty(PropertyName = "st", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> State { get; set; }


        /// <summary> HashRequired SHA256,  Use only the first 5 digits</summary>
        [JsonProperty(PropertyName = "zp", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Zip { get; set; }


        /// <summary>HashRequired SHA256, utf8 encode for special chars, a-z only (lowercase)   2 char like az or ca</summary>
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Country { get; set; }


        /// <summary>HashRecommended SHA256 </summary>
        [JsonProperty(PropertyName = "external_id", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ExternalId { get; set; }


        /// <summary>Do not hash. </summary>
        [JsonProperty(PropertyName = "client_ip_address", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientIpAddress { get; set; }


        /// <summary>Do not hash. </summary>

        [JsonProperty(PropertyName = "client_user_agent", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientUserAgent { get; set; }


        /// <summary>Do not hash. </summary>

        [JsonProperty(PropertyName = "fbc", NullValueHandling = NullValueHandling.Ignore)]
        public string ClickId { get; set; }


        /// <summary>Do not hash. </summary>

        [JsonProperty(PropertyName = "fbp", NullValueHandling = NullValueHandling.Ignore)]
        public string BrowserId { get; set; }


        /// <summary>Do not hash. </summary>

        [JsonProperty(PropertyName = "subscription_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SubscriptionId { get; set; }



        /// <summary>Do not hash. </summary>

        [JsonProperty(PropertyName = "fb_login_id", NullValueHandling = NullValueHandling.Ignore)]
        public string FacebookLoginId { get; set; }



        /// <summary>Do not hash. </summary>

        [JsonProperty(PropertyName = "lead_id", NullValueHandling = NullValueHandling.Ignore)]
        public string LeadId { get; set; }


    }
}
