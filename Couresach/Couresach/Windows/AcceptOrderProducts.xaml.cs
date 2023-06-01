using System.Windows;

namespace Couresach.Windows;

public partial class AcceptOrderProducts : Window
{
    public AcceptOrderProducts()
    {
        InitializeComponent();
    }

    private void CancelOrder_Button(object sender, RoutedEventArgs e)
    {
        Close();
    }
}