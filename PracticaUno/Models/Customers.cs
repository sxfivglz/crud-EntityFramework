using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PracticaUno.Models
{
    public class Customers
    {
        [Key]
       
        public int customer_id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Address { get; set; }
        
        public string? Website { get; set; }
        
        public decimal Credit_limit { get; set; }

    }
}
