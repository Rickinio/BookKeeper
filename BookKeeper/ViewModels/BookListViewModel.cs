using CommunityToolkit.Maui.Storage;
using System.Text;

namespace BookKeeper.ViewModels;

public partial class BookListViewModel : BaseViewModel
{
	readonly BookKeeperDataService dataService;

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	ObservableCollection<BookItem> items;

	public BookListViewModel(BookKeeperDataService service)
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
        await Shell.Current.GoToAsync(nameof(BookDetailsPage), true, new Dictionary<string, object>
        {
            { "Item", new BookItem() }
        });
    }

    [RelayCommand]
    private async void OnExport()
    {
        var data = await dataService.GetItemsAsync();
		var stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"{nameof(BookItem.Id)};{nameof(BookItem.ISBN)};{nameof(BookItem.Box)}");
		foreach( var item in data)
		{
			stringBuilder.AppendLine($"{item.Id};{item.ISBN};{item.Box}");
		}

		var folderResult = await FolderPicker.Default.PickAsync(new CancellationTokenSource().Token);

		if (folderResult.IsSuccessful)
		{
            await WriteTextToFile(stringBuilder.ToString(), folderResult.Folder.Path);
        }
		else
		{
			await Application.Current.MainPage.DisplayAlert("Error", folderResult?.Exception?.Message ?? "Something went wrong and we can't save the csv file.", "Ok");
        }
    }

    public async Task WriteTextToFile(string text, string path)
    {
        // Write the file content to the app data directory  
        string targetFile = System.IO.Path.Combine(path, "data.csv");
        using FileStream outputStream = System.IO.File.OpenWrite(targetFile);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(text);
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
		await Shell.Current.GoToAsync(nameof(BookDetailsPage), true, new Dictionary<string, object>
		{
			{ "Item", item }
		});
	}
}
