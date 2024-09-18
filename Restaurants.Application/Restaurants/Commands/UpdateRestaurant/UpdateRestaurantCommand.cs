using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand(int id) : IRequest<bool>
{
    public int Id { get; } = id;
}
