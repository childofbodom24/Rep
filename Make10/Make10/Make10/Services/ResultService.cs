using Make10.Interfaces;
using Make10.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Unity.Attributes;

namespace Make10.Services
{
    public class ResultService : BindableBaseEx, IResultService 
    {
        [InjectionConstructor]
        public ResultService(IUserService userService)
        {
            userService.Users.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    this.ResultRecords.Clear();
                }
                else if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (User u in e.NewItems)
                    {
                        this.ResultRecords.Add(u, new ResultRecord());
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (User u in e.OldItems)
                    {
                        this.ResultRecords.Remove(u);
                    }
                }

                this.RaisePropertyChanged(()=> this.ResultRecords);
            };
            
            foreach (var u in userService.Users)
            {
                this.ResultRecords.Add(u, new ResultRecord());
            }
        }

        public IDictionary<User, ResultRecord> ResultRecords { get; } = new Dictionary<User, ResultRecord>();

        public Action ResultUpdated { get; set; }
    }
}
