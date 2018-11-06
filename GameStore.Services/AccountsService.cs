using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services
{
    public class AccountsService : IAccountsService
    {
     
        private readonly GameStoreContext storeContext;

        public AccountsService(GameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
        }

        public async Task<Account> FindAccountAsync(string accountId)
        { 
            return await storeContext.Users.FindAsync(accountId); 
        }

        public async Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId)
        {
            var user = storeContext.Users.Find(userId);
            if (user == null) throw new Exception("User Not Found");

            var imageName = Guid.NewGuid() + Path.GetExtension(filename);
            var path = Path.Combine(root, imageName);

            using (var fileStream = File.Create(path))
            {
                await stream.CopyToAsync(fileStream);
            }

            user.AvatarImageName = imageName;
            storeContext.SaveChanges();
        }


        /// <summary>
        ///     Deletes an account by changing it's IsDeleted flag and sets the DeletedBy to the commandExecutor's name.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be removed</param>
        /// <returns></returns>
        public async Task<string> DeleteAccountAsync(string accountId)
        {
            var account = await storeContext.Users.FindAsync(accountId);
            if (account == null) throw new Exception("Account not found");

            if (account.IsDeleted) return $"Account {account.UserName} was not found.";

            account.IsDeleted = true;
            account.ModifiedOn = DateTime.Now;
            await storeContext.SaveChangesAsync();
            return $"Account {account.UserName} has been successfully removed.";
        }

        /// <summary>
        ///     Adds credit card to the given user.
        /// </summary>
        /// <param name="cardNumber">Card number.</param>
        /// <param name="account">Account Type</param>
        public async Task AddCreditCardAsync(string cardNumber, Account account)
        {
            var tempAccount = await storeContext.Accounts.Where(a => a.UserName == account.UserName).SingleAsync();
            account.CreditCard = cardNumber;
            tempAccount.CreditCard = cardNumber;
            await storeContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Restores an account by changing it's IsDeleted flag and clears the DeletedBy field.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be restored</param>
        /// <returns></returns>
        public async Task<string> RestoreAccountAsync(Account commandExecutor, Account accountName)
        {
            var account = await storeContext.Accounts.Where(a => a.UserName == accountName.UserName).SingleAsync();
            if (!account.IsDeleted) return $"Account {accountName} is already restored.";

            account.IsDeleted = false;
            account.DeletedBy = null;
            await storeContext.SaveChangesAsync();
            return $"Account {accountName} has been successfully restored.";
        }
         
    }
}