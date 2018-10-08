namespace GameStore.Services.Abstract
{
    public interface ICryptographicService
    {
        string ComputeHash(string stringToHash);
    }
}