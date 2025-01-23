using AutoMapper;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.ActivityType;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Document;
using LMS.Shared.DTOs.Module;


namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>();
        CreateMap<CourseDto, Course>().ReverseMap();
        CreateMap<CourseForOtherDto, Course>().ReverseMap();
        CreateMap<CourseCreateDto, Course>().ReverseMap();
        CreateMap<CourseUpdateDto, Course>().ReverseMap();
        CreateMap<ModuleDto, Module>().ReverseMap();
        CreateMap<ModuleInCollectionDto, Module>().ReverseMap();
        CreateMap<ModuleCreateDto, Module>().ReverseMap();
        CreateMap<ModuleUpdateDto, Module>().ReverseMap();
        CreateMap<ActivityDto, Activity>().ReverseMap();
        CreateMap<ActivityInCollectionDto, Activity>().ReverseMap();
        CreateMap<ActivityForActivityTypeDto, Activity>().ReverseMap();
        CreateMap<ActivityCreateDto, Activity>().ReverseMap();
        CreateMap<ActivityUpdateDto, Activity>().ReverseMap();
        CreateMap<ActivityTypeDto, ActivityType>().ReverseMap();
        CreateMap<ActivityTypeCreateDto, ActivityType>().ReverseMap();
        CreateMap<ActivityTypeUpdateDto, ActivityType>().ReverseMap();
        CreateMap<DocumentDto, Document>().ReverseMap();
        CreateMap<DocumentInCollectionDto, Document>().ReverseMap();
        CreateMap<DocumentCreateDto, Document>().ReverseMap();
        CreateMap<DocumentUpdateDto, Document>().ReverseMap();
    }
  }
