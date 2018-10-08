using GameStore.Data.Models;

namespace GameStore.Core.Abstract
{
    public interface IEngine
    {
        void Run();
        Account CurrentUser { get; set; }
    }
}