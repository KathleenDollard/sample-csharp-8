using System;
using System.Windows;
using System.Windows.Controls;

namespace TollCollectorApp
{
    internal class LogDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item,
            DependencyObject container)
        {
            if (!(container is FrameworkElement element))
            {
                return null;
            }

            return item switch
            {
                LogItem _ => element.FindResource("LogItemTemplate") as DataTemplate,
                Exception _ => element.FindResource("ExceptionTemplate") as DataTemplate,
                _ => throw new Exception()
            };
        }
    }
}
