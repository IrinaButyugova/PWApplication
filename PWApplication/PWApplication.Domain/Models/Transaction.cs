﻿using System;

namespace PWApplication.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime Date { get; set;}

        public TransactionType Type { get; set; }

        public Decimal Amount { get; set; }

        public Decimal UserBalance { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string CorrespondentId { get; set; }

        public User Correspondent { get; set; }
    }
}
