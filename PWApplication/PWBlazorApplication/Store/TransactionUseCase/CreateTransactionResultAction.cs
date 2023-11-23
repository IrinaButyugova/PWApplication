using PWApplication.BLL.Errors;

namespace PWBlazorApplication.Store.TransactionUseCase
{
	public class CreateTransactionResultAction
	{
		public bool Succeeded { get; init; }
		public List<Error> Errors { get; init; } = new List<Error>();

		public CreateTransactionResultAction(bool succeeded, List<Error> errors)
		{
			Succeeded = succeeded;
			Errors = errors;
		}
	}
}
