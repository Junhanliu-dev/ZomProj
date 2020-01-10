using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ZomAPIs.Model;
using ZomAPIs.Model.DTOs;

namespace ZomAPIs.Controllers
{
    [EnableCors("MyPolicy")]
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
        
        //Get all restaurants except num defined
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

        [HttpGet("top/{num}")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetTopN(int? num)
        {
            try
            {
                var result = await _restaurantRepository.GetTopNRes(num);

                return Ok(_mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDTO>>(result));;
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("top/{num}/{area}")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetTopNByRegion(int num, string area)
        {
            try
            {
                var result = await _restaurantRepository.GetTopNByRegion(num, area);

                return Ok(_mapper.Map<IEnumerable<Restaurant>,IEnumerable<RestaurantDTO>>(result));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }
    }
}