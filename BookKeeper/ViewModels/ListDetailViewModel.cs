namespace BookKeeper.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
	readonly BookKeeperDataService dataService;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	ObservableCollection<BookItem> items;

	public ListDetailViewModel(BookKeeperDataService service)
	{
		dataService = service;
	}

	[RelayCommand]
	private async void OnRefreshing()
	{
		IsRefreshing = true;

		try
		{
			await LoadDataAsync();
		}
		finally
		{
			IsRefreshing = false;
		}
	}

	//[RelayCommand]
	//public async Task LoadMore()
	//{
	//	var items = await dataService.GetItems();

	//	foreach (var item in items)
	//	{
	//		Items.Add(item);
	//	}
	//}

	public async Task LoadDataAsync()
	{
		Items = new ObservableCollection<BookItem>(await dataService.GetItemsAsync());
	}

	[RelayCommand]
	private async void GoToDetails(BookItem item)
	{
		await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
		{
			{ "Item", item }
		});
	}
}
