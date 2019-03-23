using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ToDoTimeKeeper.Infrastructure.Models;
using ToDoTimeKeeper.Infrastructure.ViewModels;
using ToDoTimeKeeper.Models;

namespace ToDoTimeKeeper.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "ToDoTimeKeeper";
            
            this.GotoSelectTodoPage = new DelegateCommand(() =>
            {
                NavigationService.NavigateAsync("SelectTodoPage");
            });
        }

        public ICommand GotoSelectTodoPage { get; }
    }
}
