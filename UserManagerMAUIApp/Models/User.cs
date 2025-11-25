using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagerMAUIApp.Models
{
    public class User
    {
        public int id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public bool IsActive { get; set; }

        public User(int id, string? name, string? email, string? role, bool isActive)
        {
            this.id = id;
            Name = name;
            Email = email;
            Role = role;
            IsActive = isActive;
        }
    }
}