using System;
using System.Collections.Generic;
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

        public Task<Restaurant> GetRestaurant(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Restaurant> GetRestaurantByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}