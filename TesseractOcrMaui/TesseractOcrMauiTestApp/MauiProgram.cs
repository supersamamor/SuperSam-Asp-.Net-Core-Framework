﻿#if DEBUG
using Microsoft.Extensions.Logging;
#endif
using TesseractOcrMaui;

namespace TesseractOcrMauiTestApp;
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

        builder.Services.AddLogging();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTesseractOcr(
            files =>
            {       
                files.AddFile("eng.traineddata");
                files.AddFile("metron.traineddata");
            });

        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
    }
}
