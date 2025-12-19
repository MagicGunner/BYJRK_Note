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
    private int _pageNum = 1;

    /// <summary>
    /// 每一页能显示多少项目
    /// </summary>
    public const int PageSize = 15;

    [ObservableProperty]
    private IList _selectedItems;

    [ObservableProperty]
    private string? _key;

    public MainWindowViewModel() {
        Employees = Employee.FakeMany(100).ToList();
        EmployeeCollection = CollectionViewSource.GetDefaultView(Employees);

        // 实现过滤功能
        // EmployeeCollection.Filter = (item => {
        //                                  if (string.IsNullOrEmpty(Key)) return true;
        //                                  if (item is not Employee employee) return false;
        //                                  if (employee.FirstName == null || employee.LastName == null) return false;
        //                                  return employee.FirstName.Contains(Key) || employee.LastName.Contains(Key);
        //                              });

        // 通过过滤功能实现分页
        EmployeeCollection.Filter = item => {
                                        if (item is not Employee employee) return false;
                                        if (PageNum < 1 || PageNum > Employees.Count / PageSize + 2) return false;
                                        return employee.Id >= (PageNum - 1) * PageSize && employee.Id < PageNum * PageSize;
                                    };
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

    [RelayCommand]
    private void Calculate() {
        foreach (var employee in Employees) { employee.IsSelected = employee.Salary > 100000; }

        EmployeeCollection.Refresh();
    }

    [RelayCommand]
    private void Go() {
        EmployeeCollection.Refresh();
    }

    partial void OnPageNumChanged(int value) {
        EmployeeCollection.Refresh();
    }
}