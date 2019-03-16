using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Make10.Models
{
    public class User : BindableBaseEx
    {
        private string name;
        private int handicap;
        private string imageFilePath;

        public User(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
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
        
        public string ImageFilePath
        {
            get
            {
                return imageFilePath;
            }

            set
            {
                if (SetProperty(ref imageFilePath, value))
                {
                    this.RaisePropertyChanged(() => this.Image);
                }
            }
        }

        [JsonIgnore]
        public ImageSource Image
        {
            get { return ImageSource.FromFile(this.imageFilePath); }
        }

        public void CopyTo(User user)
        {
            user.Id = this.Id;
            user.Name = this.name;
            user.Handicap = this.handicap;
            user.ImageFilePath = this.imageFilePath;
        }

        public User Clone()
        {
            var clone = new User(this.Id);
            this.CopyTo(clone);
            return clone;
        }
    }
}
