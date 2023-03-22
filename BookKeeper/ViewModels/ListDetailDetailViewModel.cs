namespace BookKeeper.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailViewModel : BaseViewModel
{
    readonly BookKeeperDataService dataService;

    [ObservableProperty]
	BookItem item;

	public ListDetailDetailViewModel(BookKeeperDataService bookKeeperDataService) 
    {
        dataService= bookKeeperDataService;
    }

    [RelayCommand]
    private async void OnSaving()
    {
        await this.dataService.SaveItemAsync(Item);
        await Shell.Current.GoToAsync(nameof(ListDetailPage), true);
    }
}
