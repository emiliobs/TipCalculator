using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UserManagerMAUIApp.Models;
using UserManagerMAUIApp.Services;

namespace UserManagerMAUIApp.ViewModels
{
    //ViewModel for managing user with MVVM pattern
    public partial class UserViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        // Observable collection of user for data binding
        [ObservableProperty]
        private ObservableCollection<User> users;

        //Indicate whether data id currently loading
        [ObservableProperty]
        private bool isLoading;

        // Indicate whether users are loaded
        [ObservableProperty]
        private bool hasUsers;

        [ObservableProperty]
        private string statusMessage;

        [ObservableProperty]
        private int totalUsers;

        public UserViewModel(IUserService userService)
        {
            this._userService = userService;
            users = new ObservableCollection<User>();
            statusMessage = "Click 'Load User' to fetch data ";
            hasUsers = false;
        }

        // Command to load users from the service
        [RelayCommand]
        private async Task LoadUserAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading users.....";

                var userList = await _userService.GetUserAsync();

                Users.Clear();
                foreach (var user in userList)
                {
                    users.Add(user);
                }

                TotalUsers = users.Count;
                HasUsers = users.Count > 0;
                StatusMessage = $"Succesfully loaded {TotalUsers} users.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                HasUsers = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task RefresUserAsync()
        {
            await LoadUserAsync();
        }
    }
}