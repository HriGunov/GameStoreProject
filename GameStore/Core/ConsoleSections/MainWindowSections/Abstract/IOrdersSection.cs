using System.Collections.Generic;
using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Data.Models;

namespace GameStore.Core.ConsoleSections.MainWindowSections.Abstract
{
    internal interface IOrdersSection : ISection
    {
        Order[] OrdersInView { get; set; }

        void ChangeTitle(string newTitle);
        void SetPageTo(int pageNumber);

        void PageDown();
        void PageUp();
        void UpdateOrders(IEnumerable<Order> newOrders);
    }
}