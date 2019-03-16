using Make10.Interfaces;
using Make10.Models;
using Make10.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
                var newId = userService.Users.Max(u => u.Id) + 1;
                new User(newId).CopyTo(this.UserForEdit);
                this.IsEditing = true;
                this.forAdd = true;
            });

            this.DeleteUser = new DelegateCommand(() =>
            {
                this.DisplayConfirmation<MainPageViewModel>(
                       $"{this.selectedRecord.Key.Name}を削除しますか？",
                       string.Empty,
                       () =>
                       {
                           if(File.Exists(this.selectedRecord.Key.ImageFilePath))
                           {
                               File.Delete(this.selectedRecord.Key.ImageFilePath);
                           }

                           userService.Users.Remove(this.selectedRecord.Key);
                           this.SelectedRecord = this.nullRecord;
                           this.RaisePropertyChanged(() => this.ResultRecords);
                           userService.Save();
                       },
                       () => { });
            }, () => this.selectedRecord.Key != null);

            this.EditUser = new DelegateCommand(() =>
            {
                this.selectedRecord.Key.CopyTo(this.UserForEdit);
                this.IsEditing = true;
                this.forAdd = false;
            }, () => this.selectedRecord.Key != null);

            this.EditEnd = new DelegateCommand<string>(s =>
            {
                if (s == "OK")
                {
                    if (forAdd) userService.Users.Add(this.UserForEdit.Clone());
                    else this.UserForEdit.CopyTo(this.selectedRecord.Key);
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

            this.ReadImage = new DelegateCommand<User>(u =>
            {
                DependencyService.Get<IImageGalleryService>().GetImageStream(stream=>
                {
                    var filepath = JsonSerializer.GetApplicationFilePath(u.Name);
                    using (FileStream destination = File.Open(filepath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(destination);
                    }

                    if (File.Exists(filepath))
                    {
                        u.ImageFilePath = filepath;
                        userService.Save();
                    }
                });
            });

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

        public User UserForEdit { get; } = new User(0);

        public bool IsEditing
        {
            get { return isEditing; }
            set { SetProperty(ref isEditing, value); }
        }

        public ICommand AddUser
        {
            get;
        }

        public ICommand DeleteUser
        {
            get;
        }

        public ICommand EditUser
        {
            get;
        }

        public ICommand EditEnd
        {
            get;
        }

        public ICommand ResetResult
        {
            get;
        }

        public ICommand ReadImage
        {
            get;
        }

        public IEnumerable<int> HandicapList => Enumerable.Range(0, 51).ToArray();

        //public IDictionary<User, ResultRecord> ResultRecords => this.resultService.ResultRecords; //<-これはAdd/DeleteしたときにXamlに反映されない

        public IEnumerable<KeyValuePair<User, ResultRecord>> ResultRecords => this.resultService.ResultRecords.OrderBy(r=>r.Value.ResultTime);

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.RaisePropertyChanged(() => this.ResultRecords);
            (this.ResetResult as DelegateCommand).RaiseCanExecuteChanged();
        }

    }
}
