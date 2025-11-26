using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using UserManagerMAUIApp.Services;
using UserManagerMAUIApp.ViewModels;
using UserManagerMAUIApp.Views;

namespace UserManagerMAUIApp
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
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Register Services (Singleton - one instance for the entire app lifetime)
            builder.Services.AddSingleton<IUserService, UserService>();

            // Register ViewModels (Transient - new instance each time)
            builder.Services.AddTransient<UserViewModel>();

            // Register Views (Transient - new instance each time)
            builder.Services.AddTransient<UserPage>();

            return builder.Build();
        }
    }
}