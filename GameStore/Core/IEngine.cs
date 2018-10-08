using GameStore.Data.Models;

namespace GameStore.Core
{
    public interface IEngine
    {
        void Run();
        Account CurrentUser { get; set; }
    }
}