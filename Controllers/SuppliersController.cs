using Microsoft.AspNetCore.Mvc;
using WebApp2024.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

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

        // Invoke-RestMethod http://localhost:5000/api/suppliers/1 -Method PATCH -ContentType "application/json" -Body '[{"op":"replace","path":"City","value":"Los Angeles"}]'
        [HttpPatch("{id}")]
        public async Task<Supplier?> PatchSupplier(long id,
            JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier? s = await context.Suppliers.FindAsync(id);
            if (s != null)
            {
                patchDoc.ApplyTo(s);
                await context.SaveChangesAsync();
            }
            return s;
        }
    }
}
