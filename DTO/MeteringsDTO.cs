using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.DTO
{
    public class MeteringDTO
    {
        public List<SensorDataDTO> meterings { get; set; }
    }
    public class SensorDataDTO
    {
        public int SensorId { get; set; }
        public string SensorName { get; set; }
        public double LatestMetering { get; set;}
        public List<DataDTO> minute { get; set; }
        public List<DataDTO> hour { get; set; }
        public List<DataDTO> day { get; set; }
        public List<DataDTO> month { get; set; }
    }
    public class DataDTO
    {
        public DateTime Date { get; set; }
        public string DateString {get; set; }
        public double Value { get; set; }
        public int MeteringTypeId { get; set; }
    }
}
