using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _90_Web.AppDbContext;
using _90_Web.Models;

namespace _90_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.Customers.Include(c => c.Branch).ToListAsync();
            return Ok(customers);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetCustomersByBranch(int branchId)
        {
            var customers = await _context.Customers
                .Where(c => c.BranchId == branchId)
                .ToListAsync();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("Customer data is invalid.");
            }

            var branchExists = await _context.Branches.AnyAsync(b => b.Id == newCustomer.BranchId);
            if (!branchExists)
            {
                return BadRequest("Specified BranchId does not exist.");
            }

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return Ok(newCustomer);
        }
    }
}