using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Prism.Ioc;
using Squirrel;
using UI.Infrastructure;
using UI.Views;

namespace UI;

[ExcludeFromCodeCoverage]
public partial class App
{
    #region Methods

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IThemeService, ThemeService>();
        containerRegistry.RegisterSingleton<IAutoUpdateService>(() =>
            new AutoUpdateService("https://github.com/Barney-Bros/RailSurveil-UI"));
    }

    protected override Window CreateShell() => new Shell();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        SquirrelAwareApp.HandleEvents(
            OnAppInstall,
            onAppUninstall: OnAppUninstall,
            onEveryRun: OnAppRun);
    }

    private void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
    {
        if (firstRun)
        {
            tools.CreateShortcutForThisExe();
        }
    }

    private void OnAppUninstall(SemanticVersion version, IAppTools tools) => tools.RemoveShortcutForThisExe();

    private void OnAppInstall(SemanticVersion version, IAppTools tools) => tools.SetProcessAppUserModelId();

    #endregion
}