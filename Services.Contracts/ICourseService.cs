using LMS.Shared.DTOs.Course;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    public interface ICourseService : IServiceWithCollection<CourseDto, CourseCreateDto, CourseUpdateDto>
    {
    }
}