using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Microsoft.Extensions.Configuration;

namespace server.Controllers
{
    public class SensorController : Controller
    {
        public IConfiguration Configuration { get; }
        public weatherContext _wc { get; set; }
        public SensorController(IConfiguration configuration, weatherContext weatherContext)
        {
            Configuration = configuration;
            _wc = weatherContext;
        }

        [HttpGet]
        public IEnumerable<Sensors> GetSensors()
        {
            return _wc.Sensors.ToList();
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
