using BethanysPieShopHRM.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BethanyStore.Libraray;
public partial class MapComponenet
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;
    [Parameter]
    public double Zoom { get; set; }

    private string ElementId = $"map-{Guid.NewGuid():D}";

    [Parameter]
    public List<Marker> Markers { get; set; } = new List<Marker>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Markers.Any())
        {
            await JSRuntime.InvokeVoidAsync(
            "deliveryMap.showOrUpdate",
             ElementId,
             Markers);
        }
    }

}

