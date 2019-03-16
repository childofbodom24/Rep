using Make10.Interfaces;
using Make10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Make10.Services
{
    class UserService : IUserService
    {
        public UserService()
        {
            this.Users = JsonSerializer.Deserialize<ObservableCollection<User>>("Users.json");
            if (this.Users == null || this.Users.Count() == 0)
            {
                this.Users = new ObservableCollection<User>();
                this.Users.Add(new User(1) { Name = "TAIGA" });
                this.Users.Add(new User(2) { Name = "YUTAKA" });
            }
        }

        public User PlayingUser { get; set; }

        public ObservableCollection<User> Users { get; } 

        public void Save()
        {
            JsonSerializer.Serialize(this.Users, "Users.json");
        }
    }
}
