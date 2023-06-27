using System;
using System.Windows;

namespace Couresach.Windows;

public partial class RegistrationForAdminWindow : Window
{
    public RegistrationForAdminWindow()
    {
        InitializeComponent();
        RoleListBox.ItemsSource = DatabaseControl.GetAllRoles();
    }

    private void RegistrationForAdmin_Button(object sender, RoutedEventArgs e)
    {
        var user = DatabaseControl.GetUserByEmail(EmailReg.Text);
        if (user == null)
        {
            EmailEx.Visibility = Visibility.Hidden;
            if (PasswordlReg.Text.Length >= 4)
            {
                if (Full_NameReg.Text != "" && Phone_NumberReg.Text != "" && RoleListBox.SelectedValue != null)
                {
                    var role = DatabaseControl.GetRoleByValue(RoleListBox.SelectedValue.ToString());
                    DatabaseControl.RegistrationUser(new User
                    {
                        Email = EmailReg.Text,
                        Password = PasswordlReg.Text,
                        Full_name = Full_NameReg.Text,
                        Phone_number = Phone_NumberReg.Text,
                        Date_reg = DateOnly.FromDateTime(DateTime.Now),
                        Role_id = role.Id,
                    });
                    Close();
                }
                else
                {
                    MessageBox.Show("Все поля обязательные!");
                }
                
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