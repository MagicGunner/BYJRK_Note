using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValueConverterDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }
}

public abstract class BaseValueConverter : MarkupExtension, IValueConverter {
    public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

    public abstract object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture);

    public override object? ProvideValue(IServiceProvider serviceProvider) {
        return this;
    }
}

public class BoolToVisibilityConverter : BaseValueConverter {
    // public static BooleanToVisibilityConverter Instance { get; } = new();

    public bool UseHidden { get; set; }

    public bool Reversed { get; set; }

    public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is not bool b) throw new ArgumentException();
        if (Reversed) b = !b;
        return b ? Visibility.Visible : UseHidden ? Visibility.Hidden : Visibility.Collapsed;
    }

    public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}