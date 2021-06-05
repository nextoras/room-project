using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Microsoft.Extensions.Configuration;
using server.DTO;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    public class MeteringController : Controller
    {
        public IConfiguration Configuration { get; }
        public weatherContext _wc { get; set; }
        public MeteringController(IConfiguration configuration, weatherContext weatherContext)
        {
            Configuration = configuration;
            _wc = weatherContext;
        }
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public ActionResult<MeteringDTO> GetMeterings()
        {
            MeteringDTO meteringDTO = new MeteringDTO();
            meteringDTO.meterings = new List<SensorDataDTO>();
            var sensors = _wc.Sensors.ToList();

            foreach (var sensor in sensors)
            {
                SensorDataDTO sensorDataDTO = new SensorDataDTO();

                sensorDataDTO.SensorId = sensor.Id;
                sensorDataDTO.SensorName = sensor.Name;

                var meterings = _wc.Meterings.Where(x => x.SensorId == sensor.Id);

                if (meterings != null)
                {
                    var meteringTypes = _wc.MeteringTypes.ToList();

                    sensorDataDTO.minute = MapMeterings(meterings, 0);
                    sensorDataDTO.hour = MapMeterings(meterings, 1);
                    sensorDataDTO.day = MapMeterings(meterings, 2);
                    sensorDataDTO.week = MapMeterings(meterings, 3);

                    meteringDTO.meterings.Add(sensorDataDTO);
                }
            }
            return Ok(meteringDTO);
        }

        private List<DataDTO> MapMeterings(IQueryable<Meterings> meterings, int meteringType)
        {
            List<DataDTO> dataDTOs = new List<DataDTO>();
            var dateMeterings = meterings.Where(x => x.MeteringTypeId == meteringType);

            foreach (var item in dateMeterings)
            {
                DataDTO dataDTO = new DataDTO();
                dataDTO.Date = item.Date;
                dataDTO.MeteringTypeId = meteringType;
                dataDTO.Value = item.Value;

                dataDTOs.Add(dataDTO);
            }

            return dataDTOs;
        }

        [HttpPost]
        public async Task<string> SetDeviceStatus(long id, bool status)
        {
            try
            {
                var deviceStatus = _wc.Devices.Where(x => x.Id == id).FirstOrDefault();

                deviceStatus.Status = status;

                _wc.Devices.Update(deviceStatus);

                await _wc.SaveChangesAsync();

                return "Получен невалидный аргумент";
            }
            catch (System.Exception)
            {
                throw;
            }

        }


    }
}
