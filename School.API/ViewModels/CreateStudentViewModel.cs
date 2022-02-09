using School.API.Models;

namespace School.API.ViewModels
{
    public class CreateStudentViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public Student FromModel() => new(Name, Email, DateOnly.FromDateTime(Birthday));

        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - Birthday.Year;

                if (DateTime.Now.DayOfYear < Birthday.DayOfYear)
                    age -= 1;

                return age;
            }
        }
    }
}
