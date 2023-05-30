using System.Runtime.CompilerServices;
using System.Windows;
using Couresach.Windows;

namespace Couresach;

public partial class WorkSpaceWindow : Window
{
    private User user;
    public WorkSpaceWindow(User user)
    {
        InitializeComponent();
        this.user = user;
        var role = DatabaseControl.GetRoleById(user.Role_id);
        if (role.Value == "USER")
        {
            OrderTabItem.Visibility = Visibility.Hidden;
            WareTabItem.Visibility = Visibility.Hidden;
            RegNewUserForAdmin.Visibility = Visibility.Hidden;
        }else if (role.Value == "MASTER")
        {
            RegNewUserForAdmin.Visibility = Visibility.Hidden;
        }
    }

    private void ExitOfAccount_Button(object sender, RoutedEventArgs e)
    {
        Window AuthWindow = new AuthWindow();
        AuthWindow.Show();
        Close();
    }

    private void RegNewUser_Button(object sender, RoutedEventArgs e)
    {
        Window RegistrationForAdminWindow = new RegistrationForAdminWindow();
        RegistrationForAdminWindow.Show();
    }

    private void DeleteAccount_Button(object sender, RoutedEventArgs e)
    {
        DatabaseControl.DeleteUser(user);
        Window AuthWindow = new AuthWindow();
        AuthWindow.Show();
        Close();
    }
}