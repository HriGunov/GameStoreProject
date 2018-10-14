using GameStore.Data.Context.Abstract;
using GameStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

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
