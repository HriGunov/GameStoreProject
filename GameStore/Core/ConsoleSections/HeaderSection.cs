using System;
using System.Linq;
using GameStore.Core.Abstract;
using GameStore.Core.Exceptions;
using GameStore.Data.Models;

namespace GameStore.Core.ConsoleSections
{
    internal class HeaderSection : Section
    {
        private readonly IEngine engine;


        public HeaderSection(IEngine engine) : this(new Position(0, 0), new Position(0, Console.WindowWidth))
        {
            this.engine = engine;
        }

        public HeaderSection(Position topLeftCorner, Position bottomRight) : base(topLeftCorner, bottomRight)
        {
        }


        public override void DrawSection(IConsoleManager consoleManager)
        {
            base.DrawSection(consoleManager);
            DrawHeader(engine.CurrentUser, consoleManager);
        }

        /// <summary>
        ///     Creates a personalized message in the top left corner
        /// </summary>
        /// <param name="currentUser"></param>
        private void DrawHeader(Account currentUser, IConsoleManager consoleManager)
        {
            for (var x = 0; x < Console.WindowWidth; x++) consoleManager.SetChar(' ', 0, x);
            string topLeftMessage;
            string topRightMessage;

            if (currentUser != null && currentUser.IsDeleted)
                throw new AccountIsDeleted(
                    "Account doesn't exist.");

            if (currentUser == null || currentUser.IsGuest)
            {
                topLeftMessage = "Currenly signed in as Guest";
                topRightMessage = "Please sign in!";
            }
            else
            {
                topLeftMessage = $"Hello, {currentUser.FirstName}";
                if (currentUser.IsAdmin) topLeftMessage = topLeftMessage + "(Admin)";


                topRightMessage =
                    $"Shopping Cart({currentUser.ShoppingCart.ShoppingCartProducts.Count}) {currentUser.ShoppingCart.ShoppingCartProducts.Sum(x => x.Product.Price).ToString("0.00")} BGN";
            }


            var storeTitle = "GameStore";
            consoleManager.SetText(topLeftMessage, TopLeftCorner.Y, TopLeftCorner.X);
            consoleManager.SetText(storeTitle, 0, Console.WindowWidth / 2 - storeTitle.Length / 2);
            consoleManager.SetText(topRightMessage, TopLeftCorner.Y, Console.WindowWidth - 1 - topRightMessage.Length);
        }
    }
}