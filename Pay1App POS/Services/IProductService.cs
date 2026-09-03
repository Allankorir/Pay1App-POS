using Pay1App_POS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay1App_POS.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetByBarcodeAsync(string barcode);
        Task<List<Product>> SearchAsync(string term);
    }
}