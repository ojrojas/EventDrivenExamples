using AutoMapper;
using EventDrivenDesign.Rest1.Dtos;
using EventDrivenDesign.Rest1.Models;

namespace EventDrivenDesign.Rest1.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}