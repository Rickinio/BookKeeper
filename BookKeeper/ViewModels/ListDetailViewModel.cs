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
	private async Task OnRefreshing()
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

    [RelayCommand]
    private async void OnAddNewItem()
	{
        await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
        {
            { "Item", new BookItem() }
        });
    }

	[RelayCommand]
	private async void OnDelete(BookItem item)
	{
        await dataService.DeleteItemAsync(item);
		await OnRefreshing();

    }

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
