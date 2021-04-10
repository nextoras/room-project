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
        public DeviceController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        // public async Task<string> SetDeviceStatus(long id, bool status)
        // {
        //     try
        //     {
        //         weatherContext _db = new weatherContext(Configuration);
        //         var deviceStatus = _db.DeviceStatus.Where(x => x.Id == id).FirstOrDefault();
                
        //         // deviceStatusl
        //         return "Получен невалидный аргумент";
        //     }
        //     catch (System.Exception)
        //     {
        //         throw;
        //     }

        // }
    }
}
