using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.ViewModels;

namespace UI.UnitTests.ViewModels;

[TestClass]
public class ShellViewModelTests
{
    #region Fields

    private ShellViewModel _viewModelUnderTest;

    #endregion

    #region Methods

    [TestInitialize]
    public void Initialize() => _viewModelUnderTest = new ShellViewModel();

    #endregion
}