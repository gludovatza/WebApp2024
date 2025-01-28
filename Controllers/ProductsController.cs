using Microsoft.AspNetCore.Mvc;
using WebApp2024.Models;

namespace WebApp2024.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private DataContext context;
        public ProductsController(DataContext ctx)
        {
            context = ctx;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }
        [HttpGet("{id}")]
        public Product? GetProduct(long id)
        {
            return context.Products.Find(id);
        }
    }
}
