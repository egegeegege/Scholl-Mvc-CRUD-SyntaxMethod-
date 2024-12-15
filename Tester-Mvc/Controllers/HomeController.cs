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

        //view da g�z�kmesi i�in �nce bo� bir view lu�turuyoruz controllerda
        public IActionResult Create()
        {
            return View();
        }

        // buras� ise create sayfas�n�n post methodunun yani ��rencilerin eklenmesi i�in �al��t��� yer
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
