namespace BindingsDemo;

public class MainWindowViewModel {
    public string Message { get; set; } = "MissBlue";

    public override string ToString() => Message;
}