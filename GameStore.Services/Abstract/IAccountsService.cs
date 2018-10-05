using GameStore.Data.Models;

namespace GameStore.Services
{
    public interface IAccountsService
    {
        Account AddAccount(string firstName, string lastName, string userName, bool isAdmin = false);
        bool IsAdmin(string accountName);
        string RemoveAccount(string commandExecutor, string accountName);
    }
}