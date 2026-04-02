namespace BethanysHRM.App.Components.Widgets;

public partial class InboxWidget
{
    public int MessageCount { get; set; }

    override protected void OnInitialized()
    {
        var random = new Random();
        MessageCount = random.Next(0, 10);
    }
}
