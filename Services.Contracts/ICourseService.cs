using LMS.Shared.DTOs.Course;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<CourseDto> CreateCourseAsync(CourseCreateDto dto);
        Task DeleteCourseAsync(int id);
        IEnumerable<CourseDto> GetAllCourses();
        Task<CourseDto> GetCourseByIdAsync(int courseId);
        Task<CourseDto> UpdateCourseAsync(int id, JsonPatchDocument<CourseUpdateDto> patchDocument);
    }
}