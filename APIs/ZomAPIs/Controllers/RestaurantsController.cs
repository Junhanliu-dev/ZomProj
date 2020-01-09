using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ZomAPIs.Model;
using ZomAPIs.Model.DTOs;

namespace ZomAPIs.Controllers
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("[Controller]")]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public RestaurantsController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        
        //Get all restaurants
        public async Task<IEnumerable<RestaurantDTO>> Index()
        {
            var result = await _restaurantRepository.GetAllRestaurants();
            
            return _mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDTO>>(result);
        }
        
        //api/restaurant/byRating?rating= 1.1
        [HttpGet("rating/{rating}")]
        public async Task<IEnumerable<RestaurantDTO>> GetByRating(double rating)
        {   
            var result = await _restaurantRepository.GetByRating(rating);
            
            return _mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDTO>>(result);;    
        }

        [HttpGet("restaurant/{id}")]
        public async Task<ActionResult<RestaurantDTO>> GetById(Int64 id)
        {
            var result = await _restaurantRepository.GetById(id);

            if (result != null)
            {
                return Ok(_mapper.Map<Restaurant,RestaurantDTO>(result));
            }
            return NotFound();

        }

        [HttpGet("{area}")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetByArea(string area)
        {
            var result = await _restaurantRepository.GetByArea(area);

            if (result != null)
            {
                return Ok(_mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDTO>>(result));
            }

            return NotFound();
        }
        
    }
}