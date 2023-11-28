using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PWApplication.BLL.Enums;
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

        protected override void OnInitialized()
        {
			base.OnInitialized();
			HomeState.StateChanged += OnStateChanged;
			FetchHomeData();
        }

        private void FetchHomeData()
        {
            Dispatcher.Dispatch(new FetchHomeDataAction());
        }

        private void Logout()
        {
            Navigation.NavigateTo($"/logout", true);
        }

        private void RepeatTransaction(int id)
        {
			var action = new FetchTransactionDataAction(id);
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

        private void FetchTransactions(SortState sortState)
        {
            var action = new FetchTransactionsAction()
			{
				UserName = HomeState.Value.Name,
				StartDate = HomeState.Value.FilterModel.StartDate,
				EndDate = HomeState.Value.FilterModel.EndDate,
				CorrespondentName = HomeState.Value.FilterModel.CorrespondentName,
				StartAmount = HomeState.Value.FilterModel.StartAmount,
				EndAmount = HomeState.Value.FilterModel.EndAmount,
				SortState = sortState
			};
			Dispatcher.Dispatch(action);
        }

		private void OnStateChanged(object? sender, EventArgs e)
		{
			if (HomeState.Value.CreationSucceeded)
			{
				FetchHomeData();
			}
		}

		public void CreateTransactionSubmit(EditContext editContext)
		{
			if (editContext == null || !editContext.Validate())
			{
				return;
			}

			var action = new CreateTransactionAction(HomeState.Value.Name, HomeState.Value.CreateTransactionModel.RecipientName, HomeState.Value.CreateTransactionModel.Amount);
            Dispatcher.Dispatch(action);
        }
	}
}
