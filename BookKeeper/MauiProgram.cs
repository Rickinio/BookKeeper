using ZXing.Net.Maui.Controls;

namespace BookKeeper;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseBarcodeReader()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("FontAwesome6FreeBrands.otf", "FAB");
				fonts.AddFont("FontAwesome6FreeRegular.otf", "FAR");
				fonts.AddFont("FontAwesome6FreeSolid.otf", "FAS");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddTransient<SampleDataService>();
        builder.Services.AddTransient<BookKeeperDataService>();
        builder.Services.AddTransient<BookDetailsViewModel>();
		builder.Services.AddTransient<BookDetailsPage>();

		builder.Services.AddSingleton<BookListViewModel>();

		builder.Services.AddSingleton<BookListPage>();

		return builder.Build();
	}
}
