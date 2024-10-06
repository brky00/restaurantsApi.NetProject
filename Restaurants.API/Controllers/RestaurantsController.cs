using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
  
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()

    {

        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants); // Restoran listesini JSON formatında döndürüyoruz
        //var currentUser = HttpContext.User;
        //if (currentUser.Identity.IsAuthenticated)
        //{
        //    var userName = currentUser.Identity.Name;
        //    // Kullanıcının claim'lerini (haklarını) alabiliriz
        //    var claims = currentUser.Claims;
        //}
        //else
        //{
        //    // Kullanıcı oturum açmamış
        //}
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto?>> GetById([FromRoute] int id)
    {
      


        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
     
            return Ok(restaurant);
       
       
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
 
         return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
    {
        //Vi kunne brukt denne validation metoden under her også med ModelState objektet for å sjekke om entityen er valid istedenfor [ApiController] som gjør dette automatisk.
        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}

        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById),new {id},null);
       
    }

    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command )
    {
        command.Id = id;
       
      await mediator.Send(command);
      
      return NoContent();

       
    }


}
