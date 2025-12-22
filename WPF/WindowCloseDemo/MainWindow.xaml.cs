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
using WindowCloseDemo.Interface;

namespace WindowCloseDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, ICloseWindow {
    public MainWindow() {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
        Close();
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
        var vm = DataContext as MainWindowViewModel;
        vm?.CloseWindow -= Close;
        vm?.CloseWindow += Close;
    }

    public void CloseWindow() => Close();
}