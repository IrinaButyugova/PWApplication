using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using PWBlazorApplication.Models;
using PWApplication.BLL.Services;
using Microsoft.AspNetCore.Components;

namespace PWBlazorApplication.Components
{
	public partial class CreateTransaction
	{
		[Inject]
		IAccountService AccountService { get; set; }
		[Inject]
		ITransactionService TransactionService { get; set; }
		[Inject]
		ITransferService TransferService { get; set; }
		[Inject]
		AuthenticationStateProvider AuthenticationStateProvider { get; set; }
		[Inject]
		NavigationManager Navigation { get; set; }

		public CreateTransactionModel createModel = new CreateTransactionModel();
		private EditContext? editContext;
		private ValidationMessageStore? messageStore;
		private string userName;

		[Parameter]
		public int Id { get; set; }

		protected async override Task OnInitializedAsync()
		{
			var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			userName = state.User.Identity.Name;
			createModel.Users = new List<string>();
			createModel.Users.Add("");
			createModel.Users.AddRange(AccountService.GetOtherUsersNames(userName));

			editContext = new(createModel);
			messageStore = new(editContext);
		}

		protected override void OnParametersSet()
		{
			if (Id == 0)
			{
				return;
			}

			var transaction = TransactionService.GetTransaction(Id);
			createModel.Amount = transaction.Amount;
			createModel.RecipientName = transaction.Correspondent.UserName;
		}

		private async void ValidSubmit()
		{
			messageStore.Clear();

			if (editContext == null || !editContext.Validate())
			{
				return;
			}

			if (createModel.Amount == 0)
			{
				messageStore.Add(() => createModel.Amount, "Amount should be greater than zero");
			}
			else
			{
				var result = TransferService.CreateTransaction(userName, createModel.RecipientName, createModel.Amount);
				if (result.Succeeded)
				{
					Navigation.NavigateTo("/", true);
				}
				else
				{
					foreach (var error in result.Errors)
					{
						messageStore.Add(() => createModel.Amount, error.Description);
					}
				}
			}

			if (editContext.GetValidationMessages().Count() > 0)
			{
				editContext.NotifyValidationStateChanged();
			}
		}
	}
}
