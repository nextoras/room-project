using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace server
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string WarehouseName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int Sku { get; set; }
        public string Region { get; set; }
    }
}
