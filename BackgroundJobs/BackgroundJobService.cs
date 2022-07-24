using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace server.BackgroundJobs
{


    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timerInitial;
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

            _timerInitial = new Timer(DoWorkInitial, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }
        private async void DoWorkInitial(object state)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var DateTimeNow = DateTime.Now;
                    _logger.LogDebug(DateTimeNow.ToString());
                    
                    if (DateTimeNow.Second == 0) DoWork(null);
                    if (DateTimeNow.Minute == 0 && DateTimeNow.Second == 0) DoWork1(null);
                    if (DateTimeNow.Hour == 0 && DateTimeNow.Minute == 0 && DateTimeNow.Second == 0) DoWork2(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("CRON BROKEN due to  ", ex.ToString());
            }
        }

        private void DoWork(object state)
        {

            _logger.LogInformation("Timed Hosted Service1 is running");

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

                        if (obsoleteRecords.Any())
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
                }
            }
        }

        private void DoWork1(object state)
        {

            _logger.LogInformation("Timed Hosted Service2 is running");

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

                        if (obsoleteRecords.Any())
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
                }
            }
        }

        private void DoWork2(object state)
        {

            _logger.LogInformation("Timed Hosted Service3 is running");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<weatherContext>())
                {
                    var sensorIds = dbContext.Sensors.Select(x => x.Id).ToList();

                    foreach (var sensorId in sensorIds)
                    {
                        var obsoleteRecords = dbContext.Meterings
                            .Where(x => x.SensorId == sensorId && x.MeteringTypeId == 2)
                            .ToList();

                        if (obsoleteRecords.Any())
                        {
                            var averageValue = obsoleteRecords
                            .Select(x => x.Value).Average();

                            dbContext.Meterings.Add(new Meterings()
                            {
                                SensorId = sensorId,
                                MeteringTypeId = 3,
                                Value = averageValue,
                                Date = DateTime.UtcNow
                            });

                            dbContext.RemoveRange(obsoleteRecords);
                        }

                        _logger.LogInformation("Hours records are cleared " +
                            "sensorId " + sensorId +
                            " obsolete records count: " + obsoleteRecords.Count + '\n');
                    }

                    dbContext.SaveChanges();
                }

            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timerInitial?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timerInitial?.Dispose();
        }
    }
}