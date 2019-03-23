using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ToDoTimeKeeper.Infrastructure.Interfaces;
using ToDoTimeKeeper.Infrastructure.ViewModels;
using ToDoTimeKeeper.Models;
using ToDoTimeKeeper.Services;

namespace ToDoTimeKeeper.ViewModels
{
	public class SelectTodoPageViewModel : ViewModelBase
    {
        private SettingService settingService;
        public SelectTodoPageViewModel(INavigationService navigationService, ISettingService settingService) : base(navigationService)
        {
            this.Title = "設定";
            this.settingService = settingService as SettingService;

            this.EditToDo = new DelegateCommand<ToDoData>(todo =>
            {
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("todo", todo);
                NavigationService.NavigateAsync("SettingPage", parameters);
            });

            this.AddToDo = new DelegateCommand<ToDoData>(todo =>
            {
            });

            this.DeleteToDo = new DelegateCommand<ToDoData>(todo =>
            {
            });
        }

        public IEnumerable<ToDoData> ToDoDataList { get => this.settingService.ToDoDataList; }

        public ICommand AddToDo { get; }
        public ICommand EditToDo { get; }
        public ICommand DeleteToDo { get; }
    }
}
