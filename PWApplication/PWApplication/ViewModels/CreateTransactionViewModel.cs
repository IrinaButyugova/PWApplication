using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PWApplication.ViewModels
{
    public class CreateTransactionViewModel
    {
        public SelectList Users { get; set; }

        [Required]
        public string RecipientName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
