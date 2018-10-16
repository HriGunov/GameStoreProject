using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections;
using GameStore.Exceptions;
using System.Collections.Generic;
using System.Linq;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    class ViewOrdersCommand : ICommand
    {
        private readonly IEngine engine;
        private readonly IOrdersSection ordersSection;
        private readonly IConsoleManager consoleManager;
        private readonly IOrderService orderService;

        public ViewOrdersCommand(IEngine engine, IOrdersSection ordersSection, IConsoleManager consoleManager, IOrderService orderService)
        {
            this.engine = engine;
            this.ordersSection = ordersSection;
            this.consoleManager = consoleManager;
            this.orderService = orderService;
        }

        public string Execute(List<string> parameters)
        {
            try
            {
                if (engine.CurrentUser == null || engine.CurrentUser.IsGuest)
                    return "You need to be logged in to add view your orders.";

                engine.MainSection = ordersSection;
                ordersSection.ChangeTitle("Your Orders");

                ordersSection.UpdateOrders(orderService.FindOrders(engine.CurrentUser));
                return "Viewing Orders";
            }
            catch (UserException e)
            {
                return e.Message;
            }
        }
    }
}