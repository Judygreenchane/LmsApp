using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.Course;


namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>();
        CreateMap<CourseDto, Course>().ReverseMap();
        CreateMap<CourseCreateDto, Course>().ReverseMap();
    }
  }
