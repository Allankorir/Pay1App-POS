using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay1App_POS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;      // Wine, Spirit, Beer, etc.
        public string Brand { get; set; } = string.Empty;
        public decimal ABV { get; set; }
        public int VolumeMl { get; set; }
        public string Region { get; set; } = string.Empty;
        public string? Vintage { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
