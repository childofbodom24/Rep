using Prism;
using Prism.Ioc;
using Make10.ViewModels;
using Make10.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Make10.Services;
using Make10.Interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Make10
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            //"NavigationPage/MainPage"を引数で渡すとNavigationバーが表示される
            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TimeAttackPage, TimeAttackPageViewModel>();
            containerRegistry.RegisterSingleton<IResultService,ResultService> ();
            containerRegistry.RegisterSingleton<IUserService,UserService>();
        }
    }
}
