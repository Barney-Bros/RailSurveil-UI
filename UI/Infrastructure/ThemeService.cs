using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;

namespace UI.Infrastructure;

[ExcludeFromCodeCoverage]
public class ThemeService : IThemeService
{
    #region Constructors

    public ThemeService() =>
        ThemeNames = ThemeManager.Current.Themes.GroupBy(x => x.BaseColorScheme).Select(x => x.Key).ToArray();

    #endregion

    #region Properties

    public string[] ThemeNames { get; }

    public string CurrentThemeName { get; private set; }

    #endregion

    #region Methods

    public void SetTheme(string themeName)
    {
        ThemeManager.Current.ChangeThemeBaseColor(Application.Current, themeName);

        var paletteHelper = new PaletteHelper();
        var theme = paletteHelper.GetTheme();
        theme.SetBaseTheme(themeName == "Dark" ? BaseTheme.Dark : BaseTheme.Light);
        paletteHelper.SetTheme(theme);
        CurrentThemeName = themeName;

        ThemeChanged?.Invoke(this, CurrentThemeName);
    }


    public event EventHandler<string> ThemeChanged;

    #endregion
}