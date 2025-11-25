using System;
using System.Collections.Generic;
using System.Text;
using UserManagerMAUIApp.Models;

namespace UserManagerMAUIApp.Services
{
    public class UserService : IUserService
    {
        // In-memory storage for user (simulating a database)
        private readonly List<User> _users;

        public UserService()
        {
            // Initialize with siple data
            _users = new List<User>()
            {
              new User(1, "Emilio Barrera","emilio@gmail.com","Administrator",true ),
              new User(2, "Jane Smith", "jane.smith@company.com", "Manager", true),
              new User(3, "Bob Johnson", "bob.johnson@company.com", "Developer", true),
              new User(4, "Alice Williams", "alice.w@company.com", "Designer", true),
              new User(5, "Charlie Brown", "charlie.b@company.com", "Developer", false),
              new User(6, "Diana Prince", "diana.p@company.com", "Manager", true),
              new User(7, "Ethan Hunt", "ethan.h@company.com", "Security", true),
              new User(8, "Fiona Green", "fiona.g@company.com", "HR", true)
            };
        }

        public async Task<List<User>> GetUserAsync()
        {
            // Simulate network delay
            await Task.Delay(1000);

            return new List<User>(_users);
        }

        public async Task AddUserAsync(User user)
        {
            await Task.Delay(500);

            _users.Add(user);
        }

        public async Task RemoveUserAsync(User user)
        {
            await Task.Delay(500);

            _users.Remove(user);
        }
    }
}