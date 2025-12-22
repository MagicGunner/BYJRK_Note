using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ContextMenuDemo;

public partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private List<string> _items = ["MissBlue", "MagicGunner",];

    [RelayCommand]
    private void Foo(object obj) {
        // Do nothing
        MessageBox.Show($"点击了{obj}");
    }
}