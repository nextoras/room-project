using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTO
{
    public class RemainProductsDTO
    {
        public ProductRemainDTO result { get; set; }
    }
    public class ProductRemainDTO
    {
        public List<ProductRemainItemsDTO> items { get; set; }
    }
    public class ProductRemainItemsDTO
    {
        public string offer_id { get; set; }
        public int product_id { get; set; }
        public List<ProductRemainItemsStockDTO> stocks { get; set; }
    }
    public class ProductRemainItemsStockDTO
    {
        public string type { get; set; }
        public int present { get; set; }
    }
}
