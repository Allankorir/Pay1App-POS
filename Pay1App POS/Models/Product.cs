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
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;
        public string? Brand { get; set; }
        public decimal? ABV { get; set; }
        public int? VolumeMl { get; set; }
        public string? Region { get; set; }
        public string? Vintage { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; } = 5;
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Category? Category { get; set; }

        // Convenience for UI (not mapped if you prefer)
        public string CategoryName => Category?.Name ?? string.Empty;
    }
}