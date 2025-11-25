using System;
using System.Collections.Generic;
using System.Text;
using UserManagerMAUIApp.Models;

namespace UserManagerMAUIApp.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUserAsync();

        Task AddUserAsync(User user);

        Task RemoveUserAsync(User user);
    }
}