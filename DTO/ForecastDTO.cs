using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTO
{
    public class ForecastDTO
    {
        public int Sku { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string WarehouseName { get; set; }
        public decimal CountForecast { get; set; }
    }
}
