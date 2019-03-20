using Prism;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoTimeKeeper.Infrastructure.Models;

namespace ToDoTimeKeeper.Models
{
    public class UserTabData : BindableBaseEx, IActiveAware
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;
    }
}
