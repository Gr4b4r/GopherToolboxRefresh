using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GopherToolboxRefresh.Data;
using GopherToolboxRefresh.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GopherToolboxRefresh.Controllers
{
    [Authorize]
    public class QuestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public QuestController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var quest = await _context.Quests.ToListAsync();
            return View(quest);
        }

        public async Task<IActionResult> Details(int id)
        {
            var quest = await _context.Quests.FirstOrDefaultAsync(h => h.Id == id);
            if (quest == null)
            {
                return NotFound();
            }
            return View(quest);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int questId, DateTime checkInDate, DateTime checkOutDate)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var order = new Order
            {
                UserId = user.Id,
                QuestId = questId,
                OrderDate = DateTime.Now,
                CancellationRequested = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("MyQuests");
        }

        public async Task<IActionResult> MyQuests()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var orders = await _context.Orders
                .Include(o => o.Quest)
                .Where(o => o.UserId == user.Id)
                .ToListAsync();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> RequestCancellation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == user.Id);

            if (order == null)
            {
                return NotFound();
            }

            var cancelRequest = new CancelRequest
            {
                OrderId = order.Id,
                RequestDate = DateTime.Now
            };

            _context.CancelRequests.Add(cancelRequest);
            order.CancellationRequested = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("MyQuests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quest quest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(quest);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving quest: " + ex.Message);
                    ModelState.AddModelError("", "Error saving quest");
                }
            }
            else
            {
                // Logowanie błędów walidacji
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
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
                try
                {
                    _context.Update(quest);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestExists(quest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving quest: " + ex.Message);
                    ModelState.AddModelError("", "Error saving quest");
                }
            }
            else
            {
                // Logowanie błędów walidacji
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(quest);
        }

        private bool QuestExists(int id)
        {
            return _context.Quests.Any(e => e.Id == id);
        }
    }
}
