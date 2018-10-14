using System.Collections.Generic;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Data.Models;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    interface IProductsSection :ISection
    {
        void DrawSection(IConsoleManager consoleManager);
        void PageDown();
        void PageUp();
        void UpdateProducts(IEnumerable<Product> newProducts);
    }
}