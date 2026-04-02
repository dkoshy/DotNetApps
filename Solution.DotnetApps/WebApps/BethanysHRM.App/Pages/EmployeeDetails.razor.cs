
using BethanysHRM.App.MockData;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Pages;

public partial class EmployeeDetails
{
    [Parameter]
    public int EmployeeId  { get; set; }
    public Employee Employee { get; set; } = default!;

    protected override Task OnInitializedAsync()
    {
        Employee = MockDataService.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId) ?? default! ;
        return base.OnInitializedAsync();
    }

}
