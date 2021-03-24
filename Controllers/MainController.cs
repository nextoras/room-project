using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server;
using server.Enteties;

namespace server.Controllers
{
    public class MainController : Controller
    {
        private class TemplateDTO
        {
            long Id { get; set; }
            DateTime Date { get; set; }
            float Temperature { get; set; }
            float Humidity { get; set; }

        }
        [HttpGet("/getSeconds")]
        public async Task<string> GetSeconds()
        {
            // weatherContext _db = new weatherContext();
            // var seconds = _db.Seconds.ToList();
            // string result = "";
            // foreach (var second in seconds)
            // {
            //     result = result + "id = " + second.Id + "  Date = " + second.Date + "   T* = " + second.Temperature + "    H = " + second.Humidity + '\n';
            // }
            return "eqeqwe";
        }

        [HttpGet("/getMinutes")]
        public async Task<string> GetMinutes()
        {
            weatherContext _db = new weatherContext();
            var minutes = _db.Minutes.ToList();
            string result = "";
            foreach (var minute in minutes)
            {
                result = result + "id = " + minute.Id + "  Date = " + minute.Date + "   T* = " + minute.Temperature + "    H = " + minute.Humidity + '\n';
            }
            return result;
        }
        
        [HttpGet("/getHours")]
        public async Task<string> GetHours()
        {
            weatherContext _db = new weatherContext();
            var hours = _db.Hours.ToList();
            string result = "";
            foreach (var hour in hours)
            {
                result = result + "id = " + hour.Id + "  Date = " + hour.Date + "   T* = " + hour.Temperature + "    H = " + hour.Humidity + '\n';
            }
            return result;
        }

        [HttpGet("/getDays")]
        public async Task<string> GetDays()
        {
            weatherContext _db = new weatherContext();
            var days = _db.Days.ToList();
            string result = "";
            foreach (var day in days)
            {
                result = result + "id = " + day.Id + "  Date = " + day.Date + "   T* = " + day.Temperature + "    H = " + day.Humidity + '\n';
            }
            return result;
        }

        [HttpGet("/SetDeviceStatus")]
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
                        return "включить";
                    }
                    else
                    if (status == 0)
                    {
                        deviceStatus.Fan = false;
                        return "выключить";
                    }
                    await _db.SaveChangesAsync();
                }
                return "Получен невалидный аргумент";
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        [HttpGet("/ActualInfo")]
        public async Task<double> ActualInfo()
        {
            try
            {
                weatherContext _db = new weatherContext();
                var lastMetering = _db.Meterings.FirstOrDefault();
                return lastMetering.Value;
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        [HttpGet("/CreateData/{t}/{h}")]
        public async Task<String> CreateData(double t, double h)
        {
            weatherContext _db = new weatherContext();

            var row_id = _db.Meterings
                .ToList().Count() + 1;
                

            Meterings metering1 = new Meterings()
                {
                    Id = row_id,
                    SensorId = 0,
                    Date = DateTime.UtcNow,
                    Value = t,
                    MeteringTypeId = 0,
                    UserId = "0"
                };

            Meterings metering2 = new Meterings()
                {
                    Id = row_id + 1,
                    SensorId = 1,
                    Date = DateTime.UtcNow,
                    Value = h,
                    MeteringTypeId = 0,
                    UserId = "0"
                };

            // _db.Meterings.Add(metering1);
            // _db.Meterings.Add(metering2);
            _db.SaveChanges();
            return "sc";
        }

        [HttpGet("/GetDataArduino")]
        public async Task<String> GetData()
        {
            weatherContext _db = new weatherContext();

            // var meterings = _db.Meterings
            //     .ToList().OrderByDescending(x => x.Id);
            
            // var check = meterings.FirstOrDefault().Value /*+ "/" + meterings.Skip(1).FirstOrDefault().Value*/;
            // String result = "false";
            // if (check > 10) result = "true";

            var deviceStatus = _db.Devices.FirstOrDefault();

            if (deviceStatus.Status == true)
            {
                return "true";
            }
            else 
            {
                return "false";
            }
        }
    }
}
