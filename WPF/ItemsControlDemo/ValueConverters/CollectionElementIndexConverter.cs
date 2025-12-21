using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace ItemsControlDemo.ValueConverters;

public class CollectionElementIndexConverter : IMultiValueConverter {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        if (values.Length >= 2 && values.All(x => true) && values[1] is IList items) return items.IndexOf(values[0]);

        return Binding.DoNothing;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}