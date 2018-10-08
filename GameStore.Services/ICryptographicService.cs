namespace GameStore.Services
{
    public interface ICryptographicService
    {
        string ComputeHash(string stringToHash);
    }
}