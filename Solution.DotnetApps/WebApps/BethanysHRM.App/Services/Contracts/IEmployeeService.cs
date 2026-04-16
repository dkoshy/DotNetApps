using BethanysPieShopHRM.Shared.Domain;

namespace BethanysHRM.App.Services.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>?> GetAllEmployees();
    Task<Employee?> GetEmployeeDetails(int EmployeeId);
    Task<Employee> AddEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(int EmployeeId);
}
