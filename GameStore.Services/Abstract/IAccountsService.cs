using System.Linq;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        Account AddAccount(string firstName, string lastName, string userName, string password, bool isAdmin = false,
            bool isGuest = false);

        Account AddAccount(Account account);
        Account FindAccount(string accountName, bool getAllData = false);
        Account GetGuestAccount();
        IQueryable<Account> GetAccounts();
        bool IsAdmin(Account account);
        string RemoveAccount(Account commandExecutor, Account accountName);
        string RestoreAccount(Account commandExecutor, Account accountName);
        void AddCreditCard(string cardNumber, Account account);
    }
}