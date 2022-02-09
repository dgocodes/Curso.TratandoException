namespace School.API.Exceptions
{
    public class MinimumAgeException : Exception
    {
        public MinimumAgeException()
        {
        }

        public MinimumAgeException(string message) : base(message)
        {
        }

        public MinimumAgeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public MinimumAgeException(string? message, int age) : base(message)
        {
            Age = age;
        }

        public int Age { get; private set; }
    }
}
