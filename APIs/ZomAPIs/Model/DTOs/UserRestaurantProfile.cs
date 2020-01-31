using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ZomAPIs.Model.MySql;

namespace ZomAPIs.Model.DTOs
{
    public class UserRestaurantProfile : Profile
    {
        public UserRestaurantProfile()
        {
            CreateMap<RestaurantInfo, RestaurantInfoDTO>(
                
                );
            
            CreateMap<UserRestaurant, UserRestaurantDTO>();

            CreateMap<User, UserResDTO>()
                .ForMember(
                    dest => dest.userRestaurantList,
                    opt => opt.MapFrom(
                        src => src.UserRestaurants
                        )
                    );

        }
    }
}