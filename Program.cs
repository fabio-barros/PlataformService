using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMemDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
builder.Services.AddScoped<IPlatformRepository, PlatformRepo>();
builder.Services.AddControllers();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();

PrepDb.PrepPopulation(app);

app.Run();
