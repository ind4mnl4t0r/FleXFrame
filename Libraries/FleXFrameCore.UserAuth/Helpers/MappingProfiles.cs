using AutoMapper;
using FleXFrameCore.UserAuth.DTOs;
using FleXFrameCore.UserAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Mapping from User to UserViewDto (read-only)
            CreateMap<User, UserViewDto>();

            // Mapping from UserCreateDto to User (read-write)
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => src.PasswordSalt));

            // If you need to convert from User back to UserCreateDto (e.g., for updates)
            CreateMap<User, UserViewDto>();
            CreateMap<User, UserCreateDto>();
        }
    }
}
