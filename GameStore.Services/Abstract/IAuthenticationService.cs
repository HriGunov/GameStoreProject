using GameStore.Data.Models;

namespace GameStore.Services.Abstract
{
    public interface IAuthenticationService
    {
        Account Authenticate(string username, string password);
    }
}
