using System.Collections;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DataGrid;

public partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private List<Employee> _employees;

    [ObservableProperty]
    private ICollectionView _employeeCollection;


    [ObservableProperty]
    private IList _selectedItems;

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

    [RelayCommand]
    private void DeleteEmployees(IList selectedItems) {
        foreach (var item in selectedItems.Cast<Employee>().ToList()) Employees.Remove(item);

        for (var i = 0; i < Employees.Count; i++) Employees[i].Id = i + 1;

        EmployeeCollection.Refresh();
    }

    partial void OnKeyChanged(string? value) {
        EmployeeCollection.Refresh();
    }

    // [RelayCommand]
    // private void GetSumSalary(IList selectedItems) {
    //     var sum = selectedItems.Cast<Employee>().Sum(e => e.Salary);
    // }

    [RelayCommand]
    private void GetSumSalary() {
        var sum = SelectedItems.Cast<Employee>().Sum(e => e.Salary);
    }
}