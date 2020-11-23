using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
    public class HomeController : Controller
    {
        // private readonly weatherContext _db;
        // HomeController(weatherContext db)
        // {
        //     _db = db;
        // }
        //public weatherContext _db = new weatherContext();

        public IActionResult Index()
        {
            weatherContext db = new weatherContext();
            var userId = 0;

            MainPageDTO mainPageDTO = new MainPageDTO();
            List<MeteringDTO> meteringDTOs = new List<MeteringDTO>();
            List<SensorDTO> sensorDTOs = new List<SensorDTO>();
            List<DeviceDTO> deviceDTOs = new List<DeviceDTO>();

            var userDevices = db.UserDevices.Where(e => e.UserId == userId).ToList();
            List<Devices> devices = new List<Devices>();
            foreach (var userDevice in userDevices)
            {
                var device = db.Devices.Where(d => d.Id == userDevice.DeviceId).FirstOrDefault();
                if (device != null)
                {
                    DeviceDTO deviceDTO = new DeviceDTO()
                    {
                        Id = device.Id,
                        Name = device.Name,
                        Status = device.Status
                    };
                    deviceDTOs.Add(deviceDTO);
                }
            }

            var userSensors = db.UserSensors.Where(e => e.UserId == userId).ToList();
            List<Sensors> sensors = new List<Sensors>();
            foreach (var userSensor in userSensors)
            {
                var sensor = db.Sensors.Where(d => d.Id == userSensor.SensorId).FirstOrDefault();
                if (sensor != null)
                {
                    var meteringTypes = db.MeteringTypes.ToList();
                    List<AvereageValueDTO> avereageValues = new List<AvereageValueDTO>();

                    foreach (var meteringType in meteringTypes)
                    {
                        
                        var metering = db.Meterings.Where(m => m.MeteringTypeId == meteringType.Id && m.SensorId == sensor.Id)
                            .OrderByDescending(m => m.Date)
                            .FirstOrDefault();
                        if (metering != null)   
                        {
                            AvereageValueDTO avereageValueDTO = new AvereageValueDTO()
                            {
                                Value = metering.Value
                            };
                            avereageValues.Add(avereageValueDTO);
                        }
                    }
                    SensorDTO sensorDTO = new SensorDTO()
                    {
                        Id = sensor.Id,
                        Name = sensor.Name,
                        AvereageValues = avereageValues
                    };
                    sensorDTOs.Add(sensorDTO);
                }
            }

            var userMeterings = db.Meterings
            .Where(m => m.UserId == userId)
            .ToList();

            foreach (var userMetering in userMeterings)
            {
                MeteringDTO meteringDTO = new MeteringDTO()
                {
                    Id = userMetering.Id,
                    SensorId = userMetering.SensorId,
                    Date = userMetering.Date,
                    Value = userMetering.Value,
                    MeteringTypeId = userMetering.MeteringTypeId,
                    UserId = userMetering.Id
                };
                meteringDTOs.Add(meteringDTO);
            }
            mainPageDTO.devices = deviceDTOs;
            mainPageDTO.sensors = sensorDTOs;
            mainPageDTO.meterings = meteringDTOs;

            return View(mainPageDTO);
        }

        [HttpPost]
        public IActionResult Index(DeviceDTO dto)
        {
            if (dto != null)
            {

                weatherContext db = new weatherContext();
                var device = db.Devices.Where(d => d.Id == dto.Id).FirstOrDefault();

                if (device != null)
                {
                    device.Status = !device.Status;
                    db.Devices.Update(device);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Разработчик всегда на связи!";

            return View();
        }

        public IActionResult GetSeconds()
        {
            weatherContext db = new weatherContext();
            var seconds = db.Seconds.ToList();
            return View(seconds);
        }

        public IActionResult GetMinutes() 
        {
            weatherContext db = new weatherContext();
            var minutes = db.Minutes.ToList();
            return View(minutes);
        }

        public IActionResult GetHours()
        {
            weatherContext db = new weatherContext();
            var hours = db.Hours.ToList();
            return View(hours);
        }

        public IActionResult GetDays()
        {
            weatherContext db = new weatherContext();
            var days = db.Days.ToList();
            return View(days);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<string> SetDeviceStatus(int status)
        {
            try
            {
                weatherContext _db = new weatherContext();
                var deviceStatus = _db.DeviceStatus.FirstOrDefault();
                if (deviceStatus != null)
                {
                    if (status == 1)
                    {
                        deviceStatus.Fan = true;
                        await _db.SaveChangesAsync();
                        return "включить";
                    }
                    else
                    if (status == 0)
                    {
                        deviceStatus.Fan = false;
                        await _db.SaveChangesAsync();
                        return "выключить";
                    }
                }
                return "Получен невалидный аргумент";
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}
