using Make10.Interfaces;
using Make10.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using Unity.Attributes;

namespace Make10.ViewModels
{
	public class TimeAttackPageViewModel : ViewModelBase
	{
        private Timer timer;
        private TimeSpan elapsedTime;
        private ObservableCollection<NameCommand> inputHistory;
        private bool turnToInputNumber;
        private string answer;
        private int resultCount;
        private IUserService userService;
        private IResultService resultService;

        [InjectionConstructor]
        public TimeAttackPageViewModel(INavigationService navigationService, IResultService resultService, IUserService userService) : base(navigationService)
        {
            this.userService = userService;
            this.resultService = resultService;

            this.timer = new Timer(100);

            timer.Elapsed += (s, e) =>
            {
                this.elapsedTime = this.elapsedTime.Add(TimeSpan.FromMilliseconds(100));
                this.RaisePropertyChanged(()=>this.ElapsedTimeText);
            };

            this.Numbers = new NameCommand[]
            {
                new NameCommand("", this.OnPushNumber),
                new NameCommand("", this.OnPushNumber),
                new NameCommand("", this.OnPushNumber),
                new NameCommand("", this.OnPushNumber),
            };

            this.Operators = Calculator.OperatorDictionary.Keys.Select(k => new NameCommand(k, this.OnPushOperator)).ToArray();

            this.resultCount = 0;

            this.inputHistory = new ObservableCollection<NameCommand>();
            this.inputHistory.CollectionChanged += (s,e)=>
            {
                this.TurnToInputNumber = this.inputHistory.Count() == 0 || Calculator.OperatorDictionary.ContainsKey(this.inputHistory.Last().Name);
                this.Answer = Calculator.Calculate(this.inputHistory.Select(nc => nc.Name));
                if(this.Answer == "10" && this.Numbers.All(n=>!n.IsEnabled))
                {
                    this.ResultRecord.Items.ElementAt(this.resultCount++).Update(this.elapsedTime, this.Numbers.Select(n=> int.Parse(n.Name)).ToArray());
                    if (this.resultCount >= this.ResultRecord.Items.Count())
                    {
                        this.resultCount = 0;
                        navigationService.GoBackAsync();
                    }
                    else
                    {
                        this.RefreshNumbers();
                        this.OnClear();
                    }
                }
            };

            this.Clear = new DelegateCommand(() =>
            {
                if (true) // for debug
                {
                    this.ResultRecord.Items.ElementAt(this.resultCount++).Update(this.elapsedTime, this.Numbers.Select(n => int.Parse(n.Name)).ToArray());

                    if (this.ResultRecord.Completed)
                    {
                        if (resultService.ResultUpdated != null)
                        {
                            resultService.ResultUpdated();
                        }

                        this.resultCount = 0;
                        navigationService.GoBackAsync();
                    }
                    else
                    {
                        this.RefreshNumbers();
                        this.OnClear();
                    }
                }
                
                this.OnClear();
            });
        }

        public ICommand Clear
        {
            get;
            private set;
        }

        // TimeSpanのBindingにてstringFormatが働かなかったのでText用プロパティを分離
        public string ElapsedTimeText => this.elapsedTime.ToString(@"s\.f");

        public string UserName => this.userService.PlayingUser?.Name;

        public ResultRecord ResultRecord => this.userService.PlayingUser == null? null : this.resultService.ResultRecords[this.userService.PlayingUser];

        public bool TurnToInputNumber
        {
            get
            {
                return turnToInputNumber;
            }

            private set
            {
                if (SetProperty(ref this.turnToInputNumber, value))
                {
                    foreach(var nc in this.Operators)
                    {
                        nc.IsEnabled = !this.turnToInputNumber;
                    }
                }
            }
        }

        public string Answer
        {
            get { return this.answer; }
            private set { SetProperty(ref this.answer, value); }
        }

        public IEnumerable<NameCommand> Numbers { get; private set; }

        public IEnumerable<NameCommand> Operators { get; private set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.RefreshNumbers();

            this.OnClear();

            this.timer.Start();
        }

        private void RefreshNumbers()
        {
            var record = this.resultService.ResultRecords.Values.FirstOrDefault(r => r.Completed);
            int i = 0;
            foreach (var n in record == null? Calculator.CreateNumbers(this.Numbers.Count()) : record.Items.ElementAt(this.resultCount).QuestionNumbers)
            {
                this.Numbers.ElementAt(i++).Name = n.ToString();
            }
        }

        private void OnClear()
        {
            this.inputHistory.Clear();
            foreach (var n in this.Numbers)
            {
                n.IsEnabled = true;
            }

            this.elapsedTime = TimeSpan.Zero;
            this.RaisePropertyChanged(() => this.ElapsedTimeText);
        }

        private void OnPushOperator(NameCommand nc)
        {
            this.inputHistory.Add(nc);
        }

        private void OnPushNumber(NameCommand nc)
        {
            nc.IsEnabled = false;
            this.inputHistory.Add(nc);
        }
    }
}
