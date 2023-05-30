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

        FIO.Text = user.Full_name;
        Phone_number.Text = user.Phone_number;
        Email.Text = user.Email;
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

    private void EditProfile_Button(object sender, RoutedEventArgs e)
    {
        EditProfile.Visibility = Visibility.Visible;
        InfoProfile.Visibility = Visibility.Hidden;
        ScrollViewerEdit.Visibility = Visibility.Visible;
    }

    private void AcceptEditProfile_Button(object sender, RoutedEventArgs e)
    {
        var userFlag = DatabaseControl.GetUserByEmail(EmailEdit.Text);
        
        if (userFlag == null)
        {
            EmailEx.Visibility = Visibility.Hidden;
            if (PasswordEdit.Text.Length >= 4 && PasswordEdit.Text == PasswordEditReplay.Text)
            {
                var editUser = DatabaseControl.EditUser(EmailEdit.Text, PasswordEdit.Text, Full_NameEdit.Text, Phone_NumberEdit.Text, user.Email);
                if (editUser != null)
                {
                    user = editUser;
                    FIO.Text = user.Full_name;
                    Phone_number.Text = user.Phone_number;
                    Email.Text = user.Email;
                }
                EditProfile.Visibility = Visibility.Hidden;
                InfoProfile.Visibility = Visibility.Visible;
                ScrollViewerEdit.Visibility = Visibility.Hidden;
                EmailEdit.Text = "";
                PasswordEdit.Text = "";
                PasswordEditReplay.Text = "";
                Full_NameEdit.Text = "";
                Phone_NumberEdit.Text = "";
            }
            else
            {
                PasswordEx.Visibility = Visibility.Visible;
            }
            
        }
        else
        {
            EmailEx.Visibility = Visibility.Visible;
        }
    }
}