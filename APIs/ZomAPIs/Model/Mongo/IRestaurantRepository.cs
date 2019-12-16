using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZomAPIs.Model
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();

        Task<IEnumerable<Restaurant>> GetByRating(double rating);

        Task<Restaurant> GetById(Int64 id);
    }
}