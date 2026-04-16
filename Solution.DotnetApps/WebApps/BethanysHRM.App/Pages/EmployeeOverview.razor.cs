using BethanysHRM.App.Services.Contracts;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Pages;

public partial class EmployeeOverview
{
    [Inject]
    public IEmployeeService? Employeeservice { get; set; }
    public List<Employee> Employees { get; set; } = default!;
    private Employee? _selectedEmployee;
    public string Title { get; set; } = "Employee Overview";

    protected override async Task OnInitializedAsync()
    {
        var data = await Employeeservice!.GetAllEmployees();
        Employees = data?.ToList() ?? default!;
    }

    public void ShowQuickView(Employee employee)
    {
        _selectedEmployee = employee;
    }
}
