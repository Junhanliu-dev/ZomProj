using System;
using System.Collections.Generic;
using System.Linq;
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
                var restaurants =  await _context.Restaurants
                    .Find(res => res.Rating >= rating)
                    .ToListAsync();
                
                return restaurants;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        public async Task<Restaurant> GetById(Int64 id)
        {
            try
            {
                return await _context.Restaurants.Find(res => res.Id == id).FirstAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetByArea(string area)
        {
           //var result = await _context.Restaurants.Find(res => res.Area.Contains(area, StringComparer.CurrentCultureIgnoreCase)).ToListAsync();

           var result = await _context.Restaurants.Find(res => res.Area.Any(i => i.ToLower() == area.ToLower())).ToListAsync();
           try
           {
                return result;
           }
           catch (Exception e)
           {
                return null;
           }
        }

        public async Task<IEnumerable<Restaurant>> GetTopNRes(int? num)
        {
            try
            {
                if (num == null)
                {
                    return await GetAllRestaurants();
                }

                return await _context.Restaurants.Find(_ => true)
                    .SortByDescending(res => res.Rating)
                    .Limit(num)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Restaurant>> GetTopNByRegion(int num, string area)
        {
            try
            {
                var result = await _context.Restaurants
                    .Find(res => res.Area.Any(i => i.ToLower() == area.ToLower()))
                    .SortByDescending(res => res.Rating)
                    .Limit(num)
                    .ToListAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}