using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoTimeKeeper.Infrastructure.ViewModels;
using ToDoTimeKeeper.Models;

namespace ToDoTimeKeeper.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "XXX";
            this.UserTabDataList = new ObservableCollection<UserTabData>(new UserTabData[]
            {
                new UserTabData(){ Username = "AAA" },
                new UserTabData(){ Username = "BBB" },
                new UserTabData(){ Username = "CCC" }
            });
        }

        public ObservableCollection<UserTabData> UserTabDataList { get; set; }
    }
}
