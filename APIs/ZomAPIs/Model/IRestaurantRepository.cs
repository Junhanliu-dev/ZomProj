using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZomAPIs.Model
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();

        Task<Restaurant> GetRestaurant(string id);

        Task<Restaurant> GetRestaurantByName(string name);


    }
}