using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace PracticaUno.Models
{
    public class Warehouses
    {
        [Key]
        public int warehouse_id { get; set; }
        [JsonPropertyName("warehouse_name")]
        public string? warehouse_name { get; set; }
       
        [ForeignKey("Locations")]
        public int? location_id { get; set; }
        public Locations? Locations { get; set; }
    }
}
