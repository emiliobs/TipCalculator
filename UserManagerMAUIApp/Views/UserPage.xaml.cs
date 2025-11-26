//using AndroidX.Lifecycle;
using UserManagerMAUIApp.ViewModels;

namespace UserManagerMAUIApp.Views;

public partial class UserPage : ContentPage
{
    public UserPage(UserViewModel userViewModel)
    {
        InitializeComponent();

        // Set the BindingContext via Dependency Injection
        BindingContext = userViewModel;
    }
}