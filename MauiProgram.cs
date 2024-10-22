using Microsoft.Extensions.Logging;
using MyMauiApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;


namespace MyMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

	    builder.Services.AddSingleton<MainPage>();

		// Register HttpClient for API communication
        builder.Services.AddHttpClient();

		// Register the TaskViewModel for Dependency Injection
		builder.Services.AddSingleton<TaskViewModel>();

		return builder.Build();
	}
}
