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
	}
}

//Pozostałości kodu, do sprawdzenia czy w tym miejscu będzie jakolwiek potrzebne
//Wstępnie nie widać problemów z funkcjonowaniem bez tej częsci

/*      [HttpPost]
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
	}*/