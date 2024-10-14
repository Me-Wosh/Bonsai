using Bonsai.Models;
using Bonsai.Services;
using Microsoft.Extensions.Logging;

namespace Bonsai;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddAutoMapper(typeof(MauiProgram).Assembly);

        builder.Services.AddSingleton<AppState>();

        builder.Services.AddSingleton<IRepository<ToDo>, ToDoRepository>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<ILocationService, LocationService>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}