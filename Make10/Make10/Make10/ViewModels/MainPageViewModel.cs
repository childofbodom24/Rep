﻿using Make10.Interfaces;
using Make10.Models;
using Make10.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Unity.Attributes;
using Xamarin.Forms;

namespace Make10.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IResultService resultService;
        private bool isEditing;
        private bool forAdd;
        private string editUserName;
        private KeyValuePair<User, ResultRecord> selectedRecord;
        private KeyValuePair<User, ResultRecord> nullRecord = new KeyValuePair<User, ResultRecord>();

        [InjectionConstructor]
        public MainPageViewModel(INavigationService navigationService, IUserService userService, IResultService resultService)
            : base(navigationService)
        {
            this.StartTimeAttack = new DelegateCommand<User>(u =>
            {
                userService.PlayingUser = u;
                navigationService.NavigateAsync("TimeAttackPage");
            });

            this.AddUser = new DelegateCommand(() =>
            {
                this.EditUserName = string.Empty;
                this.IsEditing = true;
                this.forAdd = true;
            });

            this.DeleteUser = new DelegateCommand(() =>
            {
                userService.Users.Remove(this.selectedRecord.Key);
                this.SelectedRecord = this.nullRecord;
                this.RaisePropertyChanged(() => this.ResultRecords);
                userService.Save();
            }, () => this.selectedRecord.Key != null);

            this.EditUser = new DelegateCommand(() =>
            {
                this.EditUserName = this.selectedRecord.Key.Name;
                this.IsEditing = true;
                this.forAdd = false;
            }, () => this.selectedRecord.Key != null);

            this.EditEnd = new DelegateCommand<string>(s =>
            {
                if (s == "OK")
                {
                    if (forAdd) userService.Users.Add(new User() { Name = this.editUserName });
                    else this.selectedRecord.Key.Name = this.editUserName;
                    userService.Save();
                }

                this.IsEditing = false;
                this.SelectedRecord = this.nullRecord;
                this.RaisePropertyChanged(() => this.ResultRecords);
            });

            this.ResetResult = new DelegateCommand(() =>
            {
                var winner = resultService.ResultRecords.First(r => r.Value.Rank == 1).Key;
                this.DisplayConfirmation<MainPageViewModel>(
                    $"{winner.Name}の勝ち",
                    "結果をリセットしますか？",
                    () =>
                    {
                        foreach(var r in resultService.ResultRecords.Values)
                        {
                            r.Reset();
                        }
                        
                        (this.ResetResult as DelegateCommand).RaiseCanExecuteChanged();
                    },
                    () => { });
            }, ()=> resultService.ResultRecords.Values.Any(r=>r.Rank == 1));

            this.resultService = resultService;
        }

        public ICommand StartTimeAttack
        {
            get;
            private set;
        }

        public KeyValuePair<User, ResultRecord> SelectedRecord
        {
            get
            {
                return selectedRecord;
            }

            set
            {
                if (SetProperty(ref selectedRecord, value))
                {
                    (this.DeleteUser as DelegateCommand).RaiseCanExecuteChanged();
                    (this.EditUser as DelegateCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string EditUserName
        {
            get { return editUserName; }
            set { SetProperty(ref editUserName, value); }
        }

        public bool IsEditing
        {
            get { return isEditing; }
            set { SetProperty(ref isEditing, value); }
        }

        public ICommand AddUser
        {
            get;
            private set;
        }

        public ICommand DeleteUser
        {
            get;
            private set;
        }

        public ICommand EditUser
        {
            get;
            private set;
        }

        public ICommand EditEnd
        {
            get;
            private set;
        }

        public ICommand ResetResult
        {
            get;
            private set;
        }

        //public IDictionary<User, ResultRecord> ResultRecords => this.resultService.ResultRecords; //<-これはAdd/DeleteしたときにXamlに反映されない

        public IEnumerable<KeyValuePair<User, ResultRecord>> ResultRecords => this.resultService.ResultRecords.ToArray();

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.RaisePropertyChanged(() => this.ResultRecords);
            (this.ResetResult as DelegateCommand).RaiseCanExecuteChanged();
        }

    }
}