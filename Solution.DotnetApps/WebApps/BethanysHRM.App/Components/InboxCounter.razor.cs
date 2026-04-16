using Microsoft.AspNetCore.Components;

namespace BethanysHRM.App.Components;

public partial class InboxCounter
{
    [Inject]
    public ApplicationState? AppState { get; set; }

    protected override void OnInitialized()
    {
        var radom = new Random();
        AppState.NumberOfMessages = radom.Next(0, 10);
    }
}
