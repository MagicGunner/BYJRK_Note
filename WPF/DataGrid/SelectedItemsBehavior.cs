using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Xaml.Behaviors;

namespace DataGrid;

public class SelectedItemsBehavior : Behavior<MultiSelector> {
    protected override void OnAttached() {
        base.OnAttached();
        AssociatedObject.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selector = (MultiSelector)sender;
        BindableSelectedItems = selector.SelectedItems;
        selector.GetBindingExpression(BindableSelectedItemsProperty)?.UpdateSource();
    }

    protected override void OnDetaching() {
        base.OnDetaching();
        AssociatedObject.SelectionChanged -= OnSelectionChanged;
    }

    #region Bindable SelectedItems

    public static readonly DependencyProperty BindableSelectedItemsProperty
        = DependencyProperty.Register(nameof(BindableSelectedItems), typeof(IList), typeof(SelectedItemsBehavior), new PropertyMetadata(null));

    public IList BindableSelectedItems {
        get => (IList)GetValue(BindableSelectedItemsProperty);
        set => SetValue(BindableSelectedItemsProperty, value);
    }

    #endregion
}