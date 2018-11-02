using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId);
        Account FindAccount(string accountName, bool getAllData = false);
        IQueryable<Account> GetAccounts();
        string RemoveAccount(Account commandExecutor, Account accountName);
        string RestoreAccount(Account commandExecutor, Account accountName);
        void AddCreditCard(string cardNumber, Account account);
    }
}