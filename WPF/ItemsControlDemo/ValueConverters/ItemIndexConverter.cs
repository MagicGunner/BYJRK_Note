using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ItemsControlDemo.ValueConverters;

public class ItemIndexConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is not DependencyObject obj) return Binding.DoNothing;
        var container = ItemsControl.ItemsControlFromItemContainer(obj);
        return container.ItemContainerGenerator.IndexFromContainer(obj) + 1;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}