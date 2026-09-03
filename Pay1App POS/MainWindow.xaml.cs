using Pay1App_POS.Models;
using Pay1App_POS.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Pay1App_POS;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    // Double-click product to add to cart
    private void ProductDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainViewModel vm &&
            sender is System.Windows.Controls.DataGrid grid &&
            grid.SelectedItem is Product product)
        {
            vm.AddToCartCommand.Execute(product);
        }
    }
}
