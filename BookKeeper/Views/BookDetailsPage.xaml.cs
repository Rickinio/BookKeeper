namespace BookKeeper.Views;

public partial class BookDetailsPage : ContentPage
{
	public BookDetailsPage(BookDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
