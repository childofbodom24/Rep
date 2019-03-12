using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Make10.Models
{
    public class NameCommand : BindableBase
    {
        private string name;
        private bool isEnabled;

        public NameCommand(string name, Action<NameCommand> executeMethod)
        {
            this.isEnabled = true;
            this.Name = name;
            this.Command = new DelegateCommand(() => { executeMethod(this); }, () => this.isEnabled);
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.SetProperty(ref this.isEnabled, value))
                {
                    (this.Command as DelegateCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand Command
        {
            get;
            private set;
        }

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }


    }
}