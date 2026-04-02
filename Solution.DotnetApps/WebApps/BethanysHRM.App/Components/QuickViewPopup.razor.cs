using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Components;

public partial class QuickViewPopup
{
    [Parameter]
    public Employee? Employee { get; set; }

    private Employee? _employee;

    override protected void OnParametersSet()
    {
       _employee = Employee;
    }

    public void CloseTheQuickView()
    { 
        _employee = null;
    }


}
