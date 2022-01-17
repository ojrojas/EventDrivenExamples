using AutoMapper;
using EventDrivenDesign.Rest2.Dtos;
using EventDrivenDesign.Rest2.Models;

namespace EventDrivenDesign.Rest2.Mappers
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
             CreateMap<User, UserDto>().ReverseMap();
        }
    }
}