using Microsoft.Extensions.Logging;

namespace YksiloProjekti1
{
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
                    fonts.AddFont("TT Phobos Trial Regular.ttf", "TTPhobosTrialRegular");
                    fonts.AddFont("TT Phobos Trial Bold.ttf", "TTPhobosTrialBold");
                });


    		builder.Logging.AddDebug();


            return builder.Build();
        }
    }
}
