using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Contans;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();

            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
       List<IdentityRole> roles =
    [
        new IdentityRole(UserRoles.User)
        {
            NormalizedName = UserRoles.User.ToUpper()
        },
        new IdentityRole(UserRoles.Owner)
        {
            NormalizedName = UserRoles.Owner.ToUpper()
        },
        new IdentityRole(UserRoles.Admin)
        {
            NormalizedName = UserRoles.Admin.ToUpper()
        }

    ];
        return roles;


    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = new List<Restaurant>
{
    new Restaurant
    {
        Name = "KFC",
        Category = "Fast Food",
        Description = "KFC (Kentucky Fried Chicken), merkezi Amerika'da olan ünlü bir fast food zinciridir.",
        ContactEmail = "contact@kfc.com",
        HasDelivery = true,
        Dishes = new List<Dish>
        {
            new Dish
            {
                Name = "Nashville Hot Chicken",
                Description = "Nashville Hot Chicken (10 pcs.)",
                Price = 10.30M
            },
            new Dish
            {
                Name = "Chicken Nuggets",
                Description = "Chicken Nuggets (5 pcs.)",
                Price = 5.30M
            }
        },
        Address = new Address
        {
            City = "London",
            Street = "Cork St 5",
            PostalCode = "WC2N 5DU"
        }
    },
    new Restaurant
    {
        Name = "McDonald's",
        Category = "Fast Food",
        Description = "McDonald's, dünya genelinde faaliyet gösteren bir fast food restoran zinciridir.",
        ContactEmail = "contact@mcdonald.com",
        HasDelivery = true,
        Address = new Address
        {
            City = "London",
            Street = "Bootle St 193",
            PostalCode = "W1F 8SR"
        }
    }
};


        return restaurants;
    }
}

