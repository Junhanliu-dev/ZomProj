using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ZomAPIs.Model.DTOs;

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
    }
}