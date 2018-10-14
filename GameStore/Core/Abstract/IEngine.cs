using GameStore.Core.ConsoleSections.Abstract;
using GameStore.Data.Models;

namespace GameStore.Core.Abstract
{
    public interface IEngine
    {
        Account CurrentUser { get; set; }
        ISection MainSection { get; set; }
        ISection DefaultMainSection { get; set; }
        void Run();
    }
}