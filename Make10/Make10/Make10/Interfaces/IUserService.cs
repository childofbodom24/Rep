using Make10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Make10.Interfaces
{
    public interface IUserService
    {
        User PlayingUser { get; set; }
        ObservableCollection<User> Users { get; }
    }
}
