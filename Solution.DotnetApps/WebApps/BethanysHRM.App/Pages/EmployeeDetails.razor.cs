using BethanysHRM.App.Services.Contracts;
using BethanysPieShopHRM.Shared.Domain;
using BethanysPieShopHRM.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Pages;

public partial class EmployeeDetails
{
    [Inject]
    public IEmployeeService? EmployeeService { get; set; }

    [Parameter]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = default!;
    public List<Marker> MapMarkers { get; set; } = new List<Marker>();

    protected override async  Task OnInitializedAsync()
    {
        Employee = await EmployeeService.GetEmployeeDetails(EmployeeId);
       
        if (Employee.Latitude.HasValue && Employee.Longitude.HasValue)
        {
            MapMarkers = new List<Marker>
            {
                new Marker
                {
                    Description = Employee.FirstName + " " + Employee.LastName,
                    X = Employee.Longitude.Value,
                    Y = Employee.Latitude.Value,
                    ShowPopup = false
                }
            };
        }

    }

}
