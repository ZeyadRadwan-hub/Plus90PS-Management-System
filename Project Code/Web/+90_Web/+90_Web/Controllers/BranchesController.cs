using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _90_Web.AppDbContext;
using _90_Web.Models;

namespace _90_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly Context _context;

        public BranchesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _context.Branches.ToListAsync();
            return Ok(branches);
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch([FromBody] Branch newBranch)
        {
            if (newBranch == null)
            {
                return BadRequest("Branch data is invalid.");
            }

            _context.Branches.Add(newBranch);
            await _context.SaveChangesAsync();

            return Ok(newBranch);
        }
    }
}