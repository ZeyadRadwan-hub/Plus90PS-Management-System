using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _90_Web.AppDbContext;
using _90_Web.Models;

namespace _90_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly Context _context;

        public DevicesController(Context context)
        {   
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDevices()
        {
            var devices = await _context.Devices.Include(d => d.Branch).ToListAsync();
            return Ok(devices);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<IActionResult> GetDevicesByBranch(int branchId)
        {
            var devices = await _context.Devices
                .Where(d => d.BranchId == branchId)
                .ToListAsync();

            return Ok(devices);
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] Device newDevice)
        {
            if (newDevice == null)
            {
                return BadRequest("Device data is invalid.");
            }

            var branchExists = await _context.Branches.AnyAsync(b => b.Id == newDevice.BranchId);
            if (!branchExists)
            {
                return BadRequest("Specified BranchId does not exist.");
            }

            _context.Devices.Add(newDevice);
            await _context.SaveChangesAsync();

            return Ok(newDevice);
        }
    }
}