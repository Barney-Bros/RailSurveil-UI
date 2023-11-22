using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using UI.Infrastructure;

namespace UI.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region Fields

    private readonly IApplicationSettingsService _applicationSettingsService;

    private readonly IThemeService _themeService;

    [ObservableProperty]
    private string[] _availableThemes;

    [ObservableProperty]
    private string _currentTheme;

    [ObservableProperty]
    private bool _updateExists;

    [ObservableProperty]
    private int _updatePercentage;

    #endregion

    #region Constructors

    public SettingsViewModel(IThemeService themeService, IAutoUpdateService autoUpdateService,
        IApplicationSettingsService applicationSettingsService)
    {
        _themeService = themeService;
        _applicationSettingsService = applicationSettingsService;
        _availableThemes = themeService.ThemeNames;
        var themeName = _applicationSettingsService.GetThemeSetting();
        if (string.IsNullOrEmpty(themeName))
        {
            _applicationSettingsService.SetThemeSetting(_themeService.ThemeNames.First());
            themeName = _applicationSettingsService.GetThemeSetting();
        }

        _themeService.SetTheme(themeName);
        CurrentTheme = _themeService.CurrentThemeName;
    }

    #endregion

    #region Methods

    partial void OnCurrentThemeChanged(string value)
    {
        _themeService.SetTheme(value);
        _applicationSettingsService.SetThemeSetting(value);
    }

    #endregion
}