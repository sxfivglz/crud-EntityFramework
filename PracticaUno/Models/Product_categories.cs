using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PracticaUno.Models
{
    public class Product_categories
    {
        [Key]
        
        public int category_id { get; set; }
        
        public string? category_name { get; set; }

      
    }
}
