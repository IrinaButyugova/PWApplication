using System.ComponentModel.DataAnnotations;

namespace PWBlazorApplication.Models
{
    public class CreateTransactionModel
    {
        public IEnumerable<string> Users { get; set; } = new List<string>();

        [Required]
        public string RecipientName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
