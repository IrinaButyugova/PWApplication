using PWApplication.BLL.Errors;
using PWBlazorApplication.Models;

namespace PWBlazorApplication.Store.TransactionUseCase
{
    public record TransactionState
	{
		public CreateTransactionModel Model { get; init; }
		public string UserName { get; init; }
		public bool CreationSucceeded { get; init; }
		public List<Error> Errors { get; init; } = new List<Error>();

		public TransactionState()
		{

		}

		public TransactionState(IEnumerable<string> users, string userName, bool creationSucceeded, List<Error> errors)
		{
			Model = new CreateTransactionModel()
			{ 
				Users = users
			};

			UserName = userName;
            CreationSucceeded = creationSucceeded;	
			Errors = errors;
		}
	}
}
