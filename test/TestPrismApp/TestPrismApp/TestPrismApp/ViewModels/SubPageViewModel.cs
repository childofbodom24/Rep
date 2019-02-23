using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestPrismApp.ViewModels
{
    public class SubPageViewModel : ViewModelBase
    {
        public SubPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Sub Page";
        }
    }
}
