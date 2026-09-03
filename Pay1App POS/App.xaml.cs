using System.Windows;
using Pay1App_POS.Services;
using Pay1App_POS.ViewModels;

namespace Pay1App_POS
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var productService = new ProductService();
            var mainViewModel = new MainViewModel(productService);

            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }
}