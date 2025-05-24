using BusinessObjects;
using Repositories;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;

        public AccountService()
        {
            iAccountRepository = new AccountRepository();
        }
        public AccountMember GetAccountById(string accountID)
        {
            Console.WriteLine("haa");
            return iAccountRepository.GetAccountById(accountID);
        }
    }
}
