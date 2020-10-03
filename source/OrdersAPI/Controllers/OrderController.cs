using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrdersAPI.Models;
using OrdersAPI.Repositories;

namespace OrdersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrdersDbContext dbContext;
        private readonly HttpClient httpClient;

        public OrderController(OrdersDbContext dbContext, IHttpClientFactory httpClientFactory)
        {
            this.dbContext = dbContext;
            
            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("http://productsapi/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await dbContext.Orders.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrder createOrder)
        {
            double totalPrice = 0;

            foreach (var productId in createOrder.ProductIds)
            {
                var response = await httpClient.GetAsync($"product/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
                    if (product != null)
                    {
                        totalPrice += product.Price;
                    }
                }
                else
                {
                    return NotFound($"No product found with id: {productId}");
                }
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                ProductIds = string.Join(';', createOrder.ProductIds),
                TotalPrice = totalPrice
            };
            await dbContext.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return Created($"orders/{order.Id}", order);
        }
    }
}
