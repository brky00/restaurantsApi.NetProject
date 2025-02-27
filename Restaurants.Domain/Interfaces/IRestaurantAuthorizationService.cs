﻿using Restaurants.Domain.Contans;
using Restaurants.Domain.Entities;


namespace Restaurants.Domain.Interfaces;
public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
}