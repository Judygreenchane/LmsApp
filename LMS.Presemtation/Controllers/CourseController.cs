using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Course;
using Services.Contracts;

namespace LMS.Presemtation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CourseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            var courseDtos = await _serviceManager.CourseService.GetAllCoursesAsync();
            return Ok(courseDtos);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCourse(CourseCreateDto dto)
        {
            var createdCourseDto = await _serviceManager.CourseService.CreateCourseAsync(dto);
            return Created();
        }

    }
}
