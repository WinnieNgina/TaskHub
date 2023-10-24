using AutoMapper;
using TaskHub.Dto;
using TaskHub.Models;

namespace TaskHub.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
        }
    }
}
