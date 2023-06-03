using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Couresach.Windows;

public partial class MoreInfoWareItemWindow : Window
{
    private Ware item;
    public MoreInfoWareItemWindow(Ware item)
    {
        InitializeComponent();
        CategoryComboBox.ItemsSource = DatabaseControl.GetAllCategories();
        ManComboBox.ItemsSource = DatabaseControl.GetAllMans();
        this.item = item;
        ItemName.Text = item.Item;
        if (this.item.Image != null)
        {
            ItemImage.Source = new BitmapImage(new Uri(item.Image));
        }
        else
        {
            ItemImage.Source = new BitmapImage(new Uri("C:\\Users\\Константин\\Desktop\\Coursework\\Couresach\\Couresach\\Images\\no_img.jpg"));
        }

        ItemDescription.Text = item.Description;
        
        ItemPrice.Text = item.Price.ToString();
    }
}