using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaUno.Models
{
    public class Products
    {
        [Key]
        public int product_id { get; set; }
        public string? Product_name { get; set; }
        public string? Description { get; set; }
        public decimal Standard_cost { get; set; }
        public decimal List_price { get; set; }
        [ForeignKey("Product_Categories")]
        public int category_id { get; set; }

        public Product_categories? Product_Categories { get; set; }

    }
}
