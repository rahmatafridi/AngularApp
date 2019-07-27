using AutoMapper;
using CoreApi.Dtos;
using CoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, opt=>{ 
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p=>p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
            opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });
            CreateMap<User, UserForDetailDto>()
            .ForMember(dest => dest.PhotoUrl, opt=>{ 
                opt.MapFrom(src=> src.Photos.FirstOrDefault(p=>p.IsMain).Url);
            })
            .ForMember(dest => dest.Age, opt => {
            opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });
            CreateMap<Photo, PhotosForDetailDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, photoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto,User>();

        }

        internal object Map<T>(UserForRegisterDto userForRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
