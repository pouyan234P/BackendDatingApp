using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForListDTO>().ForMember(des =>des.photourl,opt =>opt.MapFrom(src =>src.Photos.FirstOrDefault(p => p.isMain).Url)).ForMember(des =>des.age,opt =>opt.MapFrom(src => src.DateofBirth.CalculationAge()));
            CreateMap<User, UserforDetialDTO>().ForMember(des => des.Photourl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain).Url)).ForMember(des => des.age, opt => opt.MapFrom(src => src.DateofBirth.CalculationAge())).ForMember(des => des.Photoes,opt =>opt.MapFrom(src =>src.Photos));
            CreateMap<Photo, PhotoforDTO>();
            CreateMap<UserForUpdateDTO, User>();
        }
    }
}
