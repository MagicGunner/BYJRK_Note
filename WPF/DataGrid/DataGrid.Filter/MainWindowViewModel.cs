using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DataGrid.Filter;

public partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private List<Employee> _employees;

    [ObservableProperty]
    private ICollectionView _employeeCollection;

    [ObservableProperty]
    private string? _key;

    public MainWindowViewModel() {
        Employees = Employee.FakeMany(10).ToList();
        EmployeeCollection = CollectionViewSource.GetDefaultView(Employees);
        EmployeeCollection.Filter = (item => {
                                         if (string.IsNullOrEmpty(Key)) return true;
                                         if (item is not Employee employee) return false;
                                         if (employee.FirstName == null || employee.LastName == null) return false;
                                         return employee.FirstName.Contains(Key) || employee.LastName.Contains(Key);
                                     });
    }

    [RelayCommand]
    private void AddEmployee() {
        Employees.Add(Employee.FakeOne());
        EmployeeCollection.Refresh();
    }

    partial void OnKeyChanged(string? value) {
        EmployeeCollection.Refresh();
    }
}