using Make10.Extensions;
using Make10.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Make10.Models
{
    public class ResultRecordItem : BindableBaseEx
    {
        private TimeSpan time;
        public ResultRecordItem(int id)
        {
            this.Id = id;
            this.time = TimeSpan.Zero;
        }

        public int Id
        {
            get;
            private set;
        }

        public TimeSpan Time
        {
            get
            {
                return time;
            }
            private set
            {
                if (SetProperty(ref time, value))
                {
                    this.RaisePropertyChanged(() => this.TimeText);
                }
            }
        }

        public IEnumerable<int> QuestionNumbers { get; private set; }

        public string TimeText
        {
            get
            {
                return this.time == TimeSpan.Zero ? "-----" : this.time.TotalSeconds.ToString("F1");
            }
        }

        public void Update(TimeSpan time, IEnumerable<int> questionNumbers)
        {
            this.Time = time;
            this.QuestionNumbers = questionNumbers;
        }
    }
}
