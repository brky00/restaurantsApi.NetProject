using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Users;


namespace Restaurants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);
        //BU DA KULLANILABILIRDI: services.AddAutoMapper(typeof(DishesProfile), typeof(RestaurantsProfile));


        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();//bunun sayesinde Controller sinifinda  validateyi esktra kontrol etmeye gerek yok

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();



    }

}