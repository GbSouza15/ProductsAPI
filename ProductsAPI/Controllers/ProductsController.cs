using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsAPIDbContext dbContext;

        public ProductsController(ProductsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAllProducts()
        {
            var products = dbContext.Products.ToList();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewProduct(AddProductRequest addProductRequest)
        {
            var product = new Product
            {
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Category = addProductRequest.Category,
                Value = addProductRequest.Value,
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductRequest updateProductRequest)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product != null)
            {
                product.Name = updateProductRequest.Name;
                product.Description = updateProductRequest.Description;
                product.Category = updateProductRequest.Category;
                product.Value = updateProductRequest.Value;

                await dbContext.SaveChangesAsync();
                return Ok(product);
            }

            return NotFound();
        }
    }
}
