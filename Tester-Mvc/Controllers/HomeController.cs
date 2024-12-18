using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tester_Mvc.Data;
using Tester_Mvc.Models;

namespace Tester_Mvc.Controllers
{
    public class HomeController : Controller
    {
		private readonly SchoolContext _context;

		public HomeController(SchoolContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            List<Student> students = _context.Students.ToList();
            return View(students);
        }

        //view da gözükmesi için önce boþ bir view luþturuyoruz controllerda
        public IActionResult Create()
        {
            return View();
        }

        // burasý ise create sayfasýnýn post methodunun yani öðrencilerin eklenmesi için çalýþtýðý yer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Age,Department")] Student student)
        {
			if (ModelState.IsValid)
			{
				_context.Add(student);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(student);
        }
        public IActionResult Details(int id)
        {
            var student = _context.Students.Find(id);
			if (student == null)
			{
				return NotFound();
			}
			return View(student);
        }

		public IActionResult Edit(int id)
		{
			var student = _context.Students.Find(id);
			if (student == null)
			{
				return NotFound();
			}
			return View(student);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,[Bind("Id,Name,Age,Department")] Student student)
        {
			if (!StudentExists(student.Id) )
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(student);
					_context.SaveChanges();
				}
				catch (Exception)
				{
					if (!StudentExists (student.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}
			return View(student);
        }



		private bool StudentExists(int id)
		{
			return _context.Students.Any(e => e.Id == id);
		}

		public IActionResult Delete (int id)
		{
			var student = _context.Students.Find(id);
			if (ModelState == null)
			{
				return NotFound();
			}
			return View(student);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult DeleteConfirmend(int id)
		{
			var student = _context.Students.Find(id);

			if (student != null)
			{
				_context.Students.Remove(student);
				_context.SaveChanges();
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
