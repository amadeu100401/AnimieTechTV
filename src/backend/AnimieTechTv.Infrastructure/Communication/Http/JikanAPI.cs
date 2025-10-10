using AnimieTechTv.Domain.Communication.Http;
using AnimieTechTv.Domain.DTOs.AnimieApi;
using Flurl;
using Flurl.Http;
using System.Text.Json;

namespace AnimieTechTv.Infrastructure.Communication.Http;

public class JikanAPI : BaseURLs, IJikanAPI
{
    public async Task<HttpGetAnimieResponseDTO> GetAllAnimiePaginaded(IDictionary<string, int> pagination)
    {
        var url = BaseURLs.JIKAN_BASE_URL + "/anime";

        var queryParams = pagination.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);

        var fullUrl = url.SetQueryParams(queryParams);

        HttpGetAnimieResponseDTO response = await GetAnimieHttpRequest(fullUrl);

        return response!;
    }

    private static async Task<HttpGetAnimieResponseDTO> GetAnimieHttpRequest(string url)
    {
        var jsonString = await url.GetStringAsync();

        HttpGetAnimieResponseDTO response = DeserializeAnimieResponse(jsonString);
        return response;
    }

    private static HttpGetAnimieResponseDTO DeserializeAnimieResponse(string jsonString)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var response = JsonSerializer.Deserialize<HttpGetAnimieResponseDTO>(jsonString, options);

        return response;
    }
}
