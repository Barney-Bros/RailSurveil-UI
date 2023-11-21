using System;

namespace UI.Infrastructure;

public interface IThemeService
{
    #region Properties

    string[] ThemeNames { get; }
    string CurrentThemeName { get; }

    #endregion

    #region Methods

    event EventHandler<string> ThemeChanged;
    void SetTheme(string themeName);

    #endregion
}