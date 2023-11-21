using System;
using System.Threading.Tasks;

namespace UI.Infrastructure;

public interface IAutoUpdateService
{
    #region Methods

    Task<bool> UpdateExists(IProgress<int> progressReport = null);

    Task UpdateLatestAndRestart(IProgress<int> progressReport = null);

    string GetCurrentApplicationVersion();

    #endregion
}