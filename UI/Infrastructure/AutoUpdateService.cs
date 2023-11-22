using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using Squirrel;
using Squirrel.Sources;

namespace UI.Infrastructure;

[ExcludeFromCodeCoverage]
public class AutoUpdateService : IAutoUpdateService
{
    #region Fields

    private readonly string _updateSourcePath;

    #endregion

    #region Constructors

    public AutoUpdateService(string updateSourcePath) => _updateSourcePath = updateSourcePath;

    #endregion

    #region Methods

    public async Task<bool> UpdateExists(IProgress<int> progressReport = null)
    {
        using var updateManager = new UpdateManager(new GithubSource(_updateSourcePath, string.Empty, true));
        if (!updateManager.IsInstalledApp)
        {
            return false;
        }

        var update = await updateManager.CheckForUpdate(progress: x => progressReport?.Report(x));
        return update is not null;
    }

    public async Task UpdateLatestAndRestart(IProgress<int> progressReport = null)
    {
        using var updateManager = new UpdateManager(new GithubSource(_updateSourcePath, string.Empty, true));
        if (!updateManager.IsInstalledApp)
        {
            return;
        }

        var update = await updateManager.UpdateApp(x => progressReport?.Report(x));
        if (update is not null)
        {
            UpdateManager.RestartApp();
        }
    }

    public string GetCurrentApplicationVersion() => Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    #endregion
}