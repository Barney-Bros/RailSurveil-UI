using System.Diagnostics.CodeAnalysis;
using MahApps.Metro.Controls;

namespace UI.Views;

[ExcludeFromCodeCoverage]
public partial class Shell
{
    #region Constructors

    public Shell() => InitializeComponent();

    #endregion

    #region Methods

    private void OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
    {
        if (sender is HamburgerMenu hamburgerMenu)
        {
            //hamburgerMenu.Content = args.InvokedItem;
        }
    }

    #endregion
}