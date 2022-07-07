using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator.Models
{
    public class CustomDataParams
    {
        /// <summary> The category of the content associated with the event. </summary> 
        [JsonProperty(PropertyName = "content_category", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentCategory { get; set; }

        /// <summary> The content IDs associated with the event, such as product SKUs for items in an AddToCart event. If content_type is a product, 
        /// then your content IDs must be an array with a single string value. Otherwise, this array can contain any number of string values. </summary>
        [JsonProperty(PropertyName = "content_ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ContentIds { get; set; }


        /// <summary>The name of the page or product associated with the event. </summary>
        [JsonProperty(PropertyName = "content_name", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentName { get; set; }


        /// <summary> Should be set to product or product_group: select according to your sales order  </summary>
        [JsonProperty(PropertyName = "content_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }


        /// <summary>   A list of JSON objects that contain the product IDs associated with the event plus information about the products.
        /// Available fields: id, quantity, item_price, delivery_category </summary>
        [JsonProperty(PropertyName = "contents", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Contents { get; set; }



        /// <summary>The currency for the value specified, if applicable. Currency must be a valid ISO 4217 three-digit currency code. </summary>
        [JsonProperty(PropertyName = "currency", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string Currency { get; set; }

        /// <summary> Type of delivery for a purchase event. Supported values are:
        ///in_store — Customer needs to enter the store to get the purchased product.
        ///curbside — Customer picks up their order by driving to a store and waiting inside their vehicle.
        ///home_delivery — Purchase is delivered to the customer's home.</summary>
        [JsonProperty(PropertyName = "delivery_category", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryCategory { get; set; }


        /// <summary> The number of items that a user tries to buy during checkout.</summary>
        [JsonProperty(PropertyName = "num_items", NullValueHandling = NullValueHandling.Ignore)]
        public string NumItems { get; set; }


        /// <summary> The order ID for this transaction as a string. </summary>
        [JsonProperty(PropertyName = "order_id", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }


        /// <summary>The predicted lifetime value of a conversion event. </summary>
        [JsonProperty(PropertyName = "predicted_ltv", NullValueHandling = NullValueHandling.Ignore)]
        public float? PredictedLtv { get; set; }


        /// <summary> Use only with Search events.  A search query made by a user.</summary>
        [JsonProperty(PropertyName = "search_string", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchString { get; set; }



        /// <summary> Use only with CompleteRegistration events.   The status of the registration event as a string.</summary>
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }


        /// <summary>A numeric value associated with this event. This could be a monetary value or a value in some other metric. </summary>
        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public float Value { get; set; }
    }
}
