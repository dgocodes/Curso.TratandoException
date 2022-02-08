using Microsoft.EntityFrameworkCore;
using School.API.Models;

namespace School.API.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
