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
    private IApplicationSettingsService _applicationSettingsService;
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
        _applicationSettingsService = Substitute.For<IApplicationSettingsService>();
        _themeService.ThemeNames.Returns(AvailableThemeNames);
        _themeService.CurrentThemeName.Returns(AvailableThemeNames.First());
        _viewModelUnderTest = new SettingsViewModel(_themeService, _autoUpdateService, _applicationSettingsService);
    }

    [TestMethod]
    public void SettingsViewModelTests_ChangeThemeName_ServiceIsCalled()
    {
        _viewModelUnderTest.CurrentTheme = AvailableThemeNames.First();
        _themeService.Received().SetTheme(AvailableThemeNames.First());
        _applicationSettingsService.Received().SetThemeSetting(AvailableThemeNames.First());
    }

    [TestMethod]
    public void SettingsViewModelTests_ViewModelIsCreated_ThemeIsSet()
    {
        var themeService = Substitute.For<IThemeService>();
        var autoUpdateService = Substitute.For<IAutoUpdateService>();
        var applicationSettingsService = Substitute.For<IApplicationSettingsService>();
        const string expected = "42Theme";
        applicationSettingsService.GetThemeSetting().Returns(expected);
        themeService.CurrentThemeName.Returns(expected);
        themeService.ThemeNames.Returns(AvailableThemeNames);

        var viewModelUnderTest = new SettingsViewModel(themeService, autoUpdateService, applicationSettingsService);
        Assert.AreEqual(expected, viewModelUnderTest.CurrentTheme);
    }

    #endregion
}