using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _90_Web.AppDbContext;
using _90_Web.Models;

namespace _90_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.Include(p => p.Branch).ToListAsync();
            return Ok(products);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetProductsByBranch(int branchId)
        {
            var products = await _context.Products
                .Where(p => p.BranchId == branchId)
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Product data is invalid.");
            }

            var branchExists = await _context.Branches.AnyAsync(b => b.Id == newProduct.BranchId);
            if (!branchExists)
            {
                return BadRequest("Specified BranchId does not exist.");
            }

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }
    }
}