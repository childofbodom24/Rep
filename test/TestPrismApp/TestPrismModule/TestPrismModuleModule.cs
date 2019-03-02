using Prism.Ioc;
using Prism.Modularity;
using TestPrismModule.Views;
using TestPrismModule.ViewModels;
using System.Windows.Input;
using Prism.Commands;
using ClassLibrary1;
using Prism.Navigation;
using Unity.Injection;

namespace TestPrismModule
{
    public class TestPrismModuleModule : IModule
    {
        private IContainerProvider containerProvider;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            containerRegistry.RegisterInstance<TestCommand>(new TestCommand("module1",
                new DelegateCommand(() =>
                {
                    this.containerProvider.Resolve<INavigationService>().NavigateAsync("NavigationPage/ViewA");
                })));
        }
    }
}
