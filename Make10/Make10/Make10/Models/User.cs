using System;
using System.Collections.Generic;
using System.Text;

namespace Make10.Models
{
    public class User : BindableBaseEx
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
    }
}
