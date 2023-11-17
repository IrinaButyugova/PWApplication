using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PWApplication.BLL.Enums;
using PWApplication.BLL.Services;
using PWApplication.Domain.Models;
using PWBlazorApplication.Enums;
using PWBlazorApplication.Models;

namespace PWBlazorApplication.Components
{
    public partial class Home
    {
		[Inject]
        NavigationManager Navigation { get; set; }
		[Inject]
		AuthenticationStateProvider AuthenticationStateProvider { get; set; }
		[Inject]
		IAccountService AccountService { get; set; }
		[Inject]
		ITransactionService TransactionService { get; set; }
        private HomeModel _model = new HomeModel();
        private User _user;
        private int _transactionId = 0;

        protected async override Task OnInitializedAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var stateUser = state.User;
            if (stateUser == null)
            {
                return;
            }

            _user = AccountService.GetUser(stateUser.Identity.Name);
            if (_user == null)
            {
                return;
            }

            _model.Name = _user.UserName;
            _model.Balance = _user.Balance;
            _model.CurrentSort = SortState.DateDesc;
            _model.FilterModel = new FilterModel();
            UpdateTransactions();
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
            _model.CurrentSort = newSortState;
            UpdateTransactions();
        }

        private string GetSortStyle(ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.Date:
                    if (_model.CurrentSort == SortState.DateAsc)
                    {
                        return "glyphicon glyphicon-chevron-up";
                    }
                    if (_model.CurrentSort == SortState.DateDesc)
                    {
                        return "glyphicon glyphicon-chevron-down";
                    }
                    break;
                case ColumnType.CorrespondentName:
                    if (_model.CurrentSort == SortState.CorrespondentNameAsc)
                    {
                        return "glyphicon glyphicon-chevron-up";
                    }
                    if (_model.CurrentSort == SortState.CorrespondentNameDesc)
                    {
                        return "glyphicon glyphicon-chevron-down";
                    }
                    break;
                case ColumnType.Amount:
                    if (_model.CurrentSort == SortState.AmountAsc)
                    {
                        return "glyphicon glyphicon-chevron-up";
                    }
                    if (_model.CurrentSort == SortState.AmountDesc)
                    {
                        return "glyphicon glyphicon-chevron-down";
                    }
                    break;
            }

            return "";
        }

        private void Filter()
        {
            UpdateTransactions();
        }

        private void UpdateTransactions()
        {
            _model.Transactions = TransactionService.GetTransactions(_user.UserName, _model.FilterModel.StartDate, _model.FilterModel.EndDate, _model.FilterModel.CorrespondentName,
               _model.FilterModel.StartAmount, _model.FilterModel.EndAmount, _model.CurrentSort);
        }
    }
}
