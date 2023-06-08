using System.IO;
using System.Net;
using System.Windows;
using Microsoft.Win32;

namespace Couresach.Windows;

public partial class AddNewWareItemWindow : Window
{
    private const string _imageSource = 
        "C:\\Users\\Константин\\Desktop\\Coursework\\Couresach\\Couresach\\Images";

    private OpenFileDialog _img;
    private string filePath = null;
    public AddNewWareItemWindow()
    {
        InitializeComponent();
        CategoryComboBox.ItemsSource = DatabaseControl.GetAllCategories();
        ManComboBox.ItemsSource = DatabaseControl.GetAllMans();
    }

    private void CreateNewWareItem_Button(object sender, RoutedEventArgs e)
    {
        if (_img != null)
        {
            filePath = System.IO.Path.Combine(_imageSource, _img.SafeFileName);
            File.Copy(_img.FileName, filePath, true);
        }
        DatabaseControl.CreateNewWareItem(new Ware
        {
            Item = ItemName.Text,
            Image = filePath,
            Description = ItemDescription.Text,
            Quantity = int.Parse(ItemQuantity.Text),
            Category_id = DatabaseControl.GetCategoryByName(CategoryComboBox.SelectedValue.ToString()).Id,
            Price = int.Parse(ItemPrice.Text),
            Manufacturer_id = DatabaseControl.GetManByName(ManComboBox.SelectedValue.ToString()).Id,
        });
        Close();
    }

    private void AddImage_Button(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            _img = openFileDialog;
        }
        
    }
}