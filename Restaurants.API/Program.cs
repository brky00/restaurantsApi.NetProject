
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Serilog;
using Restaurants.API.Middelwares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extensions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.AddPresentation();

//

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);







var app = builder.Build();

// Scope olusturuyoruz
var scope = app.Services.CreateScope();

// ilgili servisi aliyoruz
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

// Seeding i?lemini ba?lat?yoruz
await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

app.UseHttpsRedirection();

app.MapGroup("api/identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
