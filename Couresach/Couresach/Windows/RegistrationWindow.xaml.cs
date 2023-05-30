using System;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;

namespace Couresach;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }

    private void Registration_Button(object sender, RoutedEventArgs e)
    {
        var user = DatabaseControl.GetUserByEmail(EmailReg.Text);
        if (user == null)
        {
            EmailEx.Visibility = Visibility.Hidden;
            if (PasswordlReg.Text.Length >= 4)
            {
                var role = DatabaseControl.GetRoleById(2);
                DatabaseControl.RegistrationUser(new User
                {
                    Email = EmailReg.Text,
                    Password = PasswordlReg.Text,
                    Full_name = Full_NameReg.Text,
                    Phone_number = Phone_NumberReg.Text,
                    Date_reg = DateOnly.FromDateTime(DateTime.Now),
                    Role_id = role.Id,
                });
                Window AuthWindow = new AuthWindow();
                AuthWindow.Show();
                Close();
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