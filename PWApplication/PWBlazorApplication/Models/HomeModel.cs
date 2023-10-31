using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;

namespace PWBlazorApplication.Models
{
    public class HomeModel
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public List<Transaction> Transactions { get; set; }

        public FilterModel FilterModel { get; set; }

        public SortState CurrentSort { get; set; }
    }
}
