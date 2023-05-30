using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Couresach.Windows;

namespace Couresach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void AuthLogin_Button(object sender, RoutedEventArgs e)
        {
            var user = DatabaseControl.GetUserByEmail(LoginAuth.Text);
            if (user == null)
            {
                Window AuthorizationGuard = new AuthorizationGuard();
                AuthorizationGuard.Show();
            }
            else
            {
                if (user.Password == PasswordAuth.Password)
                {
                    Window WorkSpaceWindow = new WorkSpaceWindow(user);
                    WorkSpaceWindow.Show();
                    Close();
                }
                else
                {
                    Window AuthorizationGuard = new AuthorizationGuard();
                    AuthorizationGuard.Show();
                }
            }
        }

        private void AuthRegistration_Button(object sender, RoutedEventArgs e)
        {
            Window RegistrationWindow = new RegistrationWindow();
            RegistrationWindow.Show();
            Close();
        }
    }
}