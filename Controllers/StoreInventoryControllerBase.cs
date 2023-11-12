using Microsoft.AspNetCore.Mvc;
using MintoStore.Models;
using Newtonsoft.Json;
using System.Text;

namespace MintoStore.Controllers
{
    public class StoreInventoryControllerBase
    {
        public class StoreInventoryController : Controller
        {


            HttpClientHandler _clientHandler = new HttpClientHandler();

            Product _oProducts = new Product();
            List<Product> _oProductsList = new List<Product>();

            public StoreInventoryController()
            {
                _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            }


            public IActionResult Index()
            {

                return View();
            }

            [HttpGet]
            public async Task<List<Product>> GetAllProducts()
            {
                _oProductsList = new List<Product>();

                using (var httpClient = new HttpClient(_clientHandler))
                {
                    using (var response = await httpClient.GetAsync(""))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                        _oProductsList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
#pragma warning restore CS8601 // Possible null reference assignment.
                    }
                }
#pragma warning disable CS8603 // Possible null reference return.
                return _oProductsList;
#pragma warning restore CS8603 // Possible null reference return.
            }

            [HttpGet]
            public async Task<Product> GetById(int ProductId)
            {
                _oProducts = new Product();

                using (var httpClient = new HttpClient(_clientHandler))
                {
                    using (var response = await httpClient.GetAsync("" + ProductId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                        _oProducts = JsonConvert.DeserializeObject<Product>(apiResponse);
#pragma warning restore CS8601 // Possible null reference assignment.
                    }
                }
#pragma warning disable CS8603 // Possible null reference return.
                return _oProducts;
#pragma warning restore CS8603 // Possible null reference return.
            }

            [HttpPost]
            public async Task<Product> AddUpdateWashers(Product product)
            {
                _oProducts = new Product();

                using (var httpClient = new HttpClient(_clientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8601 // Possible null reference assignment.
                        _oProducts = JsonConvert.DeserializeObject<Product>(apiResponse);
#pragma warning restore CS8601 // Possible null reference assignment.
                    }
                }
#pragma warning disable CS8603 // Possible null reference return.
                return _oProducts;
#pragma warning restore CS8603 // Possible null reference return.
            }

            [HttpDelete]
            public async Task<string> Delete(int ProductId)
            {
                string message = "";

                using (var httpClient = new HttpClient(_clientHandler))
                {
                    using (var response = await httpClient.DeleteAsync("" + ProductId))
                    {
                        message = await response.Content.ReadAsStringAsync();
                    }
                }
                return message;
            }
        }
    }
}