using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using UserManagerMAUIApp.Models;
using UserManagerMAUIApp.Services;

namespace UserManagerMAUIApp.ViewModels
{
    /// <summary>
    /// ViewModel for managing users with MVVM pattern
    /// </summary>
    public partial class UserViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Observable collection of users for data binding
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<User> users;

        /// <summary>
        /// New user name input
        /// </summary>
        [ObservableProperty]
        private string newUserName;

        /// <summary>
        /// New user email input
        /// </summary>
        [ObservableProperty]
        private string newUserEmail;

        /// <summary>
        /// New user role input
        /// </summary>
        [ObservableProperty]
        private string newUserRole;

        /// <summary>
        /// Shows/hides the add user form
        /// </summary>
        [ObservableProperty]
        private bool isAddUserVisible;

        /// <summary>
        /// Indicates whether data is currently loading
        /// </summary>
        [ObservableProperty]
        private bool isLoading;

        //partial void OnIsLoadingChanged(bool value)
        //{
        //    OnPropertyChanged(nameof(IsNotLoading));
        //}

        /// <summary>
        /// Indicates whether users are loaded
        /// </summary>
        [ObservableProperty]
        private bool hasUsers;

        /// <summary>
        /// Status message for user feedback
        /// </summary>
        [ObservableProperty]
        private string statusMessage;

        /// <summary>
        /// Total count of users
        /// </summary>
        [ObservableProperty]
        private int totalUsers;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public UserViewModel(IUserService userService)
        {
            _userService = userService;
            users = new ObservableCollection<User>();
            statusMessage = "Click 'Load Users' to fetch data";
            hasUsers = false;
        }

        /// <summary>
        /// Command to show the add user form
        /// </summary>
        [RelayCommand]
        private void ShowAddUserForm()
        {
            IsAddUserVisible = true;
            NewUserName = string.Empty;
            NewUserEmail = string.Empty;
            NewUserRole = string.Empty;
        }

        /// <summary>
        /// Command to cancel adding a user
        /// </summary>
        [RelayCommand]
        private void CancelAddUser()
        {
            IsAddUserVisible = false;
            NewUserName = string.Empty;
            NewUserEmail = string.Empty;
            NewUserRole = string.Empty;
        }

        /// <summary>
        /// Command to add a new user
        /// </summary>
        [RelayCommand]
        private async Task AddUserAsync()
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(NewUserName))
                {
                    StatusMessage = "Please enter a name";
                    return;
                }

                if (string.IsNullOrWhiteSpace(NewUserEmail))
                {
                    StatusMessage = "Please enter a email.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(NewUserRole))
                {
                    StatusMessage = "Please enter a role";
                    return;
                }

                IsLoading = true;
                StatusMessage = $"Adding {NewUserName}.....";

                // Generate new ID
                int newId = Users.Count > 0 ? Users.Max(u => u.id) + 1 : 1;

                // Create new user
                var newUser = new User(newId, NewUserName, NewUserEmail, NewUserRole, true);

                // Add to service
                await _userService.AddUserAsync(newUser);

                // Add to collection
                Users.Add(newUser);

                TotalUsers = Users.Count;
                HasUsers = Users.Count > 0;
                StatusMessage = $"Succesfully added {newUserName}";

                // Close form and clear input
                IsAddUserVisible = false;
                NewUserEmail = string.Empty;
                NewUserRole = string.Empty;
                NewUserRole = string.Empty;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Command to load users from the service
        /// </summary>
        [RelayCommand]
        private async Task LoadUsersAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading users.....";

                // Fetch users from service
                var userList = await _userService.GetUserAsync();

                // Clear and repopulate collection
                Users.Clear();
                foreach (var user in userList)
                {
                    Users.Add(user);
                }

                TotalUsers = Users.Count;
                HasUsers = Users.Count > 0;
                StatusMessage = $"Successfully loaded {TotalUsers} users";
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

        /// <summary>
        /// Command to refresh users
        /// </summary>
        [RelayCommand]
        private async Task RefreshUsersAsync()
        {
            await LoadUsersAsync();
        }

        /// <summary>
        /// Command to remove a user
        /// </summary>
        [RelayCommand]
        private async Task RemoveUserAsync(User user)
        {
            if (user == null)
            {
                return;
            }

            try
            {
                IsLoading = true;
                StatusMessage = $"Removing {user.Name}.....";

                await _userService.RemoveUserAsync(user);
                Users.Remove(user);

                TotalUsers = Users.Count;
                HasUsers = Users.Count > 0;
                StatusMessage = $"SUccesfully removed {user.Name}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}