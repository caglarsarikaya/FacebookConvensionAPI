using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator.Models
{
    public class ServerEventParams
    {
        /// <summary> deduplice </summary>
        [JsonProperty(PropertyName = "event_name", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string EventName { get; set; }

        /// <summary> unix timestamp max 7 days ago accepted </summary>
        [JsonProperty(PropertyName = "event_time", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public long EventTime { get; set; }


        /// <summary> userdata </summary>
        [JsonProperty(PropertyName = "user_data", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public CustomerInfoParams UserData { get; set; }

        /// <summary> additinal events </summary>
        [JsonProperty(PropertyName = "custom_data", NullValueHandling = NullValueHandling.Ignore)]
        public CustomDataParams? CustomData { get; set; }


        /// <summary> matched verified domain, required for conversion api </summary>
        [JsonProperty(PropertyName = "event_source_url", NullValueHandling = NullValueHandling.Ignore)]
        public string? EventSourceUrl { get; set; }


        /// <summary> delivery optimization  If set to true, we only use the event for attribution.</summary>
        [JsonProperty(PropertyName = "opt_out", NullValueHandling = NullValueHandling.Ignore)]
        public bool OptOut { get; set; }


        /// <summary> deduplice, when more pruduct in same order, bridge for the product order relation, generate random number</summary>
        [JsonProperty(PropertyName = "event_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? EventId { get; set; }

        /// <summary> email, website, phone_call, chat, physical_store, system_generated, other  </summary>
        [JsonProperty(PropertyName = "action_source", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string ActionSource { get; set; }

        /// <summary> "data_processing_options": ["LDU"] </summary>
        [JsonProperty(PropertyName = "data_processing_options", NullValueHandling = NullValueHandling.Ignore)]
        public string[] DataProcessingOptions { get; set; }

        /// <summary> Current accepted values are 1, for the United States of America, or 0, to request that we geolocate that event </summary>
        [JsonProperty(PropertyName = "data_processing_options_country", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public int? DataProcessingOptionsCountry { get; set; }


        /// <summary> Current accepted values are 1000, for California, or 0, to request that we geolocate that event. </summary>
        [JsonProperty(PropertyName = "data_processing_options_state", NullValueHandling = NullValueHandling.Ignore)]
        public int? DataProcessingOptionsState { get; set; }


    }
}
