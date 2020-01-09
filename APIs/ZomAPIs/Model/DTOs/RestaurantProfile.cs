using AutoMapper;

namespace ZomAPIs.Model.DTOs
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>();
        }
    }
}