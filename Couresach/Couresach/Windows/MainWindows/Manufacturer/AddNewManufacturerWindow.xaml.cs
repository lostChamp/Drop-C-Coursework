using System.Windows;

namespace Couresach.Windows.Manufacturer;

public partial class AddNewManufacturerWindow : Window
{
    public AddNewManufacturerWindow()
    {
        InitializeComponent();
    }

    private void AddNewMan_Button(object sender, RoutedEventArgs e)
    {
        if (NameOfMan.Text != "")
        {
            DatabaseControl.CreateNewMan(new Couresach.Manufacturer()
            {
                Name = NameOfMan.Text
            });
            Close();
        }
        else
        {
            MessageBox.Show("Введите название производителя!");
        }
        
    }
}