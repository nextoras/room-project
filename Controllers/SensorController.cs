using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.ViewModels;
using server.Enteties;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using server.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.IdentityModel.Tokens;
using server.JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    public class SensorController : Controller
    {
        public IConfiguration Configuration { get; }
        public weatherContext _wc { get; set; }
        private readonly UserManager<Users> _userManager;
        public SensorController(IConfiguration configuration, weatherContext weatherContext
            ,UserManager<Users> userManager)
        {
            Configuration = configuration;
            _wc = weatherContext;
            _userManager = userManager;
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

        [HttpPost]
        public async Task<IActionResult> AddSensor(SensorDTO sensorDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                _wc.Sensors.Add( new Sensors(){ Name = sensorDTO.Name, unit = default});

                _wc.SaveChanges();

                return Ok(sensorDTO);
            }

            return Unauthorized();
        }
    }
}
