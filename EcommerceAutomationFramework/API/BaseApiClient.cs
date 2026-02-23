using RestSharp;

namespace EcommerceAutomationFramework.API;

public abstract class BaseApiClient
{
    private readonly RestClient _client;

    protected BaseApiClient(string baseUrl)
    {
        _client = new RestClient(new RestClientOptions(baseUrl));
    }

    protected Task<RestResponse> ExecuteAsync(RestRequest request)
    {
        return _client.ExecuteAsync(request);
    }
}
