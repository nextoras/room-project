using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using server.Interfaces;

namespace server.Services
{
    public class BackgroundJobService : IBackgroundJobInterface
    {
        public weatherContext _wc { get; set; }
        private readonly ILogger<BackgroundJobService> _logger;
        protected class MeteringUpdate
        {
            public int Id;
            public double Value;
        }

        public BackgroundJobService(weatherContext weatherContext, ILogger<BackgroundJobService> logger)
        {
            _wc = weatherContext;
            _logger = logger;
        }
        public void  UpdateMeteringMinute()
        {
            var sensorIds = _wc.Sensors.Select(x => x.Id).ToList();

            foreach (var sensorId in sensorIds)
            {
                var meteringUpdates = _wc.Meterings.Where(x => x.SensorId == sensorId && x.MeteringTypeId == 1)
                    .Select(x => new MeteringUpdate
                    {
                        Id = x.Id,
                        Value = x.Value
                    });
                
                _logger.LogInformation("updated minutes");
            }
            
        }
    }
}