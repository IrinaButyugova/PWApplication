using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PWApplication.Models
{
    public class User : IdentityUser
    {
        public decimal Balance { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public List<Transaction> CorrespondentTransactions { get; set; } = new List<Transaction>();
    }
}
