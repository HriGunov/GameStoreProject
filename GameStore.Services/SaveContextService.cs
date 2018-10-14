using GameStore.Data.Context.Abstract;
using GameStore.Services.Abstract;

namespace GameStore.Services
{
    public class SaveContextService : ISaveContextService
    {
        private readonly IGameStoreContext storeContext;

        public SaveContextService(IGameStoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        public void SaveChanges()
        {
            storeContext.SaveChanges();
        }
    }
}