using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Components;

public partial class EmployeeCard
{
    [Parameter]
    public Employee Employee { get; set; } = default!;

    [Parameter]
    public EventCallback<Employee> EmployeeQuickViewClicked { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
       //if(string.IsNullOrWhiteSpace(Employee.LastName))
       // {
       //     throw new Exception("Employee LastName is null");
       // }
    }

    public void NavigateToDetails(int employeeId)
    {
        NavigationManager.NavigateTo($"/employeedetails/{employeeId}");
    }

}
