using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server;
using server.Enteties;
using Microsoft.Extensions.Configuration;

namespace server.Controllers
{
    public class MainController : Controller
    {
        public IConfiguration Configuration { get; }
        public MainController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private class TemplateDTO
        {
            long Id { get; set; }
            DateTime Date { get; set; }
            float Temperature { get; set; }
            float Humidity { get; set; }

        }
        // [HttpGet("/getSeconds")]
        // public async Task<string> GetSeconds()
        // {
        //     // weatherContext _db = new weatherContext();
        //     // var seconds = _db.Seconds.ToList();
        //     // string result = "";
        //     // foreach (var second in seconds)
        //     // {
        //     //     result = result + "id = " + second.Id + "  Date = " + second.Date + "   T* = " + second.Temperature + "    H = " + second.Humidity + '\n';
        //     // }
        //     return "eqeqwe";
        // }

        // [HttpGet("/getMinutes")]
        // public async Task<string> GetMinutes()
        // {
        //     weatherContext _db = new weatherContext(Configuration);
        //     var minutes = _db.Minutes.ToList();
        //     string result = "";
        //     foreach (var minute in minutes)
        //     {
        //         result = result + "id = " + minute.Id + "  Date = " + minute.Date + "   T* = " + minute.Temperature + "    H = " + minute.Humidity + '\n';
        //     }
        //     return result;
        // }
        
        // [HttpGet("/getHours")]
        // public async Task<string> GetHours()
        // {
        //     weatherContext _db = new weatherContext(Configuration);
        //     var hours = _db.Hours.ToList();
        //     string result = "";
        //     foreach (var hour in hours)
        //     {
        //         result = result + "id = " + hour.Id + "  Date = " + hour.Date + "   T* = " + hour.Temperature + "    H = " + hour.Humidity + '\n';
        //     }
        //     return result;
        // }

        // [HttpGet("/getDays")]
        // public async Task<string> GetDays()
        // {
        //     weatherContext _db = new weatherContext(Configuration);
        //     var days = _db.Days.ToList();
        //     string result = "";
        //     foreach (var day in days)
        //     {
        //         result = result + "id = " + day.Id + "  Date = " + day.Date + "   T* = " + day.Temperature + "    H = " + day.Humidity + '\n';
        //     }
        //     return result;
        // }

        // [HttpGet("/SetDeviceStatus")]
        // public async Task<string> SetDeviceStatus(int status)
        // {
        //     try
        //     {
        //         weatherContext _db = new weatherContext(Configuration);
        //         var deviceStatus = _db.DeviceStatus.FirstOrDefault();
        //         if (deviceStatus != null) 
        //         {
        //             if (status == 1) 
        //             {
        //                 deviceStatus.Fan = true;
        //                 await _db.SaveChangesAsync();
        //                 return "включить";
        //             }
        //             else
        //             if (status == 0)
        //             {
        //                 deviceStatus.Fan = false;
        //                 await _db.SaveChangesAsync();
        //                 return "выключить";
        //             }
                    
        //         }
        //         return "Получен невалидный аргумент";
        //     }
        //     catch (System.Exception)
        //     {
        //         throw;
        //     }
            
        // }

        // [HttpGet("/ActualInfo")]
        // public async Task<double> ActualInfo()
        // {
        //     try
        //     {
        //         weatherContext _db = new weatherContext(Configuration);
        //         var lastMetering = _db.Meterings.FirstOrDefault();
        //         return lastMetering.Value;
        //     }
        //     catch (System.Exception)
        //     {
        //         throw;
        //     }
            
        // }

         

        // [HttpGet("/GetDataArduino")]
        // public async Task<String> GetData()
        // {
        //     weatherContext _db = new weatherContext(Configuration);

        //     // var meterings = _db.Meterings
        //     //     .ToList().OrderByDescending(x => x.Id);
            
        //     // var check = meterings.FirstOrDefault().Value /*+ "/" + meterings.Skip(1).FirstOrDefault().Value*/;
        //     // String result = "false";
        //     // if (check > 10) result = "true";

        //     var deviceStatus = _db.Devices.FirstOrDefault();

        //     if (deviceStatus.Status == true)
        //     {
        //         return "true";
        //     }
        //     else 
        //     {
        //         return "false";
        //     }
        // }
    }
}
