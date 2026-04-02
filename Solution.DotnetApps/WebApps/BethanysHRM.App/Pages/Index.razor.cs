using BethanysHRM.App.Components.Widgets;

namespace BethanysHRM.App.Pages;

public partial class Index
{
    public List<Type> WidgetTypes { get; } = new List<Type>
    {
        typeof(EmployeeCountWidget),
        typeof(InboxWidget)
    };
}
