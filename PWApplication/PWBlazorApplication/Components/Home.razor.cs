using Fluxor;
using Microsoft.AspNetCore.Components;
using PWApplication.BLL.Enums;
using PWApplication.Domain.Models;
using PWBlazorApplication.Enums;
using PWBlazorApplication.Store.HomeUseCase;

namespace PWBlazorApplication.Components
{
    public partial class Home
    {
		[Inject]
        NavigationManager Navigation { get; set; }
		[Inject]
        IState<HomeState> HomeState { get; set; }
		[Inject]
		public IDispatcher Dispatcher { get; set; }

        private User _user;
        private int _transactionId = 0;

        protected async override Task OnInitializedAsync()
        {
			base.OnInitialized();
			Dispatcher.Dispatch(new FetchHomeDataAction());
        }

        private void Logout()
        {
            Navigation.NavigateTo($"/logout", true);
        }

        private void RepeatTransaction(int id)
        {
            _transactionId = id;
        }

        private void SortTable(SortState newSortState)
        {
            var action = new FetchTransactionsAction()
            {
                UserName = HomeState.Value.Name,
				StartDate = HomeState.Value.FilterModel.StartDate,
                EndDate = HomeState.Value.FilterModel.EndDate,
                CorrespondentName = HomeState.Value.FilterModel.CorrespondentName,
                StartAmount = HomeState.Value.FilterModel.StartAmount,
                EndAmount = HomeState.Value.FilterModel.EndAmount,
				SortState = newSortState
            };
            Dispatcher.Dispatch(action);
        }

        private string GetSortStyle(ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.Date:
                    if (HomeState.Value.CurrentSort == SortState.DateAsc)
                    {
                        return "glyphicon glyphicon-chevron-up";
                    }
                    if (HomeState.Value.CurrentSort == SortState.DateDesc)
                    {
                        return "glyphicon glyphicon-chevron-down";
                    }
                    break;
                case ColumnType.CorrespondentName:
                    if (HomeState.Value.CurrentSort == SortState.CorrespondentNameAsc)
                    {
                        return "glyphicon glyphicon-chevron-up";
                    }
                    if (HomeState.Value.CurrentSort == SortState.CorrespondentNameDesc)
                    {
                        return "glyphicon glyphicon-chevron-down";
                    }
                    break;
                case ColumnType.Amount:
                    if (HomeState.Value.CurrentSort == SortState.AmountAsc)
                    {
                        return "glyphicon glyphicon-chevron-up";
                    }
                    if (HomeState.Value.CurrentSort == SortState.AmountDesc)
                    {
                        return "glyphicon glyphicon-chevron-down";
                    }
                    break;
            }

            return "";
        }

        private void Filter()
        {
			var action = new FetchTransactionsAction()
			{
				UserName = HomeState.Value.Name,
				StartDate = HomeState.Value.FilterModel.StartDate,
				EndDate = HomeState.Value.FilterModel.EndDate,
				CorrespondentName = HomeState.Value.FilterModel.CorrespondentName,
				StartAmount = HomeState.Value.FilterModel.StartAmount,
				EndAmount = HomeState.Value.FilterModel.EndAmount,
				SortState = HomeState.Value.CurrentSort
			};
			Dispatcher.Dispatch(action);
		}
    }
}
