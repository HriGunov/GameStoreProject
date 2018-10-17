using System.Collections.Generic;
using GameStore.Commands.Abstract;
using GameStore.Core.Abstract;
using GameStore.Core.ConsoleSections.MainWindowSections.Abstract;
using GameStore.Exceptions;
using GameStore.Services.Abstract;

namespace GameStore.Commands
{
    internal class ViewOrdersCommand : ICommand
    {
        private readonly IConsoleManager consoleManager;
        private readonly IEngine engine;
        private readonly IOrderService orderService;
        private readonly IOrdersSection ordersSection;

        public ViewOrdersCommand(IEngine engine, IOrdersSection ordersSection, IConsoleManager consoleManager,
            IOrderService orderService)
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
                
                if (parameters.Count >= 1)
                {
                    ordersSection.SetPageTo(int.Parse(parameters[0]));
                }
                return "Viewing Orders";
            }
            catch (UserException e)
            {
                return e.Message;
            }
        }
    }
}