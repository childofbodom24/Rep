using Make10.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Make10.Interfaces
{
    public interface IResultService
    {
        IDictionary<User, ResultRecord> ResultRecords { get; }
        void UpdateRanking(User playingUser);
    }
}
