using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.DTO;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;

namespace server.BackgroundJobs
{


    public class TimedHostedService : IHostedService, IDisposable
    {
        private class Credentials
        {
            public string ClientId { get; set; }
            public string ApiKey { get; set; }
            public Credentials(string clientId, string apiKey)
            {
                ClientId = clientId;
                ApiKey = apiKey;
            }
        }

        private class OrderDictionary
        {
            public int Keyy { get; set; }
            public string Valuee { get; set; }
        }
        public IConfiguration _configuration { get; }
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timerInitial;
        private static readonly HttpClient client = new HttpClient();
        public IServiceProvider Services { get; }
        private readonly IServiceScope _scope;
        private readonly IWebHostEnvironment _env;

        public TimedHostedService(IConfiguration configuration, ILogger<TimedHostedService> logger, IServiceProvider services, IServiceScopeFactory scopeFactory, IWebHostEnvironment env)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _scope = services.CreateScope();
            Services = services;
            _configuration = configuration;
            _env = env;
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

                    if (!_env.IsDevelopment())
                    {
                        if (DateTimeNow.Second == 0) DoWork(null);
                        if (DateTimeNow.Minute == 0 && DateTimeNow.Second == 0) DoWork1(null);
                        if (DateTimeNow.Hour == 0 && DateTimeNow.Minute == 0 && DateTimeNow.Second == 0) DoWork2(null);
                        if ((DateTimeNow.Minute == 1 && DateTimeNow.Second == 0) || (DateTimeNow.Minute == 30 && DateTimeNow.Second == 0)) await DoWorkOzonDeleverings(null);
                        if ((DateTimeNow.Minute == 2 && DateTimeNow.Second == 0) || (DateTimeNow.Minute == 31 && DateTimeNow.Second == 0)) await DoWorkOzonStock(null);
                    }
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
        private async Task DoWorkOzonDeleverings(object state)
        {

            _logger.LogInformation("Timed Hosted ServiceOzon is running");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<weatherContext>())
                {
                    Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
                    using (var requestMessage =
                            new HttpRequestMessage(HttpMethod.Post, "https://api-seller.ozon.ru/v2/posting/fbo/list"))
                    {
                        HttpContent content = new StringContent("{\"dir\": \"DESC\",\"limit\": \"" + 1000 + "\",\"filter\": {\"status\": \"" + "" + "\",	\"since\": \"" + DateTime.UtcNow.AddMonths(-1).ToString("O") + "\",	\"to\": \"" + DateTime.UtcNow.ToString("O") + "\"},\"with\": {	\"analytics_data\": true}}");
                        requestMessage.Headers.Add("Client-Id", c.ClientId);
                        requestMessage.Headers.Add("Api-Key", c.ApiKey);
                        requestMessage.Content = content;
                        string responseString = "error";
                        ListOfDeliveringsDTO deliveringsFromOzonApi = new ListOfDeliveringsDTO();
                        try
                        {
                            _logger.LogInformation("Timed Hosted ServiceOzon: start ozon request");
                            var response = await client.SendAsync(requestMessage);
                            _logger.LogInformation("Timed Hosted ServiceOzon: ozon request sended");
                            responseString = await response.Content.ReadAsStringAsync();
                            _logger.LogInformation("Timed Hosted ServiceOzon: ozon request was read to string");
                            if (responseString != "error")
                            {
                                deliveringsFromOzonApi = JsonConvert.DeserializeObject<ListOfDeliveringsDTO>(responseString);
                                var rowsFromDb = dbContext.Order.Where(x => x.OrderDate > DateTime.UtcNow.AddMonths(-1))
                                    .Select(x => new OrderDictionary { Keyy = x.OrderId, Valuee = x.Status });
                                var existingOrdersFromDB = new Dictionary<int, string>();
                                HashSet<int> helpfulHashSet = new HashSet<int>();
                                //Создаём словарь из данных от озона с уникальными ключами
                                foreach (var dict in rowsFromDb)
                                {
                                    if (helpfulHashSet.Add(dict.Keyy)) existingOrdersFromDB.Add(dict.Keyy, dict.Valuee);
                                }

                                var deliveringsFromOzonApiPrepare = deliveringsFromOzonApi.result.Select(x => new OrderDictionary { Keyy = x.order_id, Valuee = x.status });
                                _logger.LogInformation("Timed Hosted ServiceOzon: ozon request was was parsed");
                                var deliveringsOzonDictionary = new Dictionary<int, string>();
                                HashSet<int> knownValues = new HashSet<int>();

                                //Создаём словарь из данных от озона с уникальными ключами
                                foreach (var dict in deliveringsFromOzonApiPrepare)
                                {
                                    if (knownValues.Add(dict.Keyy)) deliveringsOzonDictionary.Add(dict.Keyy, dict.Valuee);
                                }
                                //словарь для идентификации строк на обновление в базе (сходства)
                                Dictionary<int, string> rowsForUpdate = new Dictionary<int, string>();

                                //проходим по уникальным строкам из озона 
                                //если такой ключ есть в бд и статус отличается, то будем обновлять
                                //если нет, то создаём новую запись
                                foreach (var dic in deliveringsOzonDictionary)
                                {
                                    if (existingOrdersFromDB.ContainsKey(dic.Key))
                                    {
                                        string a;
                                        existingOrdersFromDB.TryGetValue(dic.Key, out a);
                                        if (!a.Equals(dic.Value)) rowsForUpdate.Add(dic.Key, dic.Value);
                                    }
                                    else
                                    {
                                        //посмотреть
                                        var delivery = deliveringsFromOzonApi.result.Where(x => x.order_id == dic.Key).FirstOrDefault();
                                        var products = delivery.products;
                                        foreach (var product in products)
                                        {
                                            Order order = new Order();
                                            order.ProductName = product.offer_id;
                                            order.Quantity = product.quantity;
                                            order.WarehouseName = delivery.analytics_data.warehouse_name;
                                            order.OrderDate = delivery.created_at;
                                            order.Status = delivery.status;
                                            order.Price = decimal.Parse(product.price, CultureInfo.InvariantCulture);
                                            order.Sku = product.sku;
                                            order.Region = delivery.analytics_data.region;
                                            order.OrderId = delivery.order_id;
                                            dbContext.Order.Add(order);
                                            await dbContext.SaveChangesAsync();
                                        }

                                    }
                                }

                                //обновляем существующие записи с отличным от нового статуса
                                foreach (var del in rowsForUpdate)
                                {
                                    var rowsForUpdateDB = dbContext.Order.Where(x => x.OrderId == del.Key).ToList();
                                    foreach (var row in rowsForUpdateDB)
                                    {
                                        row.Status = del.Value;
                                        dbContext.Order.Update(row);
                                        await dbContext.SaveChangesAsync();
                                    }
                                   
                                }
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                    }

                    dbContext.SaveChanges();
                }

            }
        }

        private async Task DoWorkOzonStock(object state)
        {

            _logger.LogInformation("Timed Hosted ServiceOzonStock is running");

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<weatherContext>())
                {
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api-seller.ozon.ru/v2/posting/fbo/list"))
                    {
                        try
                        {
                            Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
                            RemainProductsDTO deliverings = new RemainProductsDTO();

                            HttpContent content = new StringContent("{  \"filter\": {		\"visibility\": \"VISIBLE\"  },  \"limit\": 1000}");
                            requestMessage.Headers.Add("Client-Id", c.ClientId);
                            requestMessage.Headers.Add("Api-Key", c.ApiKey);
                            requestMessage.Content = content;
                            string responseString = "error";
                            _logger.LogInformation("Timed Hosted ServiceOzonStock: start ozon request");
                            var response = await client.SendAsync(requestMessage);
                            _logger.LogInformation("Timed Hosted ServiceOzonStock: ozon request sended");
                            responseString = await response.Content.ReadAsStringAsync();
                            _logger.LogInformation("Timed Hosted ServiceOzonStock: ozon request was read to string");

                            if (responseString != "error")
                            {
                                deliverings = JsonConvert.DeserializeObject<RemainProductsDTO>(responseString);
                            }
                            List<RemainProductsClientDTO> result = new List<RemainProductsClientDTO>();
                            foreach (var item in deliverings.result.items)
                            {
                                if (item.stocks[0].type == "fbo") result.Add(new RemainProductsClientDTO { count = item.stocks[0].present, name = item.offer_id, productId = item.product_id });
                                else
                                    result.Add(new RemainProductsClientDTO { count = item.stocks[1].present, name = item.offer_id });
                            }

                            var ozonHashSet = result.ToHashSet();
                            var dbHashSet = dbContext.OzonProductStock.Select(x => new RemainProductsClientDTO { count = x.Quantity, name = x.ProductName, productId = x.ProductId }).ToHashSet();
                            var hashSetExcept = ozonHashSet.Except(dbHashSet);
                            foreach (var item in hashSetExcept)
                            {
                                var itemInDb = await dbContext.OzonProductStock.FindAsync(item.productId);
                                if (itemInDb != null && itemInDb.Quantity != item.count)
                                {
                                    if (itemInDb.Quantity == 0 && item.count > 0) { itemInDb.DateIncome = DateTime.Now; itemInDb.Quantity = item.count; itemInDb.ProductId = item.productId; }
                                    if (itemInDb.Quantity > 0 && item.count == 0) { itemInDb.DateEnded = DateTime.Now; itemInDb.Quantity = item.count; }
                                    if (itemInDb.Quantity > 0 && item.count > 0 && itemInDb.Quantity > item.count) { itemInDb.Quantity = item.count; }
                                    if (itemInDb.Quantity > 0 && item.count > 0 && itemInDb.Quantity < item.count) { itemInDb.Quantity = item.count; itemInDb.DateIncome = DateTime.Now; }
                                    dbContext.OzonProductStock.Update(itemInDb);
                                }
                                else
                                {
                                    var itemNew = new OzonProductStock()
                                    {
                                        ProductId = item.productId,
                                        ProductName = item.name,
                                        Quantity = item.count,
                                        DateIncome = DateTime.Now
                                    };
                                    dbContext.OzonProductStock.Add(itemNew);
                                }
                            }
                            await dbContext.SaveChangesAsync();
                        }
                        catch (System.Exception)
                        {
                            throw;
                        }
                    }
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