using Make10.Interfaces;
using Make10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Make10.Services
{
    class UserService : IUserService
    {
        public UserService()
        {
            this.Users.Add(new User() { Name = "TAIGA" });
            this.Users.Add(new User() { Name = "YUTAKA" });
        }

        public User PlayingUser { get; set; }

        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();
    }
}
