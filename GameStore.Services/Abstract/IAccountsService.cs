using System.IO;
using System.Threading.Tasks;
using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAccountsService
    {
        void AddCreditCard(string cardNumber, Account account);
        string DeleteAccount(string accountId);
        string RestoreAccount(Account commandExecutor, Account accountName);
        Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId);
    }
}