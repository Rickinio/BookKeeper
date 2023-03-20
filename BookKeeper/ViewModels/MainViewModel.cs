using ZXing.Net.Maui;

namespace BookKeeper.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [RelayCommand]
    public async Task BarcodesDetectedCommand()
    {
        //var a = eventArgs;
    }
}
