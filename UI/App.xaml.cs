using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Prism.Ioc;
using Serilog;
using Squirrel;
using UI.Infrastructure;
using UI.Views;
using Settings = UI.Properties.Settings;

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

        containerRegistry.RegisterSingleton<ILogger>(() => Log.Logger);
        containerRegistry.RegisterSingleton<IApplicationSettingsService>(() =>
            new ApplicationSettingsService(Settings.Default));
    }

    protected override Window CreateShell() => new Shell();

    protected override void OnStartup(StartupEventArgs e)
    {
        InitializeLogger();
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        base.OnStartup(e);

        SquirrelAwareApp.HandleEvents(
            OnAppInstall,
            onAppUninstall: OnAppUninstall,
            onEveryRun: OnAppRun);
    }

    private void InitializeLogger()
    {
        using var log = new LoggerConfiguration()
            .WriteTo.File("RailSurveil.txt", rollOnFileSizeLimit: true, retainedFileTimeLimit: TimeSpan.FromDays(2))
            .CreateLogger();
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) =>
        Log.Error(e.ExceptionObject as Exception, "Unhandled Exception");

    private void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
    {
        try
        {
            Log.Information("OnAppRun!");
            if (firstRun)
            {
                tools.CreateShortcutForThisExe();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void OnAppUninstall(SemanticVersion version, IAppTools tools) => tools.RemoveShortcutForThisExe();

    private void OnAppInstall(SemanticVersion version, IAppTools tools) => tools.SetProcessAppUserModelId();

    #endregion
}