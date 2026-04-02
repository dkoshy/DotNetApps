using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Components;

public partial class ProfilePicture
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
