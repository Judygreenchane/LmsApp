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
using Microsoft.AspNetCore.JsonPatch;

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
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetOneCourse(int id)
        {
            
            var courseDto = await _serviceManager.CourseService.GetCourseByIdAsync(id);
            return Ok(courseDto);
        }
        [HttpGet("courselist")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            var courseDtos = _serviceManager.CourseService.GetAllCourses(); //ToDo: Fix Async/not async method
            return Ok(courseDtos);
        }
        [HttpPost("createcourse")]
        public async Task<ActionResult> CreateCourse(CourseCreateDto dto)
        {
            var createdCourseDto = await _serviceManager.CourseService.CreateCourseAsync(dto);
            return Created();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchCourse(int id, JsonPatchDocument<CourseUpdateDto> patchDocument)
        {
            if (patchDocument is null) return BadRequest();

            var courseToPatch = new CourseUpdateDto();
            patchDocument.ApplyTo(courseToPatch, ModelState);

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var changedCourse = await _serviceManager.CourseService.UpdateCourseAsync(id, patchDocument);

            return Ok(changedCourse);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            await _serviceManager.CourseService.DeleteCourseAsync(id);
            return NoContent();
        }

    }
}
