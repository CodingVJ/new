using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mywebapplication.Models;
using mywebapplication.Models.NewFolder;





namespace mywebapplication.Controllers
{
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UserController(ApplicationDbContext context)
		{
			_context = context;
		}

		private static int counter = 5;

		[HttpGet]
		public JsonResult GetCounter()
		{
			return Json(new { count = counter });
		}

		[HttpPost]
		public JsonResult IncrementCounter()
		{
			counter++;
			return Json(new { count = counter });
		}

		[HttpPost]
		public JsonResult DecrementCounter()
		{
			counter--;
			return Json(new { count = counter });
		}

		[HttpPost]
		public JsonResult ResetCounter()
		{
			counter = 0;
			return Json(new { count = counter });
		}

		[HttpPost]
		public IActionResult DeleteUser(int id)
		{
			var user = _context.Users.FirstOrDefault(i => i.Id == id);
			if (user != null)
			{
				_context.Users.Remove(user);
				_context.SaveChanges();
				return Json(new { success = true, message = "Item deleted successfully." });
			}
			return Json(new { success = false, message = "Item not found." });
		}


		// Display Registration Form 
		public IActionResult Register()
		{
			return PartialView("_Register");

		}



		[HttpPost]
		public IActionResult RegisterUser(User user)
		{
			if (ModelState.IsValid)
			{
				_context.Users.Add(user);
				_context.SaveChanges();

				return Json(new { success = true });
			}
			return View("_Register", user);
		}

		public IActionResult GetUsers()
		{
			var users = _context.Users.ToList();
			return PartialView("_UserList", users);
		}


		// Display all users (Index)
		public async Task<IActionResult> Index()
		{
			return View(await _context.Users.ToListAsync());
		}

		public async Task<IActionResult> UpdatedIndex()
		{
			return PartialView("_IndexList", await _context.Users.ToListAsync());
		}

		public async Task<IActionResult> UpdatedIndexView()
		{
			return View("_IndexList", await _context.Users.ToListAsync());
		}
		

		private bool UserExists(int id)
		{
			return _context.Users.Any(e => e.Id == id);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var user = _context.Users.FirstOrDefault(i => i.Id == id);
			if (user != null)
			{
				//_context.Users.Remove(user);
				_context.SaveChanges();
				return Json(new { success = true, message = "Item deleted successfully." });
			}
			return Json(new { success = false, message = "Item not found." });
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{

			var user = _context.Users.FirstOrDefault(u=> u.Id == id);
	        if (user == null)
			{
				return NotFound();
			}
			return PartialView("_EditUser", user);
		}

		public IActionResult Update(User updatedUser)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
			if (user == null)
			{
				return NotFound();
			}

			user.Name = updatedUser.Name;
			user.Email = updatedUser.Email;

			return View(new { success = true });
		}
	}
}


		
	   