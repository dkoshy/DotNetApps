using BethanysHRM.App.Services.Contracts;
using BethanysPieShopHRM.Shared.Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace BethanysHRM.App.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<Employee>?> GetAllEmployees()
    {
        var dataStream = await _httpClient.GetStreamAsync("api/employee");
        var data = await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(dataStream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return data;
    }

    public async Task<Employee?> GetEmployeeDetails(int EmployeeId)
    {
        var data = await _httpClient.GetStreamAsync($"api/employee/{EmployeeId}");
        return await JsonSerializer.DeserializeAsync<Employee>(data, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        employee.BirthDate = employee.BirthDate.ToUniversalTime();
        employee.JoinedDate = employee.JoinedDate?.ToUniversalTime();
        employee.ExitDate = employee.ExitDate?.ToUniversalTime();
        var resposne = await _httpClient.PostAsJsonAsync("api/employee", employee);
        resposne.EnsureSuccessStatusCode();
        return await resposne.Content.ReadFromJsonAsync<Employee>() ?? new Employee();
    }

    public async Task DeleteEmployee(int EmployeeId)
    {
        var response = await _httpClient.DeleteAsync("api/employee/EmployeeId");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateEmployee(Employee employee)
    {
        employee.JoinedDate = employee.JoinedDate?.ToUniversalTime();
        employee.ExitDate = employee.ExitDate?.ToUniversalTime();
        employee.BirthDate = employee.BirthDate.ToUniversalTime();
        var resposne = await _httpClient.PutAsJsonAsync("api/employee", employee);
        resposne.EnsureSuccessStatusCode();
    }
}
