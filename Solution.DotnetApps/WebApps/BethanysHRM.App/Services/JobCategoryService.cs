using BethanysHRM.App.Services.Contracts;
using BethanysPieShopHRM.Shared.Domain;
using System.Text.Json;

namespace BethanysHRM.App.Services;

public class JobCategoryService : IJobCategoryService
{
    private readonly HttpClient _httpClient;

    public JobCategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<JobCategory>> GetJobCategories()
    {
        var stream = await _httpClient.GetStreamAsync("api/jobcategory");
        return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    public async  Task<JobCategory> GetJobCategoryById(int id)
    {
        var resposne = _httpClient.GetAsync($"api/jobcategories/{id}");
        var data = await resposne.Result.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JobCategory>(data, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
