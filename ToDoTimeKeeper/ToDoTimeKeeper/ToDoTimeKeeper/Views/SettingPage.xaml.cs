using ToDoTimeKeeper.Infrastructure.Extensions;
using ToDoTimeKeeper.ViewModels;
using Xamarin.Forms;

namespace ToDoTimeKeeper.Views
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            this.SubscribeDisplayAlertMessage<SettingPageViewModel>((s, e) => this.DisplayAlert(e));
        }
    }
}
