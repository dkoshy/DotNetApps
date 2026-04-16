using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Components.Widgets;

public partial class InboxWidget
{
    [Inject]
    public ApplicationState? AppState { get; set; }
    public int MessageCount { get; set; }

    override protected void OnInitialized()
    {
       MessageCount = AppState?.NumberOfMessages ?? 0;
    }
}
