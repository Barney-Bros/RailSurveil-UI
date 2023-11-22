using System.Diagnostics.CodeAnalysis;
using UI.Properties;

namespace UI.Infrastructure;

[ExcludeFromCodeCoverage]
internal class ApplicationSettingsService : IApplicationSettingsService
{
    #region Fields

    private readonly Settings _settings;

    #endregion

    #region Constructors

    public ApplicationSettingsService(Settings settings) => _settings = settings;

    #endregion

    #region Methods

    public string GetThemeSetting() => _settings.ThemeName;

    public void SetThemeSetting(string themeName)
    {
        _settings.ThemeName = themeName;
        _settings.Save();
    }

    #endregion
}