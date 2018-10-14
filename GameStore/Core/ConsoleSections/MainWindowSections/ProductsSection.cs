using GameStore.Core.Abstract;
using GameStore.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    class ProductsSection : FramedSection
    {
        private readonly IEngine engine;
        private int currentPageNumber = 1;
        private int totalPages = 1;

        private Product[] productsInView;

        public ProductsSection(IEngine engine) : this(engine,new Position(1, 36), new Position(28, 119))
        {

        }
        public ProductsSection(IEngine engine, Position topLeftCorner, Position bottomright, string title = "") : base(topLeftCorner, bottomright, title)
        {
            this.engine = engine;
            productsInView = new Product[10];
        }

        public Product[] ProductsInView { get => productsInView; set => productsInView = value; }
        public void UpdateProducts( IEnumerable<Product> newProducts)
        {
            productsInView = newProducts.ToArray();
            currentPageNumber = 1;
            totalPages = (productsInView.Length / 11) + 1;
        }

        public override void DrawSection(IConsoleManager consoleManager)
        {
            if (Render)
            {
                title = "Products";
                base.DrawSection(consoleManager);
                DrawBorders(consoleManager);
                DrawProducts(consoleManager);
                DrawTitle(consoleManager);
            }
        }

        private void DrawBorders(IConsoleManager consoleManager)
        {
            int height = BottomRightCorner.Y - TopLeftCorner.Y;
            int width = BottomRightCorner.X - TopLeftCorner.X;
            var firstAccountPosition = new Position(TopLeftCorner.Y + 3, TopLeftCorner.X + 1);


            consoleManager.SetChar('╠', TopLeftCorner.Y + 2, TopLeftCorner.X);
            consoleManager.SetChar('╣', TopLeftCorner.Y + 2, BottomRightCorner.X);
            //Name
            for (int i = 0; i < 40; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 1 + i);

                for (int j = 0; j < 12; j++)
                {
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 1 + i);
                }

            }
            consoleManager.SetChar('╦', TopLeftCorner.Y, TopLeftCorner.X + 41);
            consoleManager.SetChar('║', TopLeftCorner.Y + 1, TopLeftCorner.X + 41);
            consoleManager.SetChar('╬', TopLeftCorner.Y + 2, TopLeftCorner.X + 41);
            consoleManager.SetChar('╩', BottomRightCorner.Y, TopLeftCorner.X + 41);

            var column1Title = "Name";
            consoleManager.SetText(column1Title, TopLeftCorner.Y + 1, TopLeftCorner.X + 41 / 2 - column1Title.Length / 2);


            //Comments
            var column2Width = 10;
            for (int i = 0; i < column2Width; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 42 + i);

                for (int j = 0; j < 12; j++)
                {
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 42 + i);
                }
            }
            consoleManager.SetChar('╦', TopLeftCorner.Y, TopLeftCorner.X + 52);
            consoleManager.SetChar('║', TopLeftCorner.Y + 1, TopLeftCorner.X + 52);
            consoleManager.SetChar('╬', TopLeftCorner.Y + 2, TopLeftCorner.X + 52);
            consoleManager.SetChar('╩', BottomRightCorner.Y, TopLeftCorner.X + 52);

            var column2Title = "Comments";
            consoleManager.SetText(column2Title, TopLeftCorner.Y + 1, TopLeftCorner.X + 41 + column2Width / 2 - column2Title.Length / 2 + 1);

            //Rating/Genre
            var column3Width = 11;

            for (int i = 0; i < column3Width; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 53 + i);
                for (int j = 0; j < 12; j++)
                {
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 53 + i);
                }
            }
            consoleManager.SetChar('╦', TopLeftCorner.Y, TopLeftCorner.X + 64);
            consoleManager.SetChar('║', TopLeftCorner.Y + 1, TopLeftCorner.X + 64);
            consoleManager.SetChar('╬', TopLeftCorner.Y + 2, TopLeftCorner.X + 64);
            consoleManager.SetChar('╩', BottomRightCorner.Y, TopLeftCorner.X + 64);

            var column3Title = "Genre";
            consoleManager.SetText(column3Title, TopLeftCorner.Y + 1, TopLeftCorner.X + 53 + column3Width / 2 - column3Title.Length / 2);


            //Price
            var column4Width = 18;
            for (int i = 0; i < column4Width; i++)
            {
                consoleManager.SetChar('═', TopLeftCorner.Y + 2, TopLeftCorner.X + 65 + i);
                for (int j = 0; j < 12; j++)
                {
                    consoleManager.SetChar('─', TopLeftCorner.Y + 4 + 2 * j, TopLeftCorner.X + 65 + i);
                }
            }

            var column4Title = "Price (BGN)";
            //consoleManager.SetText(column4Title, TopLeftCorner.Y + 1, TopLeftCorner.X + 84 / 2 - column4Title.Length / 2);
            consoleManager.SetText(column4Title, TopLeftCorner.Y + 1, TopLeftCorner.X + 65 + column4Width / 2 - column4Title.Length / 2);



            //Column borders
            for (int i = 0; i < height - 3; i++)
            {
                consoleManager.SetChar('║', TopLeftCorner.Y + 3 + i, TopLeftCorner.X + 41);
                consoleManager.SetChar('║', TopLeftCorner.Y + 3 + i, TopLeftCorner.X + 52);
                consoleManager.SetChar('║', TopLeftCorner.Y + 3 + i, TopLeftCorner.X + 64);


            }
        }

        private void DrawProducts(IConsoleManager consoleManager)
        {
            var currentProducts = productsInView.Skip((currentPageNumber-1)*11).Take(11).ToArray();
            var startingPosition = new Position(TopLeftCorner.Y + 3, TopLeftCorner.X + 1);
            for (int i = 0; i < currentProducts.Length; i++)
            {
                 
                //name
                consoleManager.SetText(currentProducts[i].Name, startingPosition.Y + 2 * i, startingPosition.X);
                
                consoleManager.SetText(currentProducts[i].Comments.Count.ToString(), startingPosition.Y + 2 * i, startingPosition.X+41);

                if (currentProducts[i].Genre.FirstOrDefault() != null)
                {
                    consoleManager.SetText(currentProducts[i].Genre.FirstOrDefault().Name, startingPosition.Y + 2 * i, startingPosition.X + 52);
                }
                else
                {
                    consoleManager.SetText("Unknown", startingPosition.Y + 2 * i, startingPosition.X + 52);
                }

                consoleManager.SetText(currentProducts[i].Price.ToString("0.00").PadLeft(18), startingPosition.Y + 2 * i, startingPosition.X + 64);



            }
            var pagesIndicatorString = $"Pages {currentPageNumber.ToString()}/{totalPages.ToString()}";
            var width = BottomRightCorner.X - TopLeftCorner.X;

            var startPagesIndicatorPosition = ((width) / 2) - pagesIndicatorString.Length / 2;
            consoleManager.SetText(pagesIndicatorString, BottomRightCorner.Y, TopLeftCorner.X + startPagesIndicatorPosition + 1);
        }

        public void PageUp()
        {
            currentPageNumber++;
        }
        public void PageDown()
        {
            currentPageNumber++;
        }
    }
}
