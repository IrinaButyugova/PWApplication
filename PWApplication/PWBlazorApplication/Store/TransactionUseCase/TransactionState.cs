using PWApplication.BLL.Errors;
using PWBlazorApplication.Models;

namespace PWBlazorApplication.Store.TransactionUseCase
{
    public record TransactionState
	{
		public CreateTransactionModel Model { get; init; }
		public string UserName { get; init; }
		public bool Succeeded { get; init; }
		public List<Error> Errors { get; init; } = new List<Error>();

		public TransactionState()
		{

		}

		public TransactionState(IEnumerable<string> users, string userName, bool succeeded, List<Error> errors)
		{
			Model = new CreateTransactionModel()
			{ 
				Users = users
			};

			UserName = userName;
			Succeeded = succeeded;	
			Errors = errors;
		}
	}
}
