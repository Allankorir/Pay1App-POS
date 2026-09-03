using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pay1App_POS.Models;
using Pay1App_POS.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pay1App_POS.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [ObservableProperty]
        private ObservableCollection<CartItem> cartItems = new();

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private string barcodeInput = string.Empty;

        [ObservableProperty]
        private decimal subTotal;

        [ObservableProperty]
        private decimal tax;

        [ObservableProperty]
        private decimal total;

        [ObservableProperty]
        private string statusMessage = "Ready";

        public MainViewModel(IProductService productService)
        {
            _productService = productService;
            LoadProductsCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadProducts()
        {
            var list = await _productService.GetAllProductsAsync();
            Products = new ObservableCollection<Product>(list);
            StatusMessage = $"{Products.Count} products loaded";
        }

        [RelayCommand]
        private async Task Search()
        {
            var results = await _productService.SearchAsync(SearchText);
            Products = new ObservableCollection<Product>(results);
        }

        [RelayCommand]
        private async Task AddByBarcode()
        {
            if (string.IsNullOrWhiteSpace(BarcodeInput)) return;

            var product = await _productService.GetByBarcodeAsync(BarcodeInput.Trim());
            if (product != null)
            {
                AddToCart(product);
                BarcodeInput = string.Empty;
                StatusMessage = $"Added: {product.Name}";
            }
            else
            {
                StatusMessage = "Product not found";
                MessageBox.Show("Product not found for this barcode.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        private void AddToCart(Product product)
        {
            var existing = CartItems.FirstOrDefault(c => c.Product.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity++;
                // Force UI refresh
                var index = CartItems.IndexOf(existing);
                CartItems.RemoveAt(index);
                CartItems.Insert(index, existing);
            }
            else
            {
                CartItems.Add(new CartItem { Product = product, Quantity = 1 });
            }
            RecalculateTotals();
        }

        [RelayCommand]
        private void IncreaseQuantity(CartItem item)
        {
            item.Quantity++;
            RefreshCartItem(item);
            RecalculateTotals();
        }

        [RelayCommand]
        private void DecreaseQuantity(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                RefreshCartItem(item);
            }
            else
            {
                CartItems.Remove(item);
            }
            RecalculateTotals();
        }

        [RelayCommand]
        private void RemoveFromCart(CartItem item)
        {
            CartItems.Remove(item);
            RecalculateTotals();
        }

        [RelayCommand]
        private void ClearCart()
        {
            CartItems.Clear();
            RecalculateTotals();
            StatusMessage = "Cart cleared";
        }

        [RelayCommand]
        private void Checkout()
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Cart is empty.", "Checkout", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var order = new Order
            {
                Items = CartItems.ToList(),
                Tax = Tax,
                PaymentMethod = "Cash"
            };

            // Later: call Sales microservice here
            MessageBox.Show($"Order completed!\n\nTotal: R {order.Total:N2}\nItems: {order.Items.Sum(i => i.Quantity)}",
                            "Payment Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            CartItems.Clear();
            RecalculateTotals();
            StatusMessage = $"Order {order.Id.ToString()[..8]} completed";
        }

        private void RecalculateTotals()
        {
            SubTotal = CartItems.Sum(i => i.LineTotal);
            Tax = Math.Round(SubTotal * 0.15m, 2); // 15% VAT example – change as needed
            Total = SubTotal + Tax;
        }
        [RelayCommand]
        private async Task FilterCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                await LoadProducts();
                return;
            }

            var all = await _productService.GetAllProductsAsync();
            var filtered = all.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            Products = new ObservableCollection<Product>(filtered);
            StatusMessage = $"{filtered.Count} {category} products";
        }
        private void RefreshCartItem(CartItem item)
        {
            var index = CartItems.IndexOf(item);
            if (index >= 0)
            {
                CartItems.RemoveAt(index);
                CartItems.Insert(index, item);
            }
        }
    }
}


