using System.IO;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        Task AddCreditCard(string cardNumber, Account account);
        Task<string> DeleteAccount(string accountId);
        Task<string> RestoreAccount(Account commandExecutor, Account accountName);
        Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId);
    }
}