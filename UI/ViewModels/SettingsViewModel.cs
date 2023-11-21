using CommunityToolkit.Mvvm.ComponentModel;
using UI.Infrastructure;

namespace UI.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region Fields

    private readonly IThemeService _themeService;

    [ObservableProperty] private string[] _availableThemes;

    [ObservableProperty] private string _currentTheme;

    #endregion

    #region Constructors

    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;
        _availableThemes = themeService.ThemeNames;
        _currentTheme = themeService.CurrentThemeName;
    }

    #endregion

    #region Methods

    partial void OnCurrentThemeChanged(string value) => _themeService.SetTheme(value);

    #endregion
}