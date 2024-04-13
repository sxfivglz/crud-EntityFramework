using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaUno.Models

{
    public class Order_items
    {
        [Key]
        [JsonPropertyName("order_id")]
        [ForeignKey("Orders")]
        public int order_id { get; set; }
        [JsonPropertyName("item_id")]
        [ForeignKey("Products")]
        public int item_id { get; set; }
        [JsonPropertyName("product_id")]
        public int product_id { get; set; }
        [JsonPropertyName("quantity")]
        public int quantity { get; set; }
        [JsonPropertyName("unit_price")]
        public double unit_price { get; set; }
        
    }
}
