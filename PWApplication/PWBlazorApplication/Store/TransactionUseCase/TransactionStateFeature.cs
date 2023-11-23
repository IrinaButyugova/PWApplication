using Fluxor;
using PWBlazorApplication.Models;

namespace PWBlazorApplication.Store.TransactionUseCase
{
    public class TransactionStateFeature : Feature<TransactionState>
    {
        public override string GetName() => "Transaction";

        protected override TransactionState GetInitialState()
        {
            return new TransactionState
            {
                Model = new CreateTransactionModel()
            };
        }
    }
}
