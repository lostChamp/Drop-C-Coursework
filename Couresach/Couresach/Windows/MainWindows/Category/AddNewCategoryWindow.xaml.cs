using System.Windows;

namespace Couresach.Windows.Category;

public partial class AddNewCategoryWindow : Window
{
    public AddNewCategoryWindow()
    {
        InitializeComponent();
    }

    private void AddNewCategory_Button(object sender, RoutedEventArgs e)
    {
        if (NameOfCategory.Text != "")
        {
            DatabaseControl.CreateNewCategory(new Couresach.Category()
            {
                Name = NameOfCategory.Text
            });
            Close();
        }
        else
        {
            MessageBox.Show("Введите название категории!");
        }
    }
}