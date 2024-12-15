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
       
    }
}
