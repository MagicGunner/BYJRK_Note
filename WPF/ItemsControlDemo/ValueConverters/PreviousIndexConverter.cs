using System.Globalization;
using System.Windows.Data;

namespace ItemsControlDemo.ValueConverters;

public class PreviousIndexConverter : IMultiValueConverter {
    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        var current = values[0] as IIndex;
        var previous = values[1] as IIndex;
        if (current == null) {
            return Binding.DoNothing;
        }

        if (previous == null) {
            current.Index = 1;
        } else {
            current.Index = previous.Index + 1;
        }

        return current.Index;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}