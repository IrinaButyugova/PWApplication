﻿using PWApplication.Domain.Models;

namespace PWBlazorApplication.Models
{
    public class HomeModel
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public List<Transaction> Transactions { get; set; }

        //public FilterViewModel FilterViewModel { get; set; }

        //public SortState CurrentSort { get; set; }
    }
}
