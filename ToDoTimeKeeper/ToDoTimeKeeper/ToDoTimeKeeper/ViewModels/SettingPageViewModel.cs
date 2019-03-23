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
	public class SettingPageViewModel : ViewModelBase
    {
        private ToDoData toDoData;
        public SettingPageViewModel(INavigationService navigationService, ISettingService settingService) : base(navigationService)
        {
            this.Close = new DelegateCommand<string>(ok =>
            {
                try
                {
                    var ss = settingService as SettingService;
                    if (bool.Parse(ok))
                    {
                        ss.SaveFile();
                    }
                    else
                    {
                        ss.ReadFile();
                    }

                    navigationService.GoBackAsync();
                }
                catch(Exception e)
                {
                    this.DisplayNotification<SettingPageViewModel>("Error", e.Message, () => { });
                }
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.ToDoData = parameters.ContainsKey("todo")? parameters["todo"] as ToDoData : null;
        }

        public ToDoData ToDoData
        {
            get { return toDoData; }
            set { SetProperty(ref toDoData, value); }
        }

        public ICommand Close { get; }
    }
}
