using Make10.Extensions;
using Make10.Models;
using Make10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Make10.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.SubscribeDisplayAlertMessage<MainPageViewModel>((s,e)=>this.DisplayAlert(e));
        }
    }
}