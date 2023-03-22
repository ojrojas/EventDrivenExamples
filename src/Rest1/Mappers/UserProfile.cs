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