using System.Windows;

namespace Couresach.Windows;

public partial class AddNewWareItemWindow : Window
{
    public AddNewWareItemWindow()
    {
        InitializeComponent();
        CategoryComboBox.ItemsSource = DatabaseControl.GetAllCategories();
        ManComboBox.ItemsSource = DatabaseControl.GetAllMans();
    }

    private void CreateNewWareItem_Button(object sender, RoutedEventArgs e)
    {
        DatabaseControl.CreateNewWareItem(new Ware
        {
            Item = ItemName.Text,
            Image = null,
            Description = ItemDescription.Text,
            Quantity = int.Parse(ItemQuantity.Text),
            Category_id = DatabaseControl.GetCategoryByName(CategoryComboBox.SelectedValue.ToString()).Id,
            Price = int.Parse(ItemPrice.Text),
            Manufacturer_id = DatabaseControl.GetManByName(ManComboBox.SelectedValue.ToString()).Id,
        });
        Close();
    }
}