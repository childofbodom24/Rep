using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand navigate;
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public DelegateCommand Navigate =>
            navigate ?? (navigate = new DelegateCommand(ExecuteNavigateAsync));

        async void ExecuteNavigateAsync()
        {
            await this.NavigationService.NavigateAsync("NavigationPage/SubPage");
        }
        
    }
}
