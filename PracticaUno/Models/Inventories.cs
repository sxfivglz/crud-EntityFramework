using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaUno.Models
{
    public class Inventories
    {
        [Key]
        [JsonPropertyName("product_id")]
        [ForeignKey("Products")]
        public int product_id { get; set; }
        [JsonPropertyName("warehouse_id")]
        [ForeignKey("Warehouses")]
        public int warehouse_id { get; set;}
        [JsonPropertyName("quantity")]
        public int quantity { get; set; }


    }
}
