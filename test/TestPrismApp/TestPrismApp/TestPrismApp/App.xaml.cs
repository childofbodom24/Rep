using Prism;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Navigation;
using TestPrismApp.ViewModels;
using TestPrismApp.Views;
using Unity.Injection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestPrismApp
{
    public partial class App
    {
        private IContainerRegistry containerRegistry;
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            this.containerRegistry.RegisterInstance<INavigationService>(this.NavigationService);

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<TestPrismModule.TestPrismModuleModule>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<SubPage, SubPageViewModel>();
            containerRegistry.RegisterInstance<IContainerProvider>(Container);

            this.containerRegistry = containerRegistry;
        }
    }
}
