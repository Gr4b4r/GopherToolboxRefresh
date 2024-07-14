// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GopherToolboxRefresh.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GopherToolboxRefresh.Data;

public class HomeController : Controller
{
	private readonly ApplicationDbContext _context;
	private readonly SignInManager<User> _signInManager;

	public HomeController(ApplicationDbContext context, SignInManager<User> signInManager)
	{
		_context = context;
		_signInManager = signInManager;
	}

	public IActionResult Index()
	{
		if (_signInManager.IsSignedIn(User))
		{
            return RedirectToAction("Index", "Quest");
        }
		else
		{
            return View();
        }
	}

	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
