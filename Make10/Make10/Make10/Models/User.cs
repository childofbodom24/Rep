using System;
using System.Collections.Generic;
using System.Text;

namespace Make10.Models
{
    public class User : BindableBaseEx
    {
        private string name;
        private int handicap;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        
        public int Handicap
        {
            get { return handicap; }
            set { SetProperty(ref handicap, value); }
        }

        public void CopyTo(User user)
        {
            user.Name = this.name;
            user.Handicap = this.handicap;
        }

        public User Clone()
        {
            var clone = new User();
            this.CopyTo(clone);
            return clone;
        }
    }
}
