using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ListBoxDemo;

public partial class MainWindowViewModel : ObservableObject {
    public List<Student> Students { get; } = [
                                                 new(FullName: "Jeremy", Age: 17),
                                                 new(FullName: "Brian", Age: 19),
                                                 new(FullName: "Tom", Age: 18),
                                                 new(FullName: "Peter", Age: 20),
                                                 new(FullName: "Diana", Age: 19),
                                                 new(FullName: "Emma", Age: 18)
                                             ];

    [ObservableProperty]
    private string? _selectedName;

    [RelayCommand]
    private void SelectStudent(string name) {
        SelectedName = name;
    }
}

public record Student(string FullName, int Age);