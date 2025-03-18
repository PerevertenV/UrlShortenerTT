using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess.Entities;
using UrlShortener.Models;

namespace UrlShortener.DataAccess
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
            CreateMap<UrlEntity, UrlModel>();
            CreateMap<UrlModel, UrlEntity>();
        }
    }
}
