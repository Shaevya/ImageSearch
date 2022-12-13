namespace SharedUtility
{
    public interface ICustomHttpClientFactory
    {
        HttpClient Create();
    }
}