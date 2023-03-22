namespace BookKeeper.Views;

public partial class BookListPage : ContentPage
{
	BookListViewModel ViewModel;

	public BookListPage(BookListViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = ViewModel = viewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		await ViewModel.LoadDataAsync();
	}
}
