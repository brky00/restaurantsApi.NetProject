
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context,configration) =>
{
    configration.ReadFrom.Configuration(context.Configuration);
    

});


var app = builder.Build();

// Scope olu?turuyoruz
var scope = app.Services.CreateScope();

// ?lgili servisi al?yoruz
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

// Seeding i?lemini ba?lat?yoruz
await seeder.Seed();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
