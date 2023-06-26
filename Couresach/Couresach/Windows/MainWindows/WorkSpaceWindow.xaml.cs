using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Couresach.Windows;
using Couresach.Windows.Category;
using Couresach.Windows.MainWindows.Orders;
using Couresach.Windows.Manufacturer;

namespace Couresach;

public partial class WorkSpaceWindow : Window
{
    private User user;
    private List<Ware> ware;
    private bool flagOrderWareOrService;
    private List<Ware> WareByTypes;
    private List<Ware> WareByMan;
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
            ManAndCategoryTabItem.Visibility = Visibility.Hidden;
        }else if (role.Value == "MASTER")
        {
            RegNewUserForAdmin.Visibility = Visibility.Hidden;
            WareTabItem.Visibility = Visibility.Hidden;
            ManAndCategoryTabItem.Visibility = Visibility.Hidden;
        }

        FIO.Text = user.Full_name;
        Phone_number.Text = user.Phone_number;
        Email.Text = user.Email;

        WareDataGrid.ItemsSource = DatabaseControl.GetAllWare();
        OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
        CategoryDataGrid.ItemsSource = DatabaseControl.GetAllCategories();
        ManDataGrid.ItemsSource = DatabaseControl.GetAllMans();
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
        ScrollViewerOrders.Visibility = Visibility.Hidden;
        OrdersNotFound.Visibility = Visibility.Hidden;
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
            EditProfile.Visibility = Visibility.Hidden;
        }
        else
        {
            ScrollViewerOrders.Visibility = Visibility.Visible;
            OrdersNotFound.Visibility = Visibility.Visible;
            OrdersProfile.Visibility = Visibility.Visible;
            InfoProfile.Visibility = Visibility.Hidden;
            EditProfile.Visibility = Visibility.Hidden;
            ScrollViewerEdit.Visibility = Visibility.Hidden;
        }
        
    }

    private void Back_Button(object sender, RoutedEventArgs e)
    {
        InfoProfile.Visibility = Visibility.Visible;
        ScrollViewerOrders.Visibility = Visibility.Hidden;
        OrdersNotFound.Visibility = Visibility.Hidden;
        EditProfile.Visibility = Visibility.Hidden;
    }

    private void SelectProducts_Item(object sender, RoutedEventArgs e)
    {
        TypeText.Visibility = Visibility.Visible;
        TypeFilter.Visibility = Visibility.Visible;
            
        ManText.Visibility = Visibility.Visible;
        ManFilter.Visibility = Visibility.Visible;

        ware = DatabaseControl.GetAllForProductsWare();
        WareListBoxForProduct.ItemsSource = ware;
        List<Category> categories = DatabaseControl.GetAllCategories();
        List<Manufacturer> mans = DatabaseControl.GetAllMans();
        TypeFilter.ItemsSource = categories;
        ManFilter.ItemsSource = mans;
        WareListBoxForProduct.Visibility = Visibility.Visible;
        ServiceListBoxForProduct.Visibility = Visibility.Hidden;
    }

    private void SelectServices_Item(object sender, RoutedEventArgs e)
    {
        ServiceListBoxForProduct.Visibility = Visibility.Visible;
        ServiceListBoxForProduct.ItemsSource = DatabaseControl.GetAllServices();
            
        TypeText.Visibility = Visibility.Hidden;
        TypeFilter.Visibility = Visibility.Hidden;
            
        ManText.Visibility = Visibility.Hidden;
        ManFilter.Visibility = Visibility.Hidden;
        
        WareListBoxForProduct.Visibility = Visibility.Hidden;
    }

    private void SelectType_Item(object sender, RoutedEventArgs e)
    {
        if (TypeFilter.SelectedValue != null)
        {
            WareByTypes = DatabaseControl.GetAllByTypeWare(TypeFilter.SelectedValue.ToString());
            WareListBoxForProduct.ItemsSource = null;
            WareListBoxForProduct.ItemsSource = setWares(WareByTypes, WareByMan);
        }
    }

    private void AddNewWareItem_Button(object sender, RoutedEventArgs e)
    {
        Window AddNewWareItemWindow = new AddNewWareItemWindow();
        AddNewWareItemWindow.Owner = this;
        AddNewWareItemWindow.ShowDialog();
        WareDataGrid.ItemsSource = null;
        WareDataGrid.ItemsSource = DatabaseControl.GetAllWare();
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
        Ware tempItem = WareDataGrid.SelectedItem as Ware;
        Ware item = DatabaseControl.GetWareById(tempItem.Id);
        if (item != null)
        {
            Window MoreInfoWareItemWindow = new MoreInfoWareItemWindow(item);
            MoreInfoWareItemWindow.Owner = this;
            MoreInfoWareItemWindow.ShowDialog();
        }
        
        WareDataGrid.ItemsSource = null;
        WareDataGrid.ItemsSource = DatabaseControl.GetAllWare();
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

    private void MoreInfoOrder_Button(object sender, RoutedEventArgs e)
    {
        Order item = OrderDataGrid.SelectedItem as Order;
        if (item != null)
        {
            Window MoreInfoOrderProductsWindow = new MoreInfoOrderItemWindow(item);
            MoreInfoOrderProductsWindow.Owner = this;
            MoreInfoOrderProductsWindow.ShowDialog();
        }

        OrderDataGrid.ItemsSource = null;
        OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
    }

    private void DeleteSelectedCategoryItemInDataGrid_Button(object sender, RoutedEventArgs e)
    {
        
    }

    private void DeleteSelectedManItemInDataGrid_Button(object sender, RoutedEventArgs e)
    {
        
    }

    private void AddNewMan_Button(object sender, RoutedEventArgs e)
    {
        Window AddNewManufacturerWindow = new AddNewManufacturerWindow();
        AddNewManufacturerWindow.Owner = this;
        AddNewManufacturerWindow.ShowDialog();

        ManDataGrid.ItemsSource = null;
        ManDataGrid.ItemsSource = DatabaseControl.GetAllMans();
    }

    private void AddNewCategory_Button(object sender, RoutedEventArgs e)
    {
        Window AddNewCategoryWindow = new AddNewCategoryWindow();
        AddNewCategoryWindow.Owner = this;
        AddNewCategoryWindow.ShowDialog();

        CategoryDataGrid.ItemsSource = null;
        CategoryDataGrid.ItemsSource = DatabaseControl.GetAllCategories();
    }

    private void CreateOrder_Button(object sender, RoutedEventArgs e)
    {
        
        if (WareItemRadio.IsChecked == true)
        {
            var orderSelectedItem = WareListBoxForProduct.SelectedItem as Ware;
            if (orderSelectedItem != null)
            {
                flagOrderWareOrService = true;
                Window AcceptOrderProducts = new AcceptOrderProducts(orderSelectedItem.Id, flagOrderWareOrService, user);
                AcceptOrderProducts.Owner = this;
                AcceptOrderProducts.ShowDialog();
                OrderDataGrid.ItemsSource = null;
                OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
                OrderEx.Visibility = Visibility.Hidden;
            }
            else
            {
                OrderEx.Visibility = Visibility.Visible;
            }
        }
        else if(ServiceItemRadio.IsChecked == true)
        {
            var orderSelectedItem = ServiceListBoxForProduct.SelectedItem as Service;
            if (orderSelectedItem != null)
            {
                flagOrderWareOrService = true;
                Window AcceptOrderProducts = new AcceptOrderProducts(orderSelectedItem.Id, flagOrderWareOrService, user);
                AcceptOrderProducts.Owner = this;
                AcceptOrderProducts.ShowDialog();
                OrderDataGrid.ItemsSource = null;
                OrderDataGrid.ItemsSource = DatabaseControl.GetAllOrders();
                OrderEx.Visibility = Visibility.Hidden;
                WareByMan = null;
                WareByTypes = null;
                TypeFilter.SelectedValue = null;
                ManFilter.SelectedValue = null;
            }
            else
            {
                OrderEx.Visibility = Visibility.Visible;
            }
        }
        else
        {
            OrderEx.Visibility = Visibility.Visible;
        }
    }

    private void ManFilter_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (ManFilter.SelectedValue != null)
        {
            WareByMan = DatabaseControl.GetAllByManWare(ManFilter.SelectedValue.ToString());
            WareListBoxForProduct.ItemsSource = null;
            WareListBoxForProduct.ItemsSource = setWares(WareByTypes, WareByMan);
        }
    }

    private List<Ware> setWares(List<Ware> WareByTypes, List<Ware> WareByMan)
    {
        if (WareByTypes == null)
        {
            return WareByMan;
        }
        else if(WareByMan == null)
        {
            return WareByTypes;
        }
        else
        {
            return DatabaseControl.GetAllByTypeAndManWare(TypeFilter.SelectedValue.ToString(),
                ManFilter.SelectedValue.ToString());
        }
    }
}