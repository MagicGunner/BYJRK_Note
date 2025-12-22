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
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using WindowCloseDemo.Interface;
using WindowCloseDemo.Messages;

namespace WindowCloseDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, ICloseWindow {
    public MainWindow() {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (_, m) => {
                                                                              if (m.Sender == DataContext) {
                                                                                  Close();
                                                                              }
                                                                          });
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

    private void ButtonBase_OnClick_1(object sender, RoutedEventArgs e) {
        var response = WeakReferenceMessenger.Default.Send(new RequestMessage<bool>());
        if (response.Response) {
            Close();
        }
    }
}