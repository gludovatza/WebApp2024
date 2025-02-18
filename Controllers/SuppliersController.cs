using Microsoft.AspNetCore.Mvc;
using WebApp2024.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp2024.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private DataContext context;
        public SuppliersController(DataContext ctx)
        {
            context = ctx;
        }
        // localhost:5000/api/suppliers/1
        [HttpGet("{id}")]
        public async Task<Supplier?> GetSupplier(long id)
        {
            Supplier s = await context.Suppliers
                .Include(s => s.Products)
                .FirstAsync(s => s.SupplierId == id);

            if(s.Products != null)
            {
                foreach (var product in s.Products)
                {
                    product.Supplier = null;
                }
            }
            return s;
        }
    }
}
