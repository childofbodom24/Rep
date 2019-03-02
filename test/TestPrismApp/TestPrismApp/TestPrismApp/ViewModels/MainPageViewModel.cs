using ClassLibrary1;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Attributes;

namespace TestPrismApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand navigate;

        [InjectionConstructor]
        public MainPageViewModel(TestCommand command, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            this.Commands = new TestCommand[] { command };
        }

        public IEnumerable<TestCommand> Commands
        {
            get;
            private set;
        }

        public DelegateCommand Navigate =>
            navigate ?? (navigate = new DelegateCommand(ExecuteNavigateAsync));

        async void ExecuteNavigateAsync()
        {
            await this.NavigationService.NavigateAsync("NavigationPage/ViewA");
        }
        
    }
}
