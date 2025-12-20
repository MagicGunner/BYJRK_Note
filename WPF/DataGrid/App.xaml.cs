using System.Windows;
using Bogus;

namespace DataGrid;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private void Application_Startup(object sender, StartupEventArgs e) {
        Randomizer.Seed = new Random(1334);
    }
}