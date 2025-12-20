using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DataGrid;

public partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private ObservableCollection<Employee> _employees;

    [ObservableProperty]
    private IEnumerable<Employee> _employeeDisplay;

    // [ObservableProperty]
    // private ICollectionView _employeeCollection;

    public int PageNum {
        get;
        set {
            value = Math.Max(0, value);
            value = Math.Min(value, MaxPageNum);
            if (SetProperty(ref field, value)) {
                OnPageNumChanged(value);
            }
        }
    } = 1;

    public int MaxPageNum => Employees.Count / PageSize + 1;

    /// <summary>
    /// 每一页能显示多少项目
    /// </summary>
    public const int PageSize = 15;

    [ObservableProperty]
    private IList _selectedItems;

    [ObservableProperty]
    private string? _key;

    public MainWindowViewModel() {
        Employees = new ObservableCollection<Employee>(Employee.FakeMany(100));
        EmployeeDisplay = Employees.Take(PageSize);
        // EmployeeCollection = CollectionViewSource.GetDefaultView(Employees);

        // 实现过滤功能
        // EmployeeCollection.Filter = (item => {
        //                                  if (string.IsNullOrEmpty(Key)) return true;
        //                                  if (item is not Employee employee) return false;
        //                                  if (employee.FirstName == null || employee.LastName == null) return false;
        //                                  return employee.FirstName.Contains(Key) || employee.LastName.Contains(Key);
        //                              });

        // 通过过滤功能实现分页
        // EmployeeCollection.Filter = item => {
        //                                 if (item is not Employee employee) return false;
        //                                 if (PageNum < 1 || PageNum > Employees.Count / PageSize + 2) return false;
        //                                 return employee.Id >= (PageNum - 1) * PageSize && employee.Id < PageNum * PageSize;
        //                             };

        Employees.CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        if (e.Action is NotifyCollectionChangedAction.Add or NotifyCollectionChangedAction.Remove) {
            for (var i = 0; i < Employees.Count; i++) Employees[i].Id = i + 1;

            OnPropertyChanged(nameof(MaxPageNum));
            OnPageNumChanged(PageNum);
        }
    }

    [RelayCommand]
    private void AddEmployee() {
        Employees.Add(Employee.FakeOne());
    }

    [RelayCommand]
    private void DeleteEmployees() {
        var selectedEmployees = SelectedItems.Cast<Employee>().ToList();
        for (var i = 0; i < selectedEmployees.Count; i++) {
            if (i == 0) Employees.CollectionChanged -= OnCollectionChanged;
            if (i == selectedEmployees.Count - 1) Employees.CollectionChanged += OnCollectionChanged;
            Employees.Remove(selectedEmployees[i]);
        }
    }


    [RelayCommand]
    private void GetSumSalary() {
        var sum = SelectedItems.Cast<Employee>().Sum(e => e.Salary);
    }

    [RelayCommand]
    private void Calculate() {
        foreach (var employee in Employees) { employee.IsSelected = employee.Salary > 100000; }

        // EmployeeCollection.Refresh();
    }

    [RelayCommand]
    private void GotoPage(string pageNum) {
        PageNum = int.TryParse(pageNum, out var num) ? num : 1;

        // EmployeeDisplay = Employees.GetRange(PageSize * (PageNum - 1), Math.Min(PageSize, Employees.Count - PageSize * (PageNum - 1)));
    }

    private void OnPageNumChanged(int value) {
        EmployeeDisplay = Employees.Skip(PageSize * (PageNum - 1)).Take(PageSize);
    }
}