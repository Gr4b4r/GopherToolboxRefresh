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

		public async Task<IActionResult> Index(string searchString)
		{
			var quests = from m in _context.Quests
						 select m;
			if (!String.IsNullOrEmpty(searchString))
			{
				quests = quests.Where(s => s.Name!.Contains(searchString));
			}

			var quest = await _context.Quests.ToListAsync();
			return View(await quests.ToListAsync());
			//return View(quest);
		}

		public async Task<IActionResult> Details(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var quest = await _context.Quests
				.Include(q => q.UserQuests)
				.ThenInclude(uq => uq.User)
				.FirstOrDefaultAsync(q => q.Id == id);

			if (quest == null)
			{
				return NotFound();
			}

			ViewBag.UserId = user.Id;
			return View(quest);
		}

		[HttpPost]
		public async Task<IActionResult> Join(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var quest = await _context.Quests.Include(q => q.UserQuests).FirstOrDefaultAsync(q => q.Id == id);

			if (quest == null)
			{
				return NotFound();
			}

			if (quest.UserQuests.Count(uq => !uq.IsCanceled) >= quest.SlotLimit)
			{
				return BadRequest("No more slots available for this quest.");
			}

			if (quest.UserQuests.Any(uq => uq.UserId == user.Id && !uq.IsCanceled))
			{
				return BadRequest("User already joined this quest.");
			}

			var userQuest = new Order
			{
				UserId = user.Id,
				QuestId = quest.Id,
				OrderDate = DateTime.Now,
				QuestDateStart = quest.QuestDateStart,
				QuestDateEnd = quest.QuestDateEnd,
				CancellationRequested = false,
				IsCanceled = false
			};

			quest.CurrentOccupiedSlots++;
			_context.Orders.Add(userQuest);
			_context.Quests.Update(quest);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(MyQuests));
		}

		public async Task<IActionResult> MyQuests(string searchString)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var orders = _context.Orders
				.Include(o => o.Quest)
				.Where(o => o.UserId == user.Id);

			if (!String.IsNullOrEmpty(searchString))
			{
				orders = orders.Where(o => o.Quest.Name.Contains(searchString));
			}

			var quest = await orders.ToListAsync();
			return View(quest);
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
	}
}