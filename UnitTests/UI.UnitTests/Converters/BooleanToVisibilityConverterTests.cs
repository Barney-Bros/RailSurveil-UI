using System.Windows;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Converters;

namespace UI.UnitTests.Converters;

[TestClass]
public class BooleanToVisibilityConverterTests
{
    #region Fields

    private BooleanToVisibilityConverter _converterUnderTest;

    #endregion

    #region Methods

    [TestInitialize]
    public void Initialize() => _converterUnderTest = new BooleanToVisibilityConverter();

    [TestMethod]
    [DataRow(true, Visibility.Visible)]
    [DataRow(false, Visibility.Collapsed)]
    public void BooleanToVisibilityConverterTests_Convert_ReceivesConvertedValue(bool boolean, Visibility expected) =>
        Assert.AreEqual(expected, _converterUnderTest.Convert(boolean, null, null, null));

    [TestMethod]
    public void BooleanToVisibilityConverterTests_ConvertBack_ReturnsBindingDoNothing() =>
        Assert.AreEqual(Binding.DoNothing, _converterUnderTest.ConvertBack(null, null, null, null));

    #endregion
}