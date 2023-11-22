using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace UI.Converters;

public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
{
    #region Fields

    private BooleanToVisibilityConverter _converter;

    #endregion

    #region Methods

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        _converter ??= new BooleanToVisibilityConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is bool and true ? Visibility.Visible : Visibility.Collapsed;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        Binding.DoNothing;

    #endregion
}