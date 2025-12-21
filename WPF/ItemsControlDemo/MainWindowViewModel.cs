using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ItemsControlDemo;

public partial class MainWindowViewModel : ObservableObject {
    #region ItemsControl

    /// <summary>
    /// 一些字符串，用来展示基础的 ItemsSource 绑定，奇偶行交替颜色，以及表头
    /// </summary>
    public ObservableCollection<string> Stars { get; } = new(["Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune"]);

    /// <summary>
    /// 用来展示替换 ItemsPanel 为 Canvas，以及修改 ItemContainerStyle
    /// </summary>
    public ObservableCollection<Shape> Shapes { get; } = [
                                                             new(0, new Point(50, 50), Brushes.Red),
                                                             new(1, new Point(150, 70), Brushes.Green),
                                                             new(1, new Point(300, 160), Brushes.Blue),
                                                             new(0, new Point(240, 260), Brushes.Yellow)
                                                         ];

    /// <summary>
    /// 用于展示在 Resources 中存放多个 DataType 不同的 DataTemplate 的效果
    /// </summary>
    public ObservableCollection<Fruit> Fruits { get; } = [
                                                             new Apple { Amount = 3 },
                                                             new Apple { Amount = 2 },
                                                             new Banana { Amount = 6 },
                                                             new Apple { Amount = 4 },
                                                             new Banana { Amount = 1 },
                                                             new Banana { Amount = 5 }
                                                         ];

    /// <summary>
    /// 用于展示 DataTemplateSelector 的使用
    /// </summary>
    public ObservableCollection<Employee> Employees { get; } = [
                                                                   new("John", Gender.Male, 27),
                                                                   new("Anna", Gender.Female, 31),
                                                                   new("Joyce", Gender.Female, 28),
                                                                   new("Tony", Gender.Male, 40),
                                                                   new("Brian", Gender.Male, 36)
                                                               ];

    #endregion

    public MainWindowViewModel() {
        UpdateItems();
    }

    private void UpdateItems() {
        for (var i = 0; i < Items.Count; i++) { Items[i].Index = i + 1; }
    }

    #region ItemsControl1

    [ObservableProperty]
    private ObservableCollection<Model> _items = [
                                                     "first item",
                                                     "second item",
                                                     "third item",
                                                     "fourth item",
                                                     "fifth item",
                                                     "sixth item",
                                                     "seventh item",
                                                     "eighth item",
                                                     "ninth item",
                                                     "tenth item",
                                                     "eleventh item",
                                                     "twelfth item",
                                                     "thirteenth item",
                                                     "fourteenth item",
                                                     "fifteenth item",
                                                     "sixteenth item"
                                                 ];

    public IEnumerable<int> Indexes => Enumerable.Range(1, Items.Count);

    [RelayCommand]
    private void Remove() {
        Items.RemoveAt(4);
        // OnPropertyChanged(nameof(Items));

        OnPropertyChanged(nameof(Indexes));

        UpdateItems();
    }

    #endregion
}

#region ItemsControl

#region Canvas

public record Shape(int Type, Point Pos, Brush Color);

#endregion

#region Fruits

public abstract class Fruit {
    public int    Amount    { get; set; }
    public string FruitType => this.GetType().Name;
}

public class Apple : Fruit {
}

public class Banana : Fruit {
}

#endregion

#region Employees

public record Employee(string Name, Gender Gender, int Age);

public enum Gender {
    Male,
    Female
};

public class EmployeeTemplateSelector : DataTemplateSelector {
    public DataTemplate MaleTemplate   { get; set; }
    public DataTemplate FemaleTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container) {
        var element = container as FrameworkElement;
        var employee = item as Employee;
        return employee?.Gender switch {
                   Gender.Male   => MaleTemplate,
                   Gender.Female => FemaleTemplate,
                   _             => throw new ArgumentException()
               };
    }
}

#endregion

#endregion

#region ItemsControl2

public partial class Model : ObservableObject, IIndex {
    [ObservableProperty]
    private int? _index;

    [ObservableProperty]
    private string? _content;

    public static implicit operator Model(string c) => new Model { Content = c };

    public override string? ToString() => Content;
}

public interface IIndex {
    int? Index { get; set; }
}

#endregion