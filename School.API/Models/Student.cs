namespace School.API.Models
{
    public class Student
    {
        public Student(string name, string email, DateOnly? birthday)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Birthday = birthday;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateOnly? Birthday { get; private set; }
    }
}
