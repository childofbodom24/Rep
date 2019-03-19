using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ToDoTimeKeeper.Infrastructure.Models
{
    public class BindableBaseEx : BindableBase
    {
        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyName)
        {
            var memberEx = propertyName.Body as MemberExpression;
            if (memberEx == null)
                throw new ArgumentException();

            this.RaisePropertyChanged(memberEx.Member.Name);
        }
    }
}
