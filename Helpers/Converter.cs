using System;
using System.Globalization;

namespace MyMauiApp;

public class Converter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var filterValue = value as string;
        var parameterString = parameter as string;

        // Return true if the filter value matches the RadioButton's parameter
        return filterValue == parameterString;

    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
