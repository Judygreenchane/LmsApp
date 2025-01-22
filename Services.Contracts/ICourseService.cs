using LMS.Shared.DTOs.Course;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    public interface ICourseService // todo, implement base interface
    {
        Task<CourseDto> CreateCourseAsync(CourseCreateDto dto);
        Task DeleteCourseAsync(int id);
        IEnumerable<CourseDto> GetAllCourses(bool includeModules = false, bool includeDocuments = false, bool trackChanges = false);
        Task<CourseDto> GetCourseByIdAsync(int courseId);
        Task<CourseDto> UpdateCourseAsync(int id, JsonPatchDocument<CourseUpdateDto> patchDocument);
    }
}