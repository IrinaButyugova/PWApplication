using Microsoft.AspNetCore.Components.Forms;
using PWBlazorApplication.Models;
using Microsoft.AspNetCore.Components;
using Fluxor;
using PWBlazorApplication.Store.TransactionUseCase;

namespace PWBlazorApplication.Components
{
	public partial class CreateTransaction
	{
		[Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public IState<TransactionState> TransactionState { get; set; }
		[Parameter]
		public int Id { get; set; }
		[Parameter]
		public EventCallback OnCreateCallback { get; set; }
		private CreateTransactionModel _createModel => TransactionState.Value.Model;
		private List<string> _validationMessages = new List<string>();

		protected override void OnInitialized()
		{
            base.OnInitialized();
			TransactionState.StateChanged += OnStateChanged;
            Dispatcher.Dispatch(new FetchUsersAction());
		}

        private async void OnStateChanged(object? sender, EventArgs e)
        {
            if (TransactionState.Value.CreationSucceeded)
            {
                await OnCreateCallback.InvokeAsync();
            }
            else if (TransactionState.Value.Errors != null)
            {
                _validationMessages.Clear();
                foreach (var error in TransactionState.Value.Errors)
                {
                    _validationMessages.Add(error.Description);
                }
            }
        }

        protected override void OnParametersSet()
		{
			if (Id == 0)
			{
				return;
			}

			Dispatcher.Dispatch(new FetchTransactionDataAction(Id));
		}

		private async void ValidSubmit(EditContext editContext)
		{
			_validationMessages.Clear();

			if (TransactionState.Value.Model.Amount == 0)
			{
				var messages = editContext.GetValidationMessages();
				_validationMessages.Add("Amount should be greater than zero");
			}

			if (editContext == null || !editContext.Validate() || _validationMessages.Any())
			{
				return;
			}

			Dispatcher.Dispatch(new CreateTransactionAction(TransactionState.Value.UserName, TransactionState.Value.Model.RecipientName, TransactionState.Value.Model.Amount));
        }
	}
}
