using AutoMapper;

using DataAccessUser = Blog.DataAccess.Entities.Identity.ApplicationUser;

namespace Blog.Services.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<DataAccessUser, User>();
            CreateMap<UpdateUserRequest, DataAccessUser>();
        }
    }
}