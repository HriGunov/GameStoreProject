using GameStore.Data.Models;
using System;
using System.Linq;

namespace GameStore.Core.ConsoleSections
{
    class TopLeftCornerUserSection
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
        /// Creates a personalized message in the top left corner
        /// </summary>
        /// <param name="currentUser"></param>
        public void ImprintOnConsoleMatrix(Account currentUser)
        {
            string topLeftMessage;
            string topRightMessage;


            if (currentUser.IsDeleted)
            {
                throw new ArgumentException("A deleted account cannot be logged in. So there cannot be a message for it.");
            }

            if (currentUser.IsGuest)
            {
                topLeftMessage = $"Currenly signed in as Guest";
                topRightMessage = "Please sign in!";
            }
            else
            {
                topLeftMessage = $"Hello, {currentUser.FirstName}";
                if (currentUser.ShoppingCart == null)
                {
                    topRightMessage = $"Shopping Cart(0) {0:F2} BGN";

                }
                else
                {
                    topRightMessage = $"Shopping Cart({currentUser.ShoppingCart.ShoppingCartProducts.Count}) {String.Format("F2", currentUser.ShoppingCart.ShoppingCartProducts.Sum(x => x.Product.Price))} BGN";

                }

            }

            if (currentUser.IsAdmin)
            {
                topLeftMessage = topLeftMessage + "(Admin)";
            }
            var storeTitle = "GameStore";
            consoleManager.SetText(topLeftMessage, PositionY, PositionX);
            consoleManager.SetText(storeTitle, 0, Console.WindowWidth / 2 - storeTitle.Length / 2);
            consoleManager.SetText(topRightMessage, PositionY, Console.WindowWidth - 1 - topRightMessage.Length);
        }
    }
}
