using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WindowCloseDemo.Interface;

namespace WindowCloseDemo;

public partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private string _title = "MissBlue";

    public bool SaveData() {
        return false;
    }

    public Action? CloseWindow { get; set; }

    [RelayCommand]
    private void Close() {
        if (SaveData()) {
            CloseWindow?.Invoke();
        }
    }

    [RelayCommand]
    private void CloseWithParameter(ICloseWindow window) {
        window.CloseWindow();
    }
}