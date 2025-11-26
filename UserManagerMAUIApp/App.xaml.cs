using Microsoft.Extensions.DependencyInjection;
using UserManagerMAUIApp.Views;

namespace UserManagerMAUIApp
{
    public partial class App : Application
    {
        public App(UserPage userPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(userPage);
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    //return new Window(new AppShell());
        //    MainPage = new NavigationPage(UserPage);
        //}
    }
}