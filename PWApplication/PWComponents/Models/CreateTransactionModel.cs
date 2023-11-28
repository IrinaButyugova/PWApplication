using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace PWComponents.Models
{
    public class CreateTransactionModel
    {
        [Required]
        public string RecipientName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
		[Min(0.01)]
		public decimal Amount { get; set; }
    }
}
