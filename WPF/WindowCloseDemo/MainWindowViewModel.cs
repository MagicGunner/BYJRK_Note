using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using WindowCloseDemo.Interface;
using WindowCloseDemo.Messages;

namespace WindowCloseDemo;

public partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private string _title = "MissBlue";

    public MainWindowViewModel() {
        WeakReferenceMessenger.Default.Register<RequestMessage<bool>>(this, (_, message) => message.Reply(SaveData()));
    }

    public bool SaveData() {
        return true;
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

    [RelayCommand]
    private void CloseByMessage() {
        if (SaveData()) {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage { Sender = this });
        }
    }
}