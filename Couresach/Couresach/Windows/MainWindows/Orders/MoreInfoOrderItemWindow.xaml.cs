using System.Windows;

namespace Couresach.Windows.MainWindows.Orders;

public partial class MoreInfoOrderItemWindow : Window
{
    private Order item;
    public MoreInfoOrderItemWindow(Order item)
    {
        InitializeComponent();
        this.item = item;
        FIOUser.Text = item.UsersEntity?.Full_name;
        OrderStatus.Text = item.Status;
        OrderDescription.Text = item.Description;
    }

    private void EditOrderStatus_Button(object sender, RoutedEventArgs e)
    {
        DatabaseControl.EditOrderStatus(item.UsersEntity.Id, OrderStatus.Text);
        Close();
    }
}