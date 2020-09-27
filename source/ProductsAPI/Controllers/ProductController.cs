using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsDbContext dbContext;

        public ProductController(ProductsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await dbContext.Products.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await dbContext.Products.FindAsync(id);

            return product == null ? (IActionResult)NotFound(id) : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ModifyProduct newProduct)
        {
            var product = new Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price
            };

            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Created($"product/{product.Id}", product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ModifyProduct newProduct)
        {
            var existingProduct = await dbContext.Products.FindAsync(id);
            existingProduct.Name = newProduct.Name;
            existingProduct.Price = newProduct.Price;
            await dbContext.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return new StatusCodeResult(204);
        }
    }
}
