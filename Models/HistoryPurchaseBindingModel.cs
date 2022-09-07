using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class HistoryPurchaseBindingModel
    {
        [Required]
        public string dir { get; set; }
        public int limit { get; set; }
        public HistoryPurchaseFilterBindingModel filter { get; set; }
        public HistoryPurchaseWithBindingModel with { get; set; }
    }
    public class HistoryPurchaseFilterBindingModel
    {
        public string status { get; set; }
        public DateTime since { get; set; }
        public DateTime to { get; set; }
    }
    public class HistoryPurchaseWithBindingModel
    {
        public bool analytics_data { get; set; }
    }
}
