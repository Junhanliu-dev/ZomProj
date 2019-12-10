using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZomAPIs.Model;

namespace ZomAPIs.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Restaurant>> Get()
        {
            return await _restaurantRepository.GetAllRestaurants();
        }
    }
}