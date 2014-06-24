using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BladestormSE.Resources
{
    public class Utilities
    {
        public class NotificationObject : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
            {
                string propertyName = GetPropertyName(action);
                RaisePropertyChanged(propertyName);
            }

            private static string GetPropertyName<T>(Expression<Func<T>> action)
            {
                var expression = (MemberExpression)action.Body;
                string propertyName = expression.Member.Name;
                return propertyName;
            }

            private void RaisePropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
