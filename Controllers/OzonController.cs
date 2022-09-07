using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.DTO;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization;

namespace server.Controllers
{

    public class OzonController : Controller
    {
        private class OrderDictionary
        {
            public int Keyy { get; set; }
            public string Valuee { get; set; }
        }
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
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration _configuration { get; }
        public weatherContext _wc { get; set; }
        private readonly ILogger<MeteringController> _logger;
        public OzonController(IConfiguration configuration, weatherContext weatherContext, ILogger<MeteringController> logger)
        {
            _configuration = configuration;
            _wc = weatherContext;
            _logger = logger;
        }


        [HttpPost]
        public async Task<object> GetProductList(string since, string to, string status, int limit)
        {
            try
            {
                
                Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
                ListOfDeliveringsDTO deliverings = new ListOfDeliveringsDTO();

                HttpContent content = new StringContent("{\"dir\": \"DESC\",\"limit\": \"" + limit + "\",\"filter\": {\"status\": \"" + status + "\",	\"since\": \"" + since + "\",	\"to\": \"" + to + "\"},\"with\": {	\"analytics_data\": true}}");
                string uri = "https://api-seller.ozon.ru/v2/posting/fbo/list";
                var responseString = await HttpClient(uri, "POST", content);
                if (responseString != "error")
                { 
                    deliverings = JsonConvert.DeserializeObject<ListOfDeliveringsDTO>(responseString);                     
                }
                
                return deliverings;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<object> GetForecast(DateTime since, DateTime to, int page, int? quantity, string direction)
        {
            try
            {
                var result = await Forecast(since, to, page, quantity, direction);
                return result;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task GOVNO()
        {
            Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
            using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "https://api-seller.ozon.ru/v2/posting/fbo/list"))
            {
                HttpContent content = new StringContent("{\"dir\": \"DESC\",\"limit\": \"" + 500 + "\",\"filter\": {\"status\": \"" + "" + "\",	\"since\": \"" + DateTime.UtcNow.AddMonths(-1).ToString("O") + "\",	\"to\": \"" + DateTime.UtcNow.ToString("O") + "\"},\"with\": {	\"analytics_data\": true}}");
                string b = DateTime.UtcNow.ToLongDateString(); ;
                requestMessage.Headers.Add("Client-Id", c.ClientId);
                requestMessage.Headers.Add("Api-Key", c.ApiKey);
                requestMessage.Content = content;
                string responseString = "error";
                ListOfDeliveringsDTO deliveringsFromOzonApi = new ListOfDeliveringsDTO();
                try
                {
                    var response = await client.SendAsync(requestMessage);
                    //responseString = await response.Content.ReadAsStringAsync();

                    if (responseString != "error")
                    {
                        deliveringsFromOzonApi = JsonConvert.DeserializeObject<ListOfDeliveringsDTO>(responseString);
                        var rowsFromDb = _wc.Order.Where(x => x.OrderDate > DateTime.UtcNow.AddMonths(-1))
                            .Select(x => new OrderDictionary { Keyy = x.OrderId, Valuee = x.Status });
                        var existingOrdersFromDB = new Dictionary<int, string>();
                        HashSet<int> helpfulHashSet = new HashSet<int>();
                        //������ ������� �� ������ �� ����� � ����������� �������
                        foreach (var dict in rowsFromDb)
                        {
                            if (helpfulHashSet.Add(dict.Keyy)) existingOrdersFromDB.Add(dict.Keyy, dict.Valuee);
                        }

                        var deliveringsFromOzonApiPrepare = deliveringsFromOzonApi.result.Select(x => new OrderDictionary { Keyy = x.order_id, Valuee = x.status });

                        var deliveringsOzonDictionary = new Dictionary<int, string>();
                        HashSet<int> knownValues = new HashSet<int>();

                        //������ ������� �� ������ �� ����� � ����������� �������
                        foreach (var dict in deliveringsFromOzonApiPrepare)
                        {
                            if (knownValues.Add(dict.Keyy)) deliveringsOzonDictionary.Add(dict.Keyy, dict.Valuee);
                        }
                        //������� ��� ������������� ����� �� ���������� � ���� (��������)
                        Dictionary<int, string> rowsForUpdate = new Dictionary<int, string>();

                        //�������� �� ���������� ������� �� ����� 
                        //���� ����� ���� ���� � �� � ������ ����������, �� ����� ���������
                        //���� ���, �� ������ ����� ������
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
                                    _wc.Order.Add(order);
                                    await _wc.SaveChangesAsync();
                                }
                                
                            }
                        }
                        //��������� ������������ ������ � �������� �� ������ �������
                        foreach (var del in rowsForUpdate)
                        {
                            var rowsForUpdateDB = _wc.Order.Where(x => x.OrderId == del.Key).ToList();
                            foreach (var row in rowsForUpdateDB)
                            {
                                row.Status = del.Value;
                                _wc.Order.Update(row);
                                await _wc.SaveChangesAsync();
                            }

                        }
                        await _wc.SaveChangesAsync();

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            _wc.SaveChanges();
        }

        [HttpPost]
        public async Task<object> PutOrders(string since, string to, string status, int limit)
        {
            try
            {
                Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
                ListOfDeliveringsDTO deliverings = new ListOfDeliveringsDTO();

                HttpContent content = new StringContent("{\"dir\": \"DESC\",\"limit\": \"" + limit + "\",\"filter\": {\"status\": \"" + status + "\",	\"since\": \"" + since + "\",	\"to\": \"" + to + "\"},\"with\": {	\"analytics_data\": true}}");
                string uri = "https://api-seller.ozon.ru/v2/posting/fbo/list";
                var responseString = await HttpClient(uri, "POST", content);
                if (responseString != "error")
                {
                    //deliverings = JsonConvert.DeserializeObject<ListOfDeliveringsDTO>(responseString);
                }

                foreach (var delivery in deliverings.result)
                {
                    foreach (var product in delivery.products)
                    {
                        Order order = new Order();
                        order.ProductName = product.offer_id;
                        order.Quantity = product.quantity;
                        order.WarehouseName = delivery.analytics_data.warehouse_name;
                        order.OrderDate = delivery.created_at;
                        order.Status = delivery.status;
                        order.Price = decimal.Parse(product.price, CultureInfo.InvariantCulture);
                        order.Sku = product.sku;
                        order.OrderId = delivery.order_id;
                        order.Region = delivery.analytics_data.region;
                        _wc.Order.Add(order);
                        await _wc.SaveChangesAsync();
                        
                    }
                }

                return deliverings;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<object> RemainProducts()
        {
            try
            {
                var productStock = _wc.OzonProductStock.ToList();
                return await Paginate(productStock, null, null, "");
                //return paginatedResult;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task RemainProductsPut()
        {
            try
            {
                Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
                RemainProductsDTO deliverings = new RemainProductsDTO();

                HttpContent content = new StringContent("{  \"filter\": {		\"visibility\": \"VISIBLE\"  },  \"limit\": 1000}");
                string uri = "https://api-seller.ozon.ru/v3/product/info/stocks";
                var responseString = await HttpClient(uri, "POST", content);
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
                var dbHashSet = _wc.OzonProductStock.Select(x => new RemainProductsClientDTO { count = x.Quantity, name = x.ProductName, productId = x.ProductId }).ToHashSet();
                var hashSetExcept = ozonHashSet.Except(dbHashSet);
                foreach (var item in hashSetExcept)
                {
                    var itemInDb = await _wc.OzonProductStock.FindAsync(item.productId);
                    if (itemInDb != null && itemInDb.Quantity != item.count)
                    {
                        if (itemInDb.Quantity == 0 && item.count > 0) { itemInDb.DateIncome = DateTime.Now; itemInDb.Quantity = item.count; itemInDb.ProductId = item.productId; }
                        if (itemInDb.Quantity > 0 && item.count == 0) { itemInDb.DateEnded = DateTime.Now; itemInDb.Quantity = item.count; }
                        if (itemInDb.Quantity > 0 && item.count > 0) { itemInDb.Quantity = item.count; }
                        _wc.OzonProductStock.Update(itemInDb);
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
                        _wc.OzonProductStock.Add(itemNew);
                    }
                }
                await _wc.SaveChangesAsync();
                
                
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        private async Task<string> HttpClient (string uri, string httpMethod, HttpContent content)
        {
            Credentials c = new Credentials(_configuration.GetValue<string>("OzonCredentials:Client-Id"), _configuration.GetValue<string>("OzonCredentials:Api-Key"));
            using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, uri))
            {
                requestMessage.Headers.Add("Client-Id", c.ClientId);
                requestMessage.Headers.Add("Api-Key", c.ApiKey);
                requestMessage.Content = content;
                string result = "error";
                try
                {
                    var response = await client.SendAsync(requestMessage);
                    var responseString = await response.Content.ReadAsStringAsync();
                    //result = JsonConvert.DeserializeObject<ListOfDeliveringsDTO>(responseString);
                    return responseString;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return result;
            }
        }
        private async Task<object> Forecast(DateTime since, DateTime to, int page, int? quantity, string direction)
        {
            
            if (since == DateTime.MinValue) since = DateTime.Today.AddDays(-7);
            if (to == DateTime.MinValue) to = DateTime.Today;
            var orders = _wc.Order.Where(x => x.OrderDate > since && x.OrderDate < to.AddDays(1)).ToList();
            var skus = orders.Select(x => x.Sku).Distinct();
            List<ForecastDTO> forecastDTOs = new List<ForecastDTO>();
            decimal daysLeft = to.Subtract(since).Days > 0 ? to.Subtract(since).Days : 1;
            foreach (var sku in skus)
            {
                ForecastDTO forecastDTO = new ForecastDTO();

                var order = orders.Where(x => x.Sku == sku).Select(x => new { ProductName = x.ProductName, Quantity = x.Quantity});
                forecastDTO.ProductName = order.FirstOrDefault().ProductName;
                forecastDTO.Quantity = order.Select(x => x.Quantity).Sum();
                forecastDTO.Sku = sku;
                forecastDTO.CountForecast = decimal.Round(forecastDTO.Quantity / daysLeft, 2);
                forecastDTOs.Add(forecastDTO);
            }
            return await Paginate(forecastDTOs, page, quantity, direction);
            //IOrderedEnumerable <ForecastDTO> orderedEnumerables = forecastDTOs.OrderByDescending(x => x.Quantity);
            //return orderedEnumerables;
        }
        private async Task<IEnumerable<ForecastDTO>> Paginate(List<ForecastDTO> items, int? page, int? quantity, string direction)
        {
            if (!quantity.HasValue) quantity = 200;
            if (!page.HasValue) page = 1;
            direction = string.IsNullOrEmpty(direction) ? "desc" : direction;

            var orderedItems = direction.Equals("asc") ? items.OrderBy(x => x.Quantity) : items.OrderByDescending(x => x.Quantity);
            var paginatedItems = orderedItems.Skip(((int)page - 1) * (int)quantity).Take((int)quantity);
            return paginatedItems;
        }
        private async Task<IEnumerable<OzonProductStock>> Paginate(List<OzonProductStock> items, int? page, int? quantity, string direction)
        {
            if (!quantity.HasValue) quantity = 2000;
            if (!page.HasValue) page = 1;
            direction = string.IsNullOrEmpty(direction) ? "desc" : direction;
            var orderedItems = direction.Equals("asc") ? items.OrderBy(x => x.Quantity) : items.OrderByDescending(x => x.Quantity);
            var paginatedItems = orderedItems.Skip(((int)page - 1) * (int)quantity).Take((int)quantity);
            return paginatedItems;
        }
    }
}