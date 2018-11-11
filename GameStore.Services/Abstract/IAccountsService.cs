using System.IO;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        Task<Account> FindAccountAsync(string accountId);
        Task AddCreditCardAsync(string cardNumber, Account account);
        Task DeleteAccountAsync(string accountId);
        Task RestoreAccountAsync(Account commandExecutor, Account accountName);
        Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId);
    }
}