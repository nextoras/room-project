using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.DTO
{
    public class ListOfDeliveringsDTO
    {
        public List<OrderDTO> result { get; set; }
    }
    public class OrderDTO
    {
        public List<ProductDTO> products { get; set; }
        public int order_id { get; set; }
        public AnaliticsDataDTO analytics_data { get; set; }
        public DateTime created_at { get; set; }
        public DateTime in_process_at { get; set; }
        public string status { get; set; }
    }
    public class ProductDTO
    {
        public int sku { get; set; }
        public string offer_id { get; set; }
        public int quantity { get; set; }
        public string price { get; set; }
    }
    public class AnaliticsDataDTO
    {
        public string region { get; set; }
        public string warehouse_name { get; set; }
    }
}
