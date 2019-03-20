using ToDoTimeKeeper.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ToDoTimeKeeper.Models;

namespace ToDoTimeKeeper.Infrastructure.Extensions
{
    public static class PageExtension
    {

        public static void SubscribeDisplayAlertMessage<T>(this Page page, Action<T,AlertParameter> action) where T : class
        {
            page.Appearing += (s, e) =>
            {
                MessagingCenter.Subscribe<T, AlertParameter>(page, "DisplayAlert", action);
            };

            page.Disappearing += (s, e) =>
            {
                MessagingCenter.Unsubscribe<T, AlertParameter>(page, "DisplayAlert");
            };
        }
        
        public static async void DisplayAlert(this Page page, AlertParameter arg)
        {
            if (string.IsNullOrEmpty(arg.Accept))
            {
                await page.DisplayAlert(arg.Title, arg.Message,arg.Cancel);
                arg.Action?.Invoke(false);
            }
            else
            {
                var isAccept = await page.DisplayAlert(arg.Title, arg.Message, arg.Accept, arg.Cancel);
                arg.Action?.Invoke(isAccept);
            }
        }
    }
}
