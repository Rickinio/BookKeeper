using ZXing.Net.Maui;

namespace BookKeeper.Views;

public partial class MainPage : ContentPage
{
    private bool _isDetecting;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _isDetecting = true;
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

    private void SubmitBtn_Clicked(object sender, EventArgs e)
    {
        BarcodeLbl.Text = string.Empty;
    }

    private void ScanBtn_Clicked(object sender, EventArgs e)
    {
        BarcodeLbl.Text = string.Empty;
        barcodeView.IsDetecting = true;

    }
}
