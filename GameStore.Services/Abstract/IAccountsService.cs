using System.IO;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        Task<Account> FindAccountAsync(string accountId);
        Task AddCreditCardAsync(string cardNumber, Account account);
        Task<string> DeleteAccountAsync(string accountId);
        Task<string> RestoreAccountAsync(Account commandExecutor, Account accountName);
        Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId);
    }
}