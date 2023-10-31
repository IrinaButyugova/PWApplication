namespace PWBlazorApplication.Models
{
    public class FilterModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string CorrespondentName { get; set; }

        public decimal? StartAmount { get; set; }

        public decimal? EndAmount { get; set; }
    }
}
