using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticaUno.Models
{
    public class Locations
    {
        [Key]
        [JsonPropertyName("location_id")]
        public int location_id { get; set; }
        [JsonPropertyName("address")]
        public string? address { get; set; }
        [JsonPropertyName("postal_code")]
        public string? postal_code {get;set;}
        [JsonPropertyName("city")]
        public string? city { get; set; }
        [JsonPropertyName("state")]
        public string? state { get; set; }
        [JsonPropertyName("country_id")]
        [ForeignKey("Countries")]
        public char? country_id { get; set; }

    }
}
