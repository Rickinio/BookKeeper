using ZXing.Net.Maui;

namespace BookKeeper.Views;

public partial class MainPage : ContentPage
{
    const string NON_AVAILABLE = "N/A";
    private BookKeeperDataService _bookKeeperDataService;

    public MainPage(MainViewModel viewModel, BookKeeperDataService bookKeeperDataService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _bookKeeperDataService = bookKeeperDataService;
        BarcodeLbl.Text = NON_AVAILABLE;
        barcodeView.Options = new BarcodeReaderOptions()
        {
            AutoRotate = true,
            Multiple = true,
            TryHarder = true,
            TryInverted = true,
        };
        barcodeView.IsDetecting = true;
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            BarcodeLbl.Text = e.Results.FirstOrDefault()?.Value;
            barcodeView.IsDetecting = false;
        });
    }

    private async void SubmitBtn_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(BarcodeLbl.Text) && int.TryParse(BoxEntry.Text, out var box))
        {
            await _bookKeeperDataService.SaveItemAsync(new BookItem(BarcodeLbl.Text, box));
        }

       BarcodeLbl.Text = NON_AVAILABLE;
    }

    private void ScanBtn_Clicked(object sender, EventArgs e)
    {
        BarcodeLbl.Text = NON_AVAILABLE;
        barcodeView.IsDetecting = true;
    }

    protected override void OnAppearing() {
        base.OnAppearing();
        barcodeView.CameraLocation = CameraLocation.Front;
        barcodeView.CameraLocation = CameraLocation.Rear; 
    }
}
