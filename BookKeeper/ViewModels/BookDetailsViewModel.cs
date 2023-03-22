namespace BookKeeper.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class BookDetailsViewModel : BaseViewModel
{
    readonly BookKeeperDataService dataService;

    [ObservableProperty]
	BookItem item;

	public BookDetailsViewModel(BookKeeperDataService bookKeeperDataService) 
    {
        dataService= bookKeeperDataService;
    }

    [RelayCommand]
    private async void OnSaving()
    {
        await this.dataService.SaveItemAsync(Item);
        await Shell.Current.GoToAsync(nameof(BookListPage), true);
    }
}
