using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UI.Infrastructure;

namespace UI.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region Fields

    private readonly IApplicationSettingsService _applicationSettingsService;

    private readonly IAutoUpdateService _autoUpdateService;

    private readonly IThemeService _themeService;

    [ObservableProperty]
    private string[] _availableThemes;

    [ObservableProperty]
    private string _currentTheme;


    private TaskNotifier<bool> _updateExists;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PerformUpdateCommand))]
    private int _updatePercentage;

    #endregion

    #region Constructors

    public SettingsViewModel(IThemeService themeService, IAutoUpdateService autoUpdateService,
        IApplicationSettingsService applicationSettingsService)
    {
        _themeService = themeService;
        _autoUpdateService = autoUpdateService;
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
        CheckForApplicationUpdates();
    }

    #endregion

    #region Properties

    public Task<bool> UpdateExists
    {
        get => _updateExists;
        set => SetPropertyAndNotifyOnCompletion(ref _updateExists, value);
    }

    #endregion

    #region Methods

    [RelayCommand(CanExecute = nameof(CanPerformUpdate))]
    public async Task PerformUpdate() =>
        await _autoUpdateService.UpdateLatestAndRestart(new Progress<int>(x => UpdatePercentage = x));

    private bool CanPerformUpdate() => UpdateExists.Result;

    private void CheckForApplicationUpdates() => UpdateExists = _autoUpdateService.UpdateExists();

    partial void OnCurrentThemeChanged(string value)
    {
        _themeService.SetTheme(value);
        _applicationSettingsService.SetThemeSetting(value);
    }

    #endregion
}