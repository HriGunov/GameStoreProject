using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        Account AddAccount(string firstName, string lastName, string userName, string password, bool isAdmin = false, bool isGuest = false);
        Account AddAccount(Account account);
        Account FindAccount(string accountName);
        Account GetGuestAccount();
        bool IsAdmin(string accountName);
        string RemoveAccount(string commandExecutor, string accountName);
        string RestoreAccount(string commandExecutor, string accountName);
    }
}