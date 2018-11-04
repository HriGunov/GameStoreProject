using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GameStore.Data.Context;
using GameStore.Data.Models;
using GameStore.Services.Abstract;

namespace GameStore.Services
{
    public class AccountsService : IAccountsService
    {
        private const string Pattern = @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$";
        private static readonly Regex usernameFormat = new Regex(Pattern);

        private readonly GameStoreContext storeContext;

        public AccountsService(GameStoreContext storeContext)
        {
            this.storeContext = storeContext ?? throw new ArgumentNullException(nameof(storeContext));
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
        public string DeleteAccount(string accountId)
        {
            var account = storeContext.Users.Find(accountId);
            if (account == null) throw new Exception("Account not found");

            if (account.IsDeleted) return $"Account {account.UserName} was not found.";

            account.IsDeleted = true;
            account.ModifiedOn = DateTime.Now;
            storeContext.SaveChanges();
            return $"Account {account.UserName} has been successfully removed.";
        }

        /// <summary>
        ///     Adds credit card to the given user.
        /// </summary>
        /// <param name="cardNumber">Card number.</param>
        /// <param name="account">Account Type</param>
        public void AddCreditCard(string cardNumber, Account account)
        {
            var tempAccount = storeContext.Accounts.Where(a => a.UserName == account.UserName).Single();
            account.CreditCard = cardNumber;
            tempAccount.CreditCard = cardNumber;
            storeContext.SaveChanges();
        }

        /// <summary>
        ///     Restores an account by changing it's IsDeleted flag and clears the DeletedBy field.
        /// </summary>
        /// <param name="commandExecutor">The username of the command giver.</param>
        /// <param name="accountName">The username of the account to be restored</param>
        /// <returns></returns>
        public string RestoreAccount(Account commandExecutor, Account accountName)
        {
            var account = storeContext.Accounts.Where(a => a.UserName == accountName.UserName).Single();
            if (!account.IsDeleted) return $"Account {accountName} is already restored.";

            account.IsDeleted = false;
            account.DeletedBy = null;
            storeContext.SaveChanges();
            return $"Account {accountName} has been successfully restored.";
        }


        /// <summary>
        ///     Determines whether the username meets conditions.
        ///     Username conditions:
        ///     Must be 1 to 24 character in length
        ///     Must start with letter a-zA-Z
        ///     May contain letters, numbers or '.','-' or '_'
        ///     Must not end in '.','-','._' or '-_'
        /// </summary>
        /// <param name="userName">Given userName</param>
        /// <returns>True if the username is valid</returns>
        private static bool IsUserNameAllowed(string userName)
        {
            return !string.IsNullOrEmpty(userName) && usernameFormat.IsMatch(userName);
        }
    }
}