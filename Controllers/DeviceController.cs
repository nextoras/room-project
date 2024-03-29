using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Microsoft.Extensions.Configuration;

namespace server.Controllers
{
    public class DeviceController : Controller
    {
        public IConfiguration Configuration { get; }
        public weatherContext _wc { get; set; }
        public DeviceController(IConfiguration configuration, weatherContext weatherContext)
        {
            Configuration = configuration;
            _wc = weatherContext;
        }

        [HttpGet]
        public IEnumerable<Devices> GetDevices()
        {
            return _wc.Devices.ToList().OrderBy(x => x.Id);
        }
        
        [HttpPost]
        public async Task ChangeStatus(long id)
        {
            try
            {
                var device = _wc.Devices.Where(x => x.Id == id).FirstOrDefault();
                
                if (device != null)
                {
                    device.Status = !device.Status;
                    _wc.Devices.Update(device);

                    await _wc.SaveChangesAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        
        
    }
}
