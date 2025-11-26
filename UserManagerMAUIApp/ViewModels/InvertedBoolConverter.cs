using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagerMAUIApp.ViewModels;

// Value Converters (add these at the bottom of UserViewModel.cs, outside the class)

/// <summary>
/// Converts boolean to its inverted value
/// </summary>
// Value Converters (add these at the bottom of UserViewModel.cs, outside the class)

/// <summary>
/// Converts boolean to its inverted value
/// </summary>
public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is bool boolValue ? !boolValue : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is bool boolValue ? !boolValue : value;
    }
}

/// <summary>
/// Converts boolean to status text
/// </summary>
public class BoolToStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is bool isActive && isActive ? "● Active" : "○ Inactive";
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts boolean to color
/// </summary>
public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is bool isActive && isActive ? Color.FromArgb("#10B981") : Color.FromArgb("#9CA3AF");
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}