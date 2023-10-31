using System.ComponentModel.DataAnnotations;

namespace PWBlazorApplication.Models
{
    public class CreateTransactionModel
    {
        public List<string> Users { get; set; }

        [Required]
        public string RecipientName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
