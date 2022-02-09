using Microsoft.EntityFrameworkCore;
using School.API.Context;
using School.API.Exceptions.Middleware;
using School.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.AddDateOnlyConverters()
                                                                        .AddTimeOnlyConverters());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(opt => opt.UseInMemoryDatabase("School-DBMemory"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    AppDBInitializer.Seed(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Exception Handlers 
//app.ConfigureBuildInExceptionHandler();
app.ConfigureCustomExceptionHandler();

app.MapControllers();

app.Run();
