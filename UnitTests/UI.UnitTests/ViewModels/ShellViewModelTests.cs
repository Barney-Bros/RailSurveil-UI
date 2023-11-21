using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UI.Infrastructure;
using UI.ViewModels;

namespace UI.UnitTests.ViewModels;

[TestClass]
public class ShellViewModelTests
{
    #region Fields

    private const string TestVersion = "42.42.42.0";
    private IAutoUpdateService _autoUpdateService;

    private ShellViewModel _viewModelUnderTest;

    #endregion

    #region Methods

    [TestInitialize]
    public void Initialize()
    {
        _autoUpdateService = Substitute.For<IAutoUpdateService>();
        _autoUpdateService.GetCurrentApplicationVersion().Returns(TestVersion);
        _viewModelUnderTest = new ShellViewModel(_autoUpdateService);
    }

    [TestMethod]
    public void ShellViewModelTests_Version_InitializedByAutoUpdateService() =>
        Assert.AreEqual($"v{TestVersion[..^2]}", _viewModelUnderTest.AppVersion);

    #endregion
}