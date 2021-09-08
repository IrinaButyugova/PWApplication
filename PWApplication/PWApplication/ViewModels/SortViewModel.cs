using PWApplication.Enums;

namespace PWApplication.ViewModels
{
    public class SortViewModel
    {
        public SortState DateSort { get; set; }
        public SortState CorrespondentNameSort { get; set; }
        public SortState AmountSort { get; set; }
        public SortState Current { get; set; }
        public bool Up { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            DateSort = SortState.DateAsc;
            CorrespondentNameSort = SortState.CorrespondentNameAsc;
            AmountSort = SortState.AmountAsc;

            if (sortOrder == SortState.DateDesc || sortOrder == SortState.CorrespondentNameDesc
                || sortOrder == SortState.AmountDesc)
            {
                Up = false;
            }
            else
            {
                Up = true;
            }

            switch (sortOrder)
            {
                case SortState.DateDesc:
                    Current = DateSort = SortState.DateAsc;
                    break;
                case SortState.CorrespondentNameAsc:
                    Current = CorrespondentNameSort = SortState.CorrespondentNameDesc;
                    break;
                case SortState.CorrespondentNameDesc:
                    Current = CorrespondentNameSort = SortState.CorrespondentNameAsc;
                    break;
                case SortState.AmountAsc:
                    Current = AmountSort = SortState.AmountDesc;
                    break;
                case SortState.AmountDesc:
                    Current = AmountSort = SortState.AmountAsc;
                    break;
                default:
                    Current = DateSort = SortState.DateDesc;
                    break;
            }
        }
    }
}
