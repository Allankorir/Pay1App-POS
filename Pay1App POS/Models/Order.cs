using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay1App_POS.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<CartItem> Items { get; set; } = new();
        public decimal SubTotal => Items.Sum(i => i.LineTotal);
        public decimal Tax { get; set; }
        public decimal Total => SubTotal + Tax;
        public string PaymentMethod { get; set; } = "Cash";
        public string Status { get; set; } = "Completed";
    }
}
