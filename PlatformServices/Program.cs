using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlatformService.Data;
using PlatformService.Data.Interfaces;
using PlatformService.Settings;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// if (builder.Environment.IsDevelopment())
// {
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMemDb"));
// }
// else
// {
//     builder.Services.AddDbContext<AppDbContext>(options =>
//         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// }
builder.Services.AddScoped<IPlatformRepository, PlatformRepo>();
builder.Services.AddControllers();

// Register CommandServiceSettings for DI
builder.Services.Configure<CommandServiceSettings>(builder.Configuration.GetSection("CommandService"));

// Register ICommandDataClient with HttpClient
// builder.Services.AddHttpClient<ICommandDataClient, CommandDataClient>(client =>
// {
//     var commandServiceSettings = builder.Configuration.GetSection("CommandService").Get<CommandServiceSettings>();
//     client.BaseAddress = new Uri(commandServiceSettings?.Url ?? throw new InvalidOperationException("CommandService URL is not configured."));
// });

builder.Services.AddHttpClient<ICommandDataClient, CommandDataClient>();


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
