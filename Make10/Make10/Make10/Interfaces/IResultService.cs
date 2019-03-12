using Make10.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Make10.Interfaces
{
    public interface IResultService
    {
        Action ResultUpdated { get; set; }
        IDictionary<User, ResultRecord> ResultRecords { get; }
    }
}
