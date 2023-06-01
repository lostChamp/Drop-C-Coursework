using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Couresach.Windows;

namespace Couresach;

public partial class WorkSpaceWindow : Window
{
    private User user;
    private List<Ware> ware;
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

        WareDataGrid.ItemsSource = DatabaseControl.GetAllWare();
        OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
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

    private void GetAllOrdersProfile_Button(object sender, RoutedEventArgs e)
    {
        List<Order> orders = DatabaseControl.GetAllOrdersByUserId(user.Id);
        if (orders.Count != 0)
        {
            OrdersListBox.ItemsSource = DatabaseControl.GetAllOrdersByUserId(user.Id);
            ScrollViewerOrders.Visibility = Visibility.Visible;
            OrdersProfile.Visibility = Visibility.Visible;
            OrdersListBox.Visibility = Visibility.Visible;
            InfoProfile.Visibility = Visibility.Hidden;
        }
        else
        {
            ScrollViewerOrders.Visibility = Visibility.Visible;
            OrdersNotFound.Visibility = Visibility.Visible;
            OrdersProfile.Visibility = Visibility.Visible;
            InfoProfile.Visibility = Visibility.Hidden;
        }
        
    }

    private void Back_Button(object sender, RoutedEventArgs e)
    {
        InfoProfile.Visibility = Visibility.Visible;
        ScrollViewerOrders.Visibility = Visibility.Hidden;
        OrdersNotFound.Visibility = Visibility.Hidden;
    }

    // private void NewOrder_Button(object sender, RoutedEventArgs e)
    // {
    //     DatabaseControl.CreateOrder(new Order()
    //     {
    //         User_id = user.Id,
    //         Description = Description.Text
    //     });
    // }

    private void SelectProducts_Item(object sender, RoutedEventArgs e)
    {
        TypeText.Visibility = Visibility.Visible;
        TypeFilter.Visibility = Visibility.Visible;
            
        ManText.Visibility = Visibility.Visible;
        ManFilter.Visibility = Visibility.Visible;
            
        PriceText.Visibility = Visibility.Visible;
        PriceFilter.Visibility = Visibility.Visible;

        ware = DatabaseControl.GetAllForProductsWare();
        WareListBoxForProduct.ItemsSource = ware;
        List<Ware> categoriesAndMan = DatabaseControl.GetUniqueValues();
        TypeFilter.ItemsSource = categoriesAndMan;
        ManFilter.ItemsSource = categoriesAndMan;
        WareListBoxForProduct.Visibility = Visibility.Visible;
    }

    private void SelectServices_Item(object sender, RoutedEventArgs e)
    {
        ServiceListBoxForProduct.Visibility = Visibility.Visible;
        ServiceListBoxForProduct.ItemsSource = DatabaseControl.GetAllServices();
            
        TypeText.Visibility = Visibility.Hidden;
        TypeFilter.Visibility = Visibility.Hidden;
            
        ManText.Visibility = Visibility.Hidden;
        ManFilter.Visibility = Visibility.Hidden;
            
        PriceText.Visibility = Visibility.Hidden;
        PriceFilter.Visibility = Visibility.Hidden;
        WareListBoxForProduct.Visibility = Visibility.Hidden;
    }

    private void SelectType_Item(object sender, RoutedEventArgs e)
    {
        if (TypeFilter.SelectedValue != null)
        {
            ware = DatabaseControl.GetWareByCategory(TypeFilter.SelectedValue.ToString());
            WareListBoxForProduct.ItemsSource = ware;
        }
    }

    private void SelectProductForOrder_Item(object sender, RoutedEventArgs e)
    {
        Window AcceptOrderProducts = new AcceptOrderProducts();
        AcceptOrderProducts.Show();
    }

    private void AddNewWareItem_Button(object sender, RoutedEventArgs e)
    {
        Window AddNewWareItemWindow = new AddNewWareItemWindow();
        AddNewWareItemWindow.Show();
    }

    private void SearchForWareByItem_Button(object sender, RoutedEventArgs e)
    {
        if (SearchForWare.Text != "")
        {
            WareDataGrid.ItemsSource = DatabaseControl.GetWareByItem(SearchForWare.Text);
        }
        else
        {
            WareDataGrid.ItemsSource = DatabaseControl.GetAllWare();
        }
    }

    private void SearchForOrdersBFullName_Button(object sender, KeyEventArgs e)
    {
        if (SearchForOrders.Text != "")
        {
            OrderDataGrid.ItemsSource = DatabaseControl.GetOrderByUserName(SearchForOrders.Text);
        }
        else
        {
            OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
        }
    }

    private void MoreInfoWareItem_Button(object sender, RoutedEventArgs e)
    {
        Window MoreInfoWareItemWindow = new MoreInfoWareItemWindow();
        MoreInfoWareItemWindow.Show();
    }

    private void DeleteSelectedWareItemInDataGrid_Button(object sender, RoutedEventArgs e)
    {
        Ware item = WareDataGrid.SelectedItem as Ware;
        if (item != null)
        {
            DatabaseControl.DeleteWareItem(item);
            WareDataGrid.ItemsSource = DatabaseControl.GetAllWare();
        }
    }

    private void DeleteSelectedOrderInDataGrid_Button(object sender, RoutedEventArgs e)
    {
        Order item = OrderDataGrid.SelectedItem as Order;
        if (item != null)
        {
            DatabaseControl.DeleteOrderItem(item);
            OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
        }
    }
}