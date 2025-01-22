using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Module;


namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>();
        CreateMap<CourseDto, Course>().ReverseMap();
        CreateMap<CourseCreateDto, Course>().ReverseMap();
        CreateMap<CourseUpdateDto, Course>().ReverseMap();
        CreateMap<ModuleDto, Module>().ReverseMap();
        CreateMap<ActivityDto, Activity>().ReverseMap();
        CreateMap<ActivityTypeDto, ActivityType>().ReverseMap();
    }
  }
