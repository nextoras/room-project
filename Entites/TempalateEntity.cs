using System;
using System.Collections.Generic;

namespace server
{
    public partial class TemplateEntity
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
    }
}
