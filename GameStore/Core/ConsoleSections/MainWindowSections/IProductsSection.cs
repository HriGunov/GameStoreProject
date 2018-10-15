using System.Collections.Generic;
using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Data.Models;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    public interface IProductsSection : ISection
    {
        void PageDown();
        void PageUp();
        void UpdateProducts(IEnumerable<Product> newProducts);
        void ChangeTitle(string newTitle);
    }
}