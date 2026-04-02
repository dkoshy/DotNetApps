using BethanysHRM.App.MockData;
namespace BethanysHRM.App.Components.Widgets;

public partial class EmployeeCountWidget
{
   public int EmployeeCounter { get; set; }

    override protected void OnInitialized()
    {
        EmployeeCounter = MockDataService.Employees.Count;
    }
}
