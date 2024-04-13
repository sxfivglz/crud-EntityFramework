using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PracticaUno.Models
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; }

        public DateTime? order_date { get; set; }

        public String? status { get; set; }

        // Clave foránea para customer_id referenciando la tabla Customer
        [ForeignKey("Customers")]
        public int customer_id { get; set; }

        // Propiedad de navegación para acceder a la entidad Customer relacionada
        public Customers? Customers { get; set; }

        // Clave foránea para salesman_id referenciando la tabla Employee
        [ForeignKey("Employees")]
        public int? salesman_id { get; set; }

        public Employees? Employees { get; set; }



    }
}

