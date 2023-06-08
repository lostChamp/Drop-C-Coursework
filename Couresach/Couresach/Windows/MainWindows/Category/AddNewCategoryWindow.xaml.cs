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
        DatabaseControl.CreateNewCategory(new Couresach.Category()
        {
            Name = NameOfCategory.Text
        });
        Close();
    }
}