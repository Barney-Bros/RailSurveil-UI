using CommunityToolkit.Mvvm.ComponentModel;
using UI.Infrastructure;

namespace UI.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region Fields

    private readonly IAutoUpdateService _autoUpdateService;

    private readonly IThemeService _themeService;

    [ObservableProperty] private string[] _availableThemes;

    [ObservableProperty] private string _currentTheme;

    #endregion

    #region Constructors

    public SettingsViewModel(IThemeService themeService, IAutoUpdateService autoUpdateService)
    {
        _themeService = themeService;
        _autoUpdateService = autoUpdateService;
        _availableThemes = themeService.ThemeNames;
        _currentTheme = themeService.CurrentThemeName;
        autoUpdateService.UpdateExists();
    }

    #endregion

    #region Methods

    partial void OnCurrentThemeChanged(string value) => _themeService.SetTheme(value);

    #endregion
}