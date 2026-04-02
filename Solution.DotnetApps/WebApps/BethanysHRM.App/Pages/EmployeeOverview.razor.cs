using BethanysHRM.App.MockData;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysHRM.App.Pages;

public partial class EmployeeOverview
{
    public List<Employee> Employees { get; set; } = default!;
    private Employee? _selectedEmployee;
    public string Title { get; set; } = "Employee Overview";

    protected override void OnInitialized()
    {
        Employees = MockDataService.Employees;
    }

    public void ShowQuickView(Employee employee)
    {
        _selectedEmployee = employee;
    }
}
