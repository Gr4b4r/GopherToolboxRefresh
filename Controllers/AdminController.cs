using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GopherToolboxRefresh.Data;
using GopherToolboxRefresh.Models;
using System.Linq;
using System.Threading.Tasks;
using GopherToolboxRefresh.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.ConstrainedExecution;

namespace GopherToolboxRefresh.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AdminController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<IActionResult> CancelRequests()
		{
			var requests = await _context.CancelRequests
				.Include(c => c.Order)
				.ThenInclude(o => o.Quest)
				.Include(c => c.Order.User)
				.ToListAsync();

			return View(requests);
		}

		public async Task<IActionResult> Orders()
		{
			var orders = await _context.Orders
				.Include(o => o.Quest)
				.Include(o => o.User)
				.ToListAsync();
			return View(orders);
		}

		[HttpPost]
		public async Task<IActionResult> ApproveCancellation(int id)
		{
			var request = await _context.CancelRequests
				.Include(c => c.Order)
				.ThenInclude(o => o.Quest)
				.Include(c => c.Order.User)
				.FirstOrDefaultAsync(c => c.Id == id);

			if (request == null)
			{
				return NotFound();
			}

			var order = request.Order;
			var quest = order.Quest;

			if (quest.CurrentOccupiedSlots > 0)
			{
				quest.CurrentOccupiedSlots--;
			}

			order.IsCanceled = true;
			_context.Quests.Update(quest);
			_context.Orders.Update(order);
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
				try
				{
					var existingQuest = await _context.Quests.FindAsync(id);
					if (existingQuest == null)
					{
						return NotFound();	
					}

					existingQuest.Name = quest.Name;
					existingQuest.City = quest.City;
					existingQuest.Address = quest.Address;
					existingQuest.QuestDateStart = quest.QuestDateStart;
					existingQuest.QuestDateEnd = quest.QuestDateEnd;
					existingQuest.SlotLimit = quest.SlotLimit;
					existingQuest.ImageUrl = quest.ImageUrl;
					existingQuest.Description = quest.Description;

					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}
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

		public IActionResult UserManagement()
		{
			var user = _context.Users.ToList();
			return View(user);
		}

		public IActionResult RegisterUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RegisterUser(RegisterViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Name = model.Name,
                    Surname = model.Surname,
					Nickname = model.Nickname,
                    Birthdate = model.Birthdate,
                    Phone = model.Phone,
					Rola = model.Rola,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, model.Rola);
                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
					return View(model);
                }

            }
            return RedirectToAction("UserManagement");
        }

		public async Task<IActionResult> EditUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var globalAdmin = await _userManager.FindByEmailAsync("admin@admin.pl");
			if (user == globalAdmin)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			var model = new EditUserViewModel
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Surname = user.Surname,
				Nickname = user.Nickname,
				Birthdate = user.Birthdate,
				Phone = user.Phone,
				Rola = user.Rola
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditUser(EditUserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}


			var user = await _userManager.FindByIdAsync(model.Id);
			if (user == null)
			{
				return NotFound();
			}
			var globalAdmin = await _userManager.FindByEmailAsync("admin@admin.pl");
			if (user == globalAdmin)
			{
				return RedirectToAction(nameof(UserManagement));
			}

			user.Email = model.Email;
			user.Name = model.Name;
			user.Surname = model.Surname;
			user.Nickname = model.Nickname;
			user.Birthdate = model.Birthdate;
			user.Phone = model.Phone;
			user.Rola = model.Rola;

			var result = await _userManager.UpdateAsync(user);

			if (!string.IsNullOrEmpty(model.Password))
			{
				var token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var Passwordresult = await _userManager.ResetPasswordAsync(user, token, model.Password);

				if (!Passwordresult.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
					return View(model);
				}
			}
			await _userManager.RemoveFromRoleAsync(user, "Admin");
			await _userManager.RemoveFromRoleAsync(user, "Uzytkownik");			
			var roleResult = await _userManager.AddToRoleAsync(user, model.Rola);
			if (!roleResult.Succeeded)
			{
				foreach (var error in roleResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			if (result.Succeeded && roleResult.Succeeded)
			{
				return RedirectToAction("UserManagement");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> RemoveUser(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			var globalAdmin = await _userManager.FindByEmailAsync("admin@admin.pl");
			if (user == null)
			{
				return NotFound();
			}
			if (user == globalAdmin)
			{
                return RedirectToAction(nameof(UserManagement));
            }

			var model = new EditUserViewModel
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Surname = user.Surname,
				Nickname = user.Nickname,
				Birthdate = user.Birthdate,
				Phone = user.Phone,
				Rola = user.Rola,
			};
			return View(model);
		}

		[HttpPost, ActionName("RemoveUser")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> RemoveUserConfirmed(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			var globalAdmin = await _userManager.FindByEmailAsync("admin@admin.pl");
            if (user == null)
            {
                return NotFound();
            }
            if (user == globalAdmin)
            {
                return RedirectToAction(nameof(UserManagement));
            }

			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction(nameof(UserManagement));
			}
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(user);
		}

		[HttpGet]
		public async Task<IActionResult> UserDetails(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var role = _userManager.GetRolesAsync;
			var model = new EditUserViewModel
			{
				Id = user.Id,
				Email = user.Email,
				Name = user.Name,
				Surname = user.Surname,
				Nickname = user.Nickname,
				Birthdate = user.Birthdate,
				Phone = user.Phone,
				Rola = user.Rola,
			};
			return View(model);
		}
	}
}
