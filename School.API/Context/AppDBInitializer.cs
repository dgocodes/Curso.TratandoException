namespace School.API.Context
{
    public class AppDBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                if (!context.Students.Any())
                {
                    context.Students.AddRange
                    (
                        new Models.Student("Maria", "maria@email.com", DateOnly.FromDateTime(DateTime.Now.AddYears(-5))),
                        new Models.Student("João", "joao@email.com", DateOnly.FromDateTime(DateTime.Now.AddYears(-16))),
                        new Models.Student("Fulano", "fulano@email.com", DateOnly.FromDateTime(DateTime.Now.AddYears(-18))),
                        new Models.Student("Ciclano", "cliclano@email.com", DateOnly.FromDateTime(DateTime.Now.AddYears(-40))),
                        new Models.Student("Beltrano", "beltrano@email.com", DateOnly.FromDateTime(DateTime.Now.AddYears(-20)))
                    );

                    context.SaveChangesAsync();
                }

            }
        }
    }
}
