using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ZomAPIs.Model;

namespace ZomAPIs.Controllers
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("[Controller]")]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Restaurant>> Get()
        {
            return await _restaurantRepository.GetAllRestaurants();
        }
        
        //api/restaurant/byRating?rating= 1.1
        [HttpGet("rating/{rating}")]
        public async Task<IEnumerable<Restaurant>> GetByRating(double rating)
        {
            return await _restaurantRepository.GetByRating(rating);
        }
        
        
    }
}