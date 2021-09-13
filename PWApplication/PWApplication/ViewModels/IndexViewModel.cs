using PWApplication.Domain.Models;
using System.Collections.Generic;

namespace PWApplication.ViewModels
{
    public class IndexViewModel
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public List<Transaction> Transactions { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}
