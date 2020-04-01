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
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
