using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Tester_Mvc.Models;

namespace Tester_Mvc.Data
{
	public class SchoolContext :DbContext
	{
		public SchoolContext(DbContextOptions<SchoolContext> options) :base(options)
		{
			
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Öğrenci ders arasındaki ilişkiyi tanımlayalım
			modelBuilder.Entity<Course>()
				.HasOne(c => c.Student)
				.WithMany(s => s.Courses)
				.HasForeignKey(c => c.StudentId);
		}
		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
	}
}
