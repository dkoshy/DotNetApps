using BethanysHRM.App.Services.Contracts;
using BethanysPieShopHRM.Shared.Domain;
using System.Text.Json;

namespace BethanysHRM.App.Services;

public class CountryServices : ICountryServices
{
    private readonly HttpClient _httpClient;

    public CountryServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async  Task<IEnumerable<Country>> GetCountries()
    {
        var stream = await _httpClient.GetStreamAsync("api/country");
        var data = await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return data;

    }

    public async Task<Country> GetCountryById(int id)
    {
        var response = await _httpClient.GetAsync($"api/country/{id}");
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Country>(result, new JsonSerializerOptions( JsonSerializerDefaults.Web));
        
    }
}
