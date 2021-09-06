using PWApplication.Models;
using System.Linq;

namespace PWApplication.Services
{
    public class ExchangeService : IExchange
    {
        private readonly ApplicationContext _appContext;

        public ExchangeService(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public void IncreaseBalance(string id, decimal amount)
        {
            var user = _appContext.Users.Where(x => x.Id == id).FirstOrDefault();
            user.Balance += amount;
            _appContext.SaveChanges();
        }
    }
}
