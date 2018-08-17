using System.ComponentModel.DataAnnotations;

namespace RazorPagesMoive.Model
{
	public class Customer
    {
		public int Id { get; set; }

		[Required, StringLength(100)]
		public string Name { get; set; }
	}
}
