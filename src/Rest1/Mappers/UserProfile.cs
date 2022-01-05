using AutoMapper;
using Rest1.Dtos;
using Rest1.Models;

namespace Rest1.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}