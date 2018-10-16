using System.Collections.Generic;
using System.Linq;
using GameStore.Core.Abstract;
using GameStore.Data.Models;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    internal class OrdersSection : FramedSection, IOrdersSection
    {
        private int currentPageNumber = 1;

        private int totalPages = 1;

        public OrdersSection() : this(new Position(1, 36), new Position(28, 119))
        {
        }

        public OrdersSection(Position topLeftCorner, Position bottomright, string title = "") : base(topLeftCorner,
            bottomright, title)
        {
            OrdersInView = new Order[10];
            title = "Your Orders";
        }

        public Order[] OrdersInView { get; set; }

        public void UpdateOrders(IEnumerable<Order> newOrders)
        {
            OrdersInView = newOrders.ToArray();
            currentPageNumber = 1;
            totalPages = OrdersInView.Length / 11 + 1;
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            if (Render)
            {
                base.DrawSection(consoleManager);
                DrawBorders(consoleManager);
                DrawOrders(consoleManager);
                DrawTitle(consoleManager);
            }
        }

        public void PageUp()
        {
            currentPageNumber++;
        }

        public void PageDown()
        {
            currentPageNumber++;
        }

        public void ChangeTitle(string newTitle)
        {
            title = newTitle;
        }

        private void DrawBorders(IConsoleManager consoleManager)
        {
            var height = BottomRightCorner.Y - TopLeftCorner.Y;
            var width = BottomRightCorner.X - TopLeftCorner.X;
            var firstAccountPosition = new Position(TopLeftCorner.Y + 3, TopLeftCorner.X + 1);


            consoleManager.SetChar('╠', TopLeftCorner.Y + 2, TopLeftCorner.X);
            consoleManager.SetChar('╣', TopLeftCorner.Y + 2, BottomRightCorner.X);

            // Order ID
            for (var i = 0; i < 10; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 1 + i);

                for (var j = 0; j < 12; j++)
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 1 + i);
            }

            consoleManager.SetChar('╦', TopLeftCorner.Y, TopLeftCorner.X + 11);
            consoleManager.SetChar('║', TopLeftCorner.Y + 1, TopLeftCorner.X + 11);
            consoleManager.SetChar('╬', TopLeftCorner.Y + 2, TopLeftCorner.X + 11);
            consoleManager.SetChar('╩', BottomRightCorner.Y, TopLeftCorner.X + 11);

            var column1Title = "Order ID";
            consoleManager.SetText(column1Title, TopLeftCorner.Y + 1,
                TopLeftCorner.X + 11 / 2 - column1Title.Length / 2);

            // Few Products from the Order
            for (var i = 0; i < 40; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 12 + i);

                for (var j = 0; j < 12; j++)
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 12 + i);
            }

            consoleManager.SetChar('╦', TopLeftCorner.Y, TopLeftCorner.X + 52);
            consoleManager.SetChar('║', TopLeftCorner.Y + 1, TopLeftCorner.X + 52);
            consoleManager.SetChar('╬', TopLeftCorner.Y + 2, TopLeftCorner.X + 52);
            consoleManager.SetChar('╩', BottomRightCorner.Y, TopLeftCorner.X + 52);

            var columnTwoTitle = "Order Items";
            consoleManager.SetText(columnTwoTitle, TopLeftCorner.Y + 1,
                TopLeftCorner.X + 52 / 2 - columnTwoTitle.Length / 2);

            //Status
            var column2Width = 10;
            for (var i = 0; i < column2Width; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 53 + i);

                for (var j = 0; j < 12; j++)
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 53 + i);
            }

            consoleManager.SetChar('╦', TopLeftCorner.Y, TopLeftCorner.X + 63);
            consoleManager.SetChar('║', TopLeftCorner.Y + 1, TopLeftCorner.X + 63);
            consoleManager.SetChar('╬', TopLeftCorner.Y + 2, TopLeftCorner.X + 63);
            consoleManager.SetChar('╩', BottomRightCorner.Y, TopLeftCorner.X + 63);

            var column2Title = "Status";
            consoleManager.SetText(column2Title, TopLeftCorner.Y + 1,
                TopLeftCorner.X + 53 + column2Width / 2 - column2Title.Length / 2 + 1);

            //Price
            var column4Width = 19;
            for (var i = 0; i < column4Width; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 64 + i);
                for (var j = 0; j < 12; j++)
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 64 + i);
            }

            var column4Title = "Paid (BGN)";
            //consoleManager.SetText(column4Title, TopLeftCorner.Y + 1, TopLeftCorner.X + 84 / 2 - column4Title.Length / 2);
            consoleManager.SetText(column4Title, TopLeftCorner.Y + 1,
                TopLeftCorner.X + 64 + column4Width / 2 - column4Title.Length / 2);


            //Column borders
            for (var i = 0; i < height - 3; i++)
            {
                consoleManager.SetChar('║', TopLeftCorner.Y + 3 + i, TopLeftCorner.X + 11);
                consoleManager.SetChar('║', TopLeftCorner.Y + 3 + i, TopLeftCorner.X + 52);
                consoleManager.SetChar('║', TopLeftCorner.Y + 3 + i, TopLeftCorner.X + 63);
            }
        }

        private void DrawOrders(IConsoleManager consoleManager)
        {
            var currentOrders = OrdersInView.Skip((currentPageNumber - 1) * 11).Take(11).ToArray();
            var startingPosition = new Position(TopLeftCorner.Y + 3, TopLeftCorner.X + 1);
            for (var i = 0; i < currentOrders.Length; i++)
            {
                // Order ID
                consoleManager.SetText(currentOrders[i].Id.ToString(), startingPosition.Y + 2 * i, startingPosition.X);

                // Few Products from the Order
                consoleManager.SetText(string.Join(", ", currentOrders[i].OrderProducts.Select(o => o.Product.Name)),
                    startingPosition.Y + 2 * i, startingPosition.X + 11);

                // Order Status
                consoleManager.SetText("Completed", startingPosition.Y + 2 * i,
                    startingPosition.X + 52);

                // Order total cost
                consoleManager.SetText(
                    currentOrders[i].OrderProducts.Sum(o => o.Product.Price).ToString("0.00").PadLeft(19),
                    startingPosition.Y + 2 * i, startingPosition.X + 63);
            }

            var pagesIndicatorString = $"Pages {currentPageNumber.ToString()}/{totalPages.ToString()}";
            var width = BottomRightCorner.X - TopLeftCorner.X;

            var startPagesIndicatorPosition = width / 2 - pagesIndicatorString.Length / 2;
            consoleManager.SetText(pagesIndicatorString, BottomRightCorner.Y,
                TopLeftCorner.X + startPagesIndicatorPosition + 1);
        }
    }
}