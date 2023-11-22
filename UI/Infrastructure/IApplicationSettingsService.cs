namespace UI.Infrastructure;

public interface IApplicationSettingsService
{
    #region Methods

    string GetThemeSetting();
    void SetThemeSetting(string themeName);

    #endregion
}