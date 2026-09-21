using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _90_Web.AppDbContext;
using _90_Web.Models;

namespace _90_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly Context _context;

        public SubscriptionsController(Context context)
        {
            _context = context;
        }

        [HttpGet("check/{branchId}")]
        public async Task<IActionResult> CheckSubscriptionStatus(int branchId)
        {
            var subscription = await _context.Subscriptions
                .Where(s => s.BranchId == branchId)
                .OrderByDescending(s => s.ExpiryDate)
                .FirstOrDefaultAsync();

            if (subscription == null)
            {
                return Ok(new { isAccessGranted = false, message = "No active subscription found for this branch." });
            }

            bool isAccessGranted = subscription.IsPaid && subscription.ExpiryDate >= DateTime.UtcNow;

            return Ok(new
            {
                isAccessGranted = isAccessGranted,
                expiryDate = subscription.ExpiryDate,
                isPaid = subscription.IsPaid
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddSubscription([FromBody] Subscription newSubscription)
        {
            if (newSubscription == null)
            {
                return BadRequest("Subscription data is invalid.");
            }

            var branchExists = await _context.Branches.AnyAsync(b => b.Id == newSubscription.BranchId);
            if (!branchExists)
            {
                return BadRequest("Specified BranchId does not exist.");
            }

            _context.Subscriptions.Add(newSubscription);
            await _context.SaveChangesAsync();

            return Ok(newSubscription);
        }
    }
}