using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating restaurant with id : {request.Id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant is null)
            return false;

        await restaurantsRepository.Update(restaurant);
        return true;


    }
}
