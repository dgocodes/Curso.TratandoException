namespace School.API.Exceptions
{
    public class EmailAlreadyRegistredException : Exception
    {
        public EmailAlreadyRegistredException()
        {
        }

        public EmailAlreadyRegistredException(string message) : base(message)
        {
        }

        public EmailAlreadyRegistredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public EmailAlreadyRegistredException(string? message, string email) : base(message)
        {
            Email = email;
        }

        public string Email { get; private set; }
    }
}
