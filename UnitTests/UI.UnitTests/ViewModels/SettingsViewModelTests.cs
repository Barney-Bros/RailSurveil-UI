using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UI.Infrastructure;
using UI.ViewModels;

namespace UI.UnitTests.ViewModels;

[TestClass]
public class SettingsViewModelTests
{
    #region Fields

    private static readonly string[] AvailableThemeNames = { "Light", "Dark" };
    private IAutoUpdateService _autoUpdateService;
    private IThemeService _themeService;
    private SettingsViewModel _viewModelUnderTest;

    #endregion

    #region Methods

    [TestInitialize]
    public void Initialize()
    {
        _themeService = Substitute.For<IThemeService>();
        _autoUpdateService = Substitute.For<IAutoUpdateService>();
        _themeService.ThemeNames.Returns(AvailableThemeNames);
        _viewModelUnderTest = new SettingsViewModel(_themeService, _autoUpdateService);
    }

    [TestMethod]
    public void SettingsViewModelTests_ChangeThemeName_ServiceIsCalled()
    {
        _viewModelUnderTest.CurrentTheme = AvailableThemeNames.First();
        _themeService.Received().SetTheme(AvailableThemeNames.First());
    }

    #endregion
}