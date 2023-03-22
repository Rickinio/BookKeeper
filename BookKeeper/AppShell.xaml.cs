namespace BookKeeper;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(BookDetailsPage), typeof(BookDetailsPage));
        Routing.RegisterRoute(nameof(BookListPage), typeof(BookListPage));
    }
}
