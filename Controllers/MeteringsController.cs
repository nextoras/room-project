using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Microsoft.Extensions.Configuration;
using server.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace server.Controllers
{
    public class MeteringController : Controller
    {
        public IConfiguration Configuration { get; }
        public weatherContext _wc { get; set; }
        private readonly ILogger<MeteringController> _logger;
        public MeteringController(IConfiguration configuration, weatherContext weatherContext, ILogger<MeteringController> logger)
        {
            Configuration = configuration;
            _wc = weatherContext;
            _logger = logger;
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
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

                var meterings = _wc.Meterings.Where(x => x.SensorId == sensor.Id).OrderBy(x => x.Date);

                if (meterings != null)
                {
                    

                    sensorDataDTO.minute = MapMeterings(meterings, 0);
                    sensorDataDTO.hour = MapMeterings(meterings, 1);
                    sensorDataDTO.day = MapMeterings(meterings, 2);
                    sensorDataDTO.month = MapMeterings(meterings, 3);

                    sensorDataDTO.LatestMetering = sensorDataDTO.minute.Any() ? sensorDataDTO.minute.FirstOrDefault().Value : 0;

                    meteringDTO.meterings.Add(sensorDataDTO);
                }
            }

            return Ok(meteringDTO);
        }

        private List<DataDTO> MapMeterings(IQueryable<Meterings> meterings, int meteringType)
        {
            List<DataDTO> dataDTOs = new List<DataDTO>();
            var dateMeterings = meterings.Where(x => x.MeteringTypeId == ((int)meteringType));
             
            foreach (var item in dateMeterings)
            {
                DataDTO dataDTO = new DataDTO();
                dataDTO.Date = item.Date;
                dataDTO.DateString = dataDTO.Date.Minute.ToString();
                dataDTO.DateString = meteringType switch
                {
                    0 => dataDTO.Date.ToString("H:mm:ss"),
                    1 => dataDTO.Date.ToString("H:mm"),
                    2 => dataDTO.Date.ToString("dd:hh:mm"),
                    3 => dataDTO.Date.ToString("dd:MMMM")
                };
                dataDTO.MeteringTypeId = ((int)meteringType);
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

        [HttpGet("api/CreateData/{t}/{h}")]
        public async Task<String> CreateData(double t, double h)
        {
            var row_id = _wc.Meterings.Select(x => x.Id).OrderByDescending(x => x).FirstOrDefault() + 1;

            //if (row_id == null) throw new ArgumentNullException();

            Meterings metering1 = new Meterings()
            {
                Id = row_id,
                SensorId = 0,
                Date = DateTime.UtcNow,
                Value = t,
                MeteringTypeId = 0
            };

            Meterings metering2 = new Meterings()
            {
                Id = row_id + 1,
                SensorId = 1,
                Date = DateTime.UtcNow,
                Value = h,
                MeteringTypeId = 0
            };

            _wc.Meterings.Add(metering1);
            _wc.Meterings.Add(metering2);
            _wc.SaveChanges();
            return "sc";
        }
    }
}