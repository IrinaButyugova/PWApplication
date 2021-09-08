using System;
using System.ComponentModel.DataAnnotations;

namespace PWApplication.ViewModels
{
    public class FilterViewModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string CorrespondentName { get; set; }

        public decimal? StartAmount { get; set; }

        public decimal? EndAmount { get; set; }
    }
}
