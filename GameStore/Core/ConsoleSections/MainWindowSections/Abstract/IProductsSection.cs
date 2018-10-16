using System.Collections.Generic;
using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Data.Models;

namespace GameStore.Core.ConsoleSections.MainWindowSections.Abstract
{
    public interface IProductsSection : ISection
    {
        void PageDown();
        void PageUp();
        void UpdateProducts(IEnumerable<Product> newProducts);
        void ChangeTitle(string newTitle);
    }
}