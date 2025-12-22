using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingsDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
        var binding = new Binding {
                                      Path = new PropertyPath("Message"),
                                      Mode = BindingMode.TwoWay
                                  };
        BindingOperations.SetBinding(TBL, TextBlock.TextProperty, binding);

        Resources["Str"] = "MagicGunner";
    }
}

internal class MyResource {
    public        string Message      { get; set; } = "Public Property";
    public static string StaticString { get; set; } = "Static String";
    public const  string ConstString = "Const String";
}