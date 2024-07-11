using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GopherToolboxRefresh.Data;
using GopherToolboxRefresh.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GopherToolboxRefresh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> CancelRequests()
        {
            var requests = await _context.CancelRequests
                .Include(c => c.Order)
                .ThenInclude(o => o.Quest)
                .Include(c => c.Order.User) // Include user information
                .ToListAsync();

            return View(requests);
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .Include(o => o.Quest)
                .Include(o => o.User) // Include user information
                .ToListAsync();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveCancellation(int id)
        {
            var request = await _context.CancelRequests
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            var order = request.Order;
            order.IsCanceled = true; // Set cancel status
            _context.Orders.Update(order); // Update the order
            _context.CancelRequests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("CancelRequests");
        }

        [HttpPost]
        public async Task<IActionResult> DenyCancellation(int id)
        {
            var request = await _context.CancelRequests
                .Include(c => c.Order)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            request.Order.CancellationRequested = false;
            _context.CancelRequests.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("CancelRequests");
        }

        public IActionResult Index()
        {
            var quests = _context.Quests.ToList();
            return View(quests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quest quest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quest);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            return View(quest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Quest quest)
        {
            if (id != quest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quest);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }

            _context.Quests.Remove(quest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var quest = await _context.Quests.FirstOrDefaultAsync(h => h.Id == id);
            if (quest == null)
            {
                return NotFound();
            }
            return View(quest);
        }
    }
}
