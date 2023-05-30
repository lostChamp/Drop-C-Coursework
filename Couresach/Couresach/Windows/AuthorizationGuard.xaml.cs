using System.Windows;

namespace Couresach.Windows;

public partial class AuthorizationGuard : Window
{
    public AuthorizationGuard()
    {
        InitializeComponent();
    }

    private void Close_GuardWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }
}