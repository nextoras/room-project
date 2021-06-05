using System;
using System.Collections.Generic;

namespace server
{
    public partial class MainPageDTO
    {
        public List<DeviceDTO> devices { get; set; }
        public List<MeteringsDTO> meterings { get; set; }
        public List<SensorDTO> sensors { get; set; }
        public List<AvereageValueDTO> avereageValues { get; set; }

        public MainPageDTO()
        {
            devices = new List<DeviceDTO>();
            meterings = new List<MeteringsDTO>();
            sensors = new List<SensorDTO>();   
        }
    }
}
