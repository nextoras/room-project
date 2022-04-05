using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using server.Interfaces;

namespace server.BackgroundJobs
{


    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        private Timer _timer;
        private Timer _timer1;
        public IServiceProvider Services { get; }
        private readonly IServiceScope _scope;


        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider services, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _scope = services.CreateScope();
            Services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
     
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            _timer1 = new Timer(DoWork1, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3600));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {

            _logger.LogInformation("Timed Hosted Service is running");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<weatherContext>())
                {
                    var sensorIds = dbContext.Sensors.Select(x => x.Id).ToList();

                    foreach (var sensorId in sensorIds)
                    {
                        var obsoleteRecords = dbContext.Meterings
                            .Where(x => x.SensorId == sensorId && x.MeteringTypeId == 0)
                            .ToList();

                        if (obsoleteRecords.Count > 0)
                        {
                            var averageValue = obsoleteRecords
                            .Select(x => x.Value).Average();

                            dbContext.Meterings.Add(new Meterings()
                                {
                                    SensorId = sensorId,
                                    MeteringTypeId = 1,
                                    Value = averageValue,
                                    Date = DateTime.UtcNow
                                });

                        dbContext.RemoveRange(obsoleteRecords);
                        }

                        _logger.LogInformation("Minute records are cleared " +
                            "sensorId " + sensorId + 
                            " obsolete records count: " + obsoleteRecords.Count + '\n');
                    }

                    dbContext.SaveChanges();

                    Console.WriteLine("Online tagger Service is Running");
                }

            }

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}");
        }

        private void DoWork1(object state)
        {

            _logger.LogInformation("Timed Hosted Service is running");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<weatherContext>())
                {
                    var sensorIds = dbContext.Sensors.Select(x => x.Id).ToList();

                    foreach (var sensorId in sensorIds)
                    {
                        var obsoleteRecords = dbContext.Meterings
                            .Where(x => x.SensorId == sensorId && x.MeteringTypeId == 1)
                            .ToList();

                        if (obsoleteRecords.Count > 0)
                        {
                            var averageValue = obsoleteRecords
                            .Select(x => x.Value).Average();

                            dbContext.Meterings.Add(new Meterings()
                            {
                                SensorId = sensorId,
                                MeteringTypeId = 2,
                                Value = averageValue,
                                Date = DateTime.UtcNow
                            });

                            dbContext.RemoveRange(obsoleteRecords);
                        }

                        _logger.LogInformation("Minute records are cleared " +
                            "sensorId " + sensorId +
                            " obsolete records count: " + obsoleteRecords.Count + '\n');
                    }

                    dbContext.SaveChanges();

                    Console.WriteLine("Online tagger Service is Running");
                }

            }

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}