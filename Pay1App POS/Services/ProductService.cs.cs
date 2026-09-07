using Microsoft.EntityFrameworkCore;
using Pay1App_POS.Data;
using Pay1App_POS.Models;

namespace Pay1App_POS.Services
{
    public class ProductService : IProductService
    {
        public async Task<List<Product>> GetAllProductsAsync()
        {
            using var db = new PosDbContext();
            return await db.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetByBarcodeAsync(string barcode)
        {
            using var db = new PosDbContext();
            return await db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Barcode == barcode && p.IsActive);
        }

        public async Task<List<Product>> SearchAsync(string term)
        {
            using var db = new PosDbContext();
            term = term?.Trim() ?? string.Empty;

            var query = db.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.SKU.ToLower().Contains(term) ||
                    p.Barcode.Contains(term) ||
                    (p.Brand != null && p.Brand.ToLower().Contains(term)) ||
                    (p.Category != null && p.Category.Name.ToLower().Contains(term)));
            }

            return await query.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            using var db = new PosDbContext();
            return await db.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByCategoryAsync(string categoryName)
        {
            using var db = new PosDbContext();
            return await db.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.Category != null && p.Category.Name == categoryName)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}