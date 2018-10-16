using System.Collections.Generic;
using GameStore.Core.Abstract;
using GameStore.Data.Models;
using GameStore.Core.ConsoleSections.Abstract;

namespace GameStore.Core.ConsoleSections.MainWindowSections
{
    internal interface IOrdersSection : ISection
    {
        Order[] OrdersInView { get; set; }

        void ChangeTitle(string newTitle);
        void PageDown();
        void PageUp();
        void UpdateOrders(IEnumerable<Order> newOrders);
    }
}