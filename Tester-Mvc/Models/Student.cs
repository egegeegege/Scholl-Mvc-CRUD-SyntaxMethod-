namespace Tester_Mvc.Models
{
	public class Student
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Department { get; set; }

		// Öğrencini aldığı derslerle zorunlu olmayan bir ilişki
		public ICollection<Course> Courses { get; set; } = new List<Course>();
	}
}
