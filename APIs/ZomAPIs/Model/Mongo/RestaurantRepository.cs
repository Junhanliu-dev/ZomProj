using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ZomAPIs.Model
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly MongoContext _context = null;
        
        public RestaurantRepository(IOptions<Settings> Settings)
        {
            _context = new MongoContext(Settings);
        }
        
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            try
            {
                return await _context.Restaurants.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        
        public async Task<IEnumerable<Restaurant>> GetByRating(double rating)
        {
            try
            {
                return await _context.Restaurants.Find(res => res.Rating >= rating).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task<Restaurant> GetById(long id)
        {
            try
            {
                return await _context.Restaurants.Find(res => res.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}