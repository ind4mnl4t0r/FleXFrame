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
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())  // Optional: Ignore PasswordHash if not setting it here
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());  // Optional: Ignore PasswordSalt if not setting it here

            // If you need to convert from User back to UserCreateDto (e.g., for updates)
            CreateMap<User, UserCreateDto>();
        }
    }
}
