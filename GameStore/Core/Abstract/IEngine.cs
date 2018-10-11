using GameStore.Data.Models;

namespace GameStore.Core.Abstract
{
    public interface IEngine
    {
        Account CurrentUser { get; set; }
        void Run();
    }
}