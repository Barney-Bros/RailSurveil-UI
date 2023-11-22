using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UI.Infrastructure;

namespace UI.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    #region Fields

    private readonly IAutoUpdateService _autoUpdateService;

    [ObservableProperty]
    private bool _updateExists;

    [ObservableProperty]
    private int _updatePercentage;

    #endregion

    #region Constructors

    public ShellViewModel(IAutoUpdateService autoUpdateService)
    {
        _autoUpdateService = autoUpdateService;
        AppVersion = $"v{autoUpdateService.GetCurrentApplicationVersion()[..^2]}";
    }

    #endregion

    #region Properties

    public string AppVersion { get; }

    #endregion

    #region Methods

    [RelayCommand]
    public async Task Initialize() => UpdateExists = await _autoUpdateService.UpdateExists();

    [RelayCommand]
    public async Task PerformUpdate() =>
        await _autoUpdateService.UpdateLatestAndRestart(new Progress<int>(x => UpdatePercentage = x));

    #endregion
}