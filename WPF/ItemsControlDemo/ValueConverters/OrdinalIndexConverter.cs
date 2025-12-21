using System.Globalization;
using System.Windows.Data;

namespace ItemsControlDemo.ValueConverters;

public class OrdinalIndexConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return (int)(value ?? 0) + 1;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}