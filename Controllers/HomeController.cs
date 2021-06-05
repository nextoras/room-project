using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public weatherContext _wc { get; set; }
        public HomeController(IConfiguration configuration, weatherContext weatherContext)
        {
            Configuration = configuration;
            _wc = weatherContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            return Ok();
        }
        // public IActionResult Index()
        // {
            // weatherContext db = new weatherContext(Configuration);
            // var userId = "asd";

            // MainPageDTO mainPageDTO = new MainPageDTO();
            // List<MeteringDTO> meteringDTOs = new List<MeteringDTO>();
            // List<SensorDTO> sensorDTOs = new List<SensorDTO>();
            // List<DeviceDTO> deviceDTOs = new List<DeviceDTO>();

            // //var userDevices = db.UserDevices.FirstOrDefault();
            // List<Devices> devices = new List<Devices>();
            // // foreach (var userDevice in userDevices)
            // // {
            // //     var device = db.Devices.Where(d => d.Id == userDevice.DeviceId).FirstOrDefault();
            //     if (true /*userDevices != null*/)
            //     {
            //         DeviceDTO deviceDTO = new DeviceDTO()
            //         {
            //             Id = 1,//userDevices.Id,
            //             Name = "asd",//userDevices.Name,
            //             Status = true,// userDevices.Status
            //         };
            //         deviceDTOs.Add(deviceDTO);
            //     }
            // // }

            // var userSensors = db.UserSensors.Where(e => e.UserId == userId).ToList();
            // List<Sensors> sensors = new List<Sensors>();
            // foreach (var userSensor in userSensors)
            // {
            //     var sensor = db.Sensors.Where(d => d.Id == userSensor.SensorId).FirstOrDefault();
            //     if (sensor != null)
            //     {
            //         var meteringTypes = db.MeteringTypes.ToList();
            //         List<AvereageValueDTO> avereageValues = new List<AvereageValueDTO>();

            //         foreach (var meteringType in meteringTypes)
            //         {
                        
            //             var metering = db.Meterings.Where(m => m.MeteringTypeId == meteringType.Id && m.SensorId == sensor.Id)
            //                 .OrderByDescending(m => m.Date)
            //                 .FirstOrDefault();
            //             if (metering != null)   
            //             {
            //                 AvereageValueDTO avereageValueDTO = new AvereageValueDTO()
            //                 {
            //                     Value = metering.Value
            //                 };
            //                 avereageValues.Add(avereageValueDTO);
            //             }
            //         }
            //         SensorDTO sensorDTO = new SensorDTO()
            //         {
            //             Id = sensor.Id,
            //             Name = sensor.Name,
            //             AvereageValues = avereageValues
            //         };
            //         sensorDTOs.Add(sensorDTO);
            //     }
            // }

            // var userMeterings = db.Meterings
            // .Where(m => m.UserId == userId)
            // .ToList();

            // foreach (var userMetering in userMeterings)
            // {
            //     MeteringDTO meteringDTO = new MeteringDTO()
            //     {
            //         Id = userMetering.Id,
            //         SensorId = userMetering.SensorId,
            //         Date = userMetering.Date,
            //         Value = userMetering.Value,
            //         MeteringTypeId = userMetering.MeteringTypeId
            //         //UserId = userMetering.Id
            //     };
            //     meteringDTOs.Add(meteringDTO);
            // }
            // mainPageDTO.devices = deviceDTOs;
            // mainPageDTO.sensors = sensorDTOs;
            // mainPageDTO.meterings = meteringDTOs;

        //     return Ok(/*mainPageDTO*/);
        // }

        // [HttpPost]
        // public IActionResult Index(DeviceDTO dto)
        // {
        //     if (dto != null)
        //     {

        //         weatherContext db = new weatherContext(Configuration);
        //         var device = db.Devices.Where(d => d.Id == dto.Id).FirstOrDefault();

        //         if (device != null)
        //         {
        //             device.Status = !device.Status;
        //             db.Devices.Update(device);
        //             db.SaveChanges();
        //         }
        //         return RedirectToAction("Index");
        //     }
        //     return RedirectToAction("Index");
        // }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Разработчик всегда на связи!";

            return View();
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
                var deviceStatus = _wc.Devices.FirstOrDefault();
                if (deviceStatus != null)
                {
                    // if (status == 1)
                    // {
                    //     deviceStatus.Fan = true;
                    //     await _db.SaveChangesAsync();
                    //     return "включить";
                    // }
                    // else
                    // if (status == 0)
                    // {
                    //     deviceStatus.Fan = false;
                    //     await _db.SaveChangesAsync();
                    //     return "выключить";
                    // }
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
