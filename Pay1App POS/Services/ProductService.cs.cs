using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pay1App_POS.Models;

namespace Pay1App_POS.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            // Sample wines & spirits data
            _products = new List<Product>
            {
                new() { Id = 1, Name = "Cabernet Sauvignon 2020", SKU = "WINE-001", Barcode = "6001234567890",
                        Category = "Wine", Brand = "Stellenbosch Reserve", ABV = 14.5m, VolumeMl = 750,
                        Region = "Stellenbosch", Vintage = "2020", Price = 289.99m, Cost = 145m, StockQuantity = 48 },

                new() { Id = 2, Name = "Sauvignon Blanc 2023", SKU = "WINE-002", Barcode = "6001234567891",
                        Category = "Wine", Brand = "Constantia Hills", ABV = 12.5m, VolumeMl = 750,
                        Region = "Constantia", Vintage = "2023", Price = 159.99m, Cost = 78m, StockQuantity = 72 },

                new() { Id = 3, Name = "Jameson Irish Whiskey", SKU = "SPIRIT-001", Barcode = "5011007003005",
                        Category = "Spirit", Brand = "Jameson", ABV = 40m, VolumeMl = 750,
                        Region = "Ireland", Price = 349.99m, Cost = 210m, StockQuantity = 36 },

                new() { Id = 4, Name = "Johnnie Walker Black Label", SKU = "SPIRIT-002", Barcode = "5000267014203",
                        Category = "Spirit", Brand = "Johnnie Walker", ABV = 40m, VolumeMl = 750,
                        Region = "Scotland", Price = 429.99m, Cost = 265m, StockQuantity = 28 },

                new() { Id = 5, Name = "Absolut Vodka", SKU = "SPIRIT-003", Barcode = "7312040017003",
                        Category = "Spirit", Brand = "Absolut", ABV = 40m, VolumeMl = 750,
                        Region = "Sweden", Price = 249.99m, Cost = 145m, StockQuantity = 55 },

                new() { Id = 6, Name = "Amarula Cream Liqueur", SKU = "SPIRIT-004", Barcode = "6001224001013",
                        Category = "Spirit", Brand = "Amarula", ABV = 17m, VolumeMl = 750,
                        Region = "South Africa", Price = 189.99m, Cost = 95m, StockQuantity = 40 },

                new() { Id = 7, Name = "Pinotage 2021", SKU = "WINE-003", Barcode = "6001234567892",
                        Category = "Wine", Brand = "Kanonkop", ABV = 14m, VolumeMl = 750,
                        Region = "Stellenbosch", Vintage = "2021", Price = 219.99m, Cost = 110m, StockQuantity = 32 },

                new() { Id = 8, Name = "Chenin Blanc 2022", SKU = "WINE-004", Barcode = "6001234567893",
                        Category = "Wine", Brand = "Raats Family", ABV = 13m, VolumeMl = 750,
                        Region = "Stellenbosch", Vintage = "2022", Price = 175.00m, Cost = 88m, StockQuantity = 60 },
            };
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return Task.FromResult(_products.Where(p => p.IsActive).ToList());
        }

        public Task<Product?> GetByBarcodeAsync(string barcode)
        {
            var product = _products.FirstOrDefault(p => p.Barcode == barcode && p.IsActive);
            return Task.FromResult(product);
        }

        public Task<List<Product>> SearchAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return GetAllProductsAsync();

            term = term.ToLower();
            var results = _products
                .Where(p => p.IsActive &&
                       (p.Name.ToLower().Contains(term) ||
                        p.Brand.ToLower().Contains(term) ||
                        p.Category.ToLower().Contains(term) ||
                        p.SKU.ToLower().Contains(term) ||
                        p.Barcode.Contains(term)))
                .ToList();

            return Task.FromResult(results);
        }
    }
}
