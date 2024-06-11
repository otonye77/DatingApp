using AutoMapper;
using DatingApp.DTOs;
using DatingApp.Entities;

namespace DatingApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<Photo, PhotoDto>();
        }
    }
}
