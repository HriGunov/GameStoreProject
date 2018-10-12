using System;
using System.Linq;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Core.Exceptions;

namespace GameStore.Core.ConsoleSections
{
    internal class TopLeftCornerUserSection
    {
        private readonly IConsoleManager consoleManager;

        public TopLeftCornerUserSection(IConsoleManager consoleManager, int y, int x)
        {
            this.consoleManager = consoleManager;
            PositionY = y;
            PositionX = x;
        }

        public int PositionY { get; set; }
        public int PositionX { get; set; }

        /// <summary>
        ///     Creates a personalized message in the top left corner
        /// </summary>
        /// <param name="currentUser"></param>
        public void ImprintOnConsoleMatrix(Account currentUser)
        {
            string topLeftMessage;
            string topRightMessage;

            if (currentUser != null && currentUser.IsDeleted)
                throw new AccountIsDeleted(
                    "Account doesn't exist.");

            if (currentUser == null || currentUser.IsGuest)
            {
                topLeftMessage = "Currenly signed in as Guest";
                topRightMessage = "Please sign in!";
                return;
            }
            else
            {
                topLeftMessage = $"Hello, {currentUser.FirstName}";
                if (currentUser.ShoppingCart == null)
                    topRightMessage = $"Shopping Cart(0) {0:F2} BGN";
                else
                    topRightMessage =
                        $"Shopping Cart({currentUser.ShoppingCart.ShoppingCartProducts.Count}) {string.Format("F2", currentUser.ShoppingCart.ShoppingCartProducts.Sum(x => x.Product.Price))} BGN";
            }

            if (currentUser.IsAdmin) topLeftMessage = topLeftMessage + "(Admin)";
            var storeTitle = "GameStore";
            consoleManager.SetText(topLeftMessage, PositionY, PositionX);
            consoleManager.SetText(storeTitle, 0, Console.WindowWidth / 2 - storeTitle.Length / 2);
            consoleManager.SetText(topRightMessage, PositionY, Console.WindowWidth - 1 - topRightMessage.Length);
        }
    }
}