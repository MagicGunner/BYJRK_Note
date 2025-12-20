using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DataGrid;

public class MultiSelectorHelper {
    #region Bindable SelectedItems

    public static readonly DependencyProperty BindableSelectedItemsProperty
        = DependencyProperty.RegisterAttached("BindableSelectedItems", typeof(IList), typeof(MultiSelectorHelper), new PropertyMetadata(null));

    public static void SetBindableSelectedItems(DependencyObject element, IList value) {
        element.SetValue(BindableSelectedItemsProperty, value);
    }

    public static IList GetBindableSelectedItems(DependencyObject element) {
        return (IList)element.GetValue(BindableSelectedItemsProperty);
    }

    #endregion

    #region Monitor SelectionChanged

    public static readonly DependencyProperty MonitorSelectionChangedProperty
        = DependencyProperty.RegisterAttached("MonitorSelectionChanged", typeof(bool), typeof(MultiSelectorHelper), new PropertyMetadata(false, MonitorSelectionChangedPropertyChangedCallback));

    public static void SetMonitorSelectionChanged(DependencyObject element, bool value) {
        element.SetValue(MonitorSelectionChangedProperty, value);
    }

    public static bool GetMonitorSelectionChanged(DependencyObject element) {
        return (bool)element.GetValue(MonitorSelectionChangedProperty);
    }

    private static void MonitorSelectionChangedPropertyChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
        if (obj is not MultiSelector selector) throw new InvalidOperationException();

        if ((bool)e.NewValue) {
            selector.SelectionChanged += OnSelectionChanged;
        } else {
            selector.SelectionChanged -= OnSelectionChanged;
        }
    }

    private static void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selector = (MultiSelector)sender;
        SetBindableSelectedItems(selector, selector.SelectedItems);
        selector.GetBindingExpression(BindableSelectedItemsProperty)?.UpdateSource();
    }

    #endregion
}