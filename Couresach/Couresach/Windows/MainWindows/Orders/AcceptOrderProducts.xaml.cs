using System.Runtime.CompilerServices;
using System.Windows;

namespace Couresach.Windows;

public partial class AcceptOrderProducts : Window
{
    private bool flag;
    private int id;
    private User user;
    public AcceptOrderProducts(int id, bool flag, User user)
    {
        InitializeComponent();
        this.flag = flag;
        this.user = user;
        this.id = id;
    }
    
    

    private void BackOutOrder_Button(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void CreateOrder_Button(object sender, RoutedEventArgs e)
    {
        if (flag)
        {
            DatabaseControl.CreateOrder(new Order()
            {
                Description = DescriptionOrder.Text,
                User_id = user.Id,
                Status = "В обработке",
                Ware_id = id,
                Service_id = null,
            });
            DatabaseControl.decQuantityOfItemById(id);
        }
        else
        {
            DatabaseControl.CreateOrder(new Order()
            {
                Description = DescriptionOrder.Text,
                User_id = user.Id,
                Status = "В обработке",
                Ware_id = null,
                Service_id = id,
            });
        }
        Close();
    }
}