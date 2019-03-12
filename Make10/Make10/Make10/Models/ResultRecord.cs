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

        public ResultRecord()
        {
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

        public string ResultTimeText => this.Completed ? new TimeSpan(this.items.Select(i=>i.Time.Ticks).Sum()).ToString(@"s\.f") : "--.-";

        public void Reset()
        {
            foreach(var item in this.items)
            {
                item.Update(TimeSpan.Zero, null);
            }
        }
    }
}
