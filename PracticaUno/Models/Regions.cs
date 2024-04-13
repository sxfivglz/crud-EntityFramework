using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PracticaUno.Models
{
    public class Regions
    {
        [Key]
        [JsonPropertyName("region_id")]
        public int region_id { get; set; }
        [JsonPropertyName("region_name")]
        public string region_name { get; set;}
      
    }
}
