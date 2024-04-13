using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaUno.Models
{
	public class Contacts
	{
		[Key]
		public int contact_id { get; set; }

		public string First_name { get; set; }

		public string Last_name { get; set; }

		public string Email { get; set; }

		public string? Phone { get; set; }

		[ForeignKey("Customers")]
		public int? customer_id { get; set; }

		public Customers? Customers { get; set; }	
	}
}
