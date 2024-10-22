using Restaurants.Domain.Entities;
namespace Restaurants.Application.Users;
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
