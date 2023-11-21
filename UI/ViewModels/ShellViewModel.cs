using CommunityToolkit.Mvvm.ComponentModel;
using UI.Infrastructure;

namespace UI.ViewModels;

public class ShellViewModel : ObservableObject
{
    #region Constructors

    public ShellViewModel(IAutoUpdateService autoUpdateService) =>
        AppVersion = $"v{autoUpdateService.GetCurrentApplicationVersion()[..^2]}";

    #endregion

    #region Properties

    public string AppVersion { get; }

    #endregion
}