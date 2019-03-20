using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ToDoTimeKeeper.Models
{
    public class AlertParameter
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Accept { get; set; }
        public string Cancel { get; set; }
        public Action<bool> Action { get; set; }
    }
}
