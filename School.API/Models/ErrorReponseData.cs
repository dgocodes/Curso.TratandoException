using System.Text.Json;

namespace School.API.Models
{
    public class ErrorReponseData
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
