using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Couresach.Windows;

public partial class MoreInfoWareItemWindow : Window
{
    private Ware item;
    private const string _imageSource = 
        "C:\\Users\\Константин\\Desktop\\Coursework\\Couresach\\Couresach\\Images";

    private OpenFileDialog _img;
    private string filePath;
    public MoreInfoWareItemWindow(Ware item)
    {
        InitializeComponent();
        CategoryComboBox.ItemsSource = DatabaseControl.GetAllCategories();
        ManComboBox.ItemsSource = DatabaseControl.GetAllMans();
        this.item = item;
        ManComboBox.SelectedValue = item.ManufacturerEntity?.Name;
        ItemName.Text = item.ManufacturerEntity?.Name;
        // ItemName.Text = item.Item;
        ItemImage.Source = new BitmapImage(new Uri(item.Image));
        ItemDescription.Text = item.Description;
        ItemQuantity.Text = item.Quantity.ToString();
        ItemPrice.Text = item.Price.ToString();
        CategoryComboBox.SelectedValue = item.CategoryEntity?.Name;
    }

    private void EditWareItem_Button(object sender, RoutedEventArgs e)
    {
        if (_img != null)
        {
            filePath = System.IO.Path.Combine(_imageSource, _img.SafeFileName);
            File.Copy(_img.FileName, filePath, true);
        }
        else
        {
            filePath = item.Image;
        }
        var manId = DatabaseControl.GetManByName(ManComboBox.SelectedValue.ToString()).Id;
        var categoryId = DatabaseControl.GetCategoryByName(CategoryComboBox.SelectedValue.ToString()).Id;

        DatabaseControl.EditWareItem(item.Item, ItemName.Text, filePath,
            ItemDescription.Text, int.Parse(ItemQuantity.Text),
            categoryId , int.Parse(ItemPrice.Text), manId);
        
        Close();
    }

    private void EditPhoto_Button(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            _img = openFileDialog;
        }
    }
}