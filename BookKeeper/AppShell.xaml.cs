namespace BookKeeper;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ListDetailDetailPage), typeof(ListDetailDetailPage));
        Routing.RegisterRoute(nameof(ListDetailPage), typeof(ListDetailPage));
    }
}
