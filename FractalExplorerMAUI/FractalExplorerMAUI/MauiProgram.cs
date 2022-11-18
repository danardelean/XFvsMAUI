using FractlExplorer;
using Microsoft.Extensions.Logging;

namespace FractalExplorerMAUI;

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
		var services = builder.Services;
		services.AddSingleton<FractlExplorer.Utility.IImageCreator, ImageCreator>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

