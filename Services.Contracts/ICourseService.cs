using LMS.Shared.DTOs.Course;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> CreateCourseAsync(CourseCreateDto dto);
        Task<CourseDto> GetCourseByIdAsync(int courseId);
        Task<CourseDto> UpdateCourseAsync(int id, JsonPatchDocument<CourseUpdateDto> patchDocument);
        Task DeleteCourseAsync(int id);
    }
}
