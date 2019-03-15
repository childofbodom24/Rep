using Make10.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Unity.Attributes;

namespace Make10.Models
{
    public class ResultRecord : BindableBaseEx
    {
        private ResultRecordItem[] items;
        private int winCount;

        public ResultRecord()
        {
            this.winCount = 0;
            this.items = new ResultRecordItem[3];
            foreach (var n in Enumerable.Range(0, this.items.Count()))
            {
                this.items[n] = new ResultRecordItem(n + 1);
                this.items[n].PropertyChanged += (s, e) =>
                {
                    var item = (s as ResultRecordItem);
                    if (e.PropertyName == nameof(item.TimeText))
                    {
                        this.RaisePropertyChanged(() => Completed);
                        this.RaisePropertyChanged(() => ResultTimeText);
                    }
                };
            }
        }

        public IEnumerable<ResultRecordItem> Items => this.items;

        public bool Completed => this.items.All(i => i.Time != TimeSpan.Zero);

        public TimeSpan ResultTime => new TimeSpan(this.items.Select(i => i.Time.Ticks).Sum()) + TimeSpan.FromSeconds(this.Handicap);

        public string ResultTimeText
        {
            get
            {
                var handicapText = this.Handicap == 0 ? string.Empty : $"({this.Handicap.ToString("+#;-#;")})";
                var text = this.Completed ? this.ResultTime.TotalSeconds.ToString("F1") + handicapText : "-----";
                return this.Rank == 1 ? $"☆{text}" : text;
            }
        }

        public string WinCountText
        {
            get
            {
                return this.winCount <= 3 ? Enumerable.Range(0, this.winCount).Aggregate<int, string>(string.Empty, (text, i) => text += "★") : $"★×{this.winCount}";
            }
        }

        public int Rank { get; set; }
        public int Handicap { get; set; }

        public void Reset()
        {
            if (this.Rank == 1)
            {
                this.winCount++;
                this.RaisePropertyChanged(() => this.WinCountText);
            }

            this.Rank = 0;
            foreach(var item in this.items)
            {
                item.Update(TimeSpan.Zero, null);
            }
        }
    }
}
