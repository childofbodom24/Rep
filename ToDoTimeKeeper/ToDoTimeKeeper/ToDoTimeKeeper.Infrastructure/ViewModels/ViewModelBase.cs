using ToDoTimeKeeper.Infrastructure.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.Forms;
using Make10.Models;

namespace ToDoTimeKeeper.Infrastructure.ViewModels
{
    public class ViewModelBase : BindableBaseEx, INavigationAware, IDestructible
    {
        private string _title;

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected INavigationService NavigationService { get; private set; }

        protected void DisplayAlert<T>(AlertParameter parameters) where T : class
        {
            MessagingCenter.Send(this as T, "DisplayAlert", parameters);
        }

        protected void DisplayNotification<T>(string title, string message, Action ok) where T : ViewModelBase
        {
            this.DisplayAlert<T>(new AlertParameter()
            {
                Title = title,
                Message = message,
                Accept = "",
                Cancel = "OK",
                Action = result =>
                {
                    ok?.Invoke();
                }
            });
        }
        protected void DisplayConfirmation<T>(string title, string message, Action ok, Action cancel, string okText = "OK", string cancelText = "CANCEL") where T : class
        {
            this.DisplayAlert<T>(new AlertParameter()
            {
                Title = title,
                Message = message,
                Accept = okText,
                Cancel = cancelText,
                Action = result =>
                {
                    if (result) ok?.Invoke();
                    else cancel?.Invoke();
                }
            });
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {

        }
    }
}
