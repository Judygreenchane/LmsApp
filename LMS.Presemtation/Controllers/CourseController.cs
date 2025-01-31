﻿using System;
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
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CourseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetOneCourse(int id, bool includeModules = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (!await _serviceManager.CourseService.AnyAsync(id))
            {
                return NotFound($"There is no course with id: {id}");
            }

            var courseDto = await _serviceManager.CourseService.FindByIdAsync(id, includeModules, includeDocuments, trackChanges);
            return Ok(courseDto);
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses(bool includeModules = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (!await _serviceManager.CourseService.AnyAsync())
            {
                return NotFound($"There is no courses");
            }
            var courseDtos = _serviceManager.CourseService.FindAll(includeModules, includeDocuments, trackChanges); //ToDo: Fix Call
            return Ok(courseDtos);
        }
        [HttpPost()]
        public async Task<ActionResult> CreateCourse(CourseCreateDto dto)
        {
            var createdCourseDto = await _serviceManager.CourseService.CreateAsync(dto);
            return Created();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchCourse(int id, JsonPatchDocument<CourseUpdateDto> patchDocument)
        {
            if (!await _serviceManager.CourseService.AnyAsync(id))
            {
                return NotFound($"There is no course with id: {id}");
            }

            if (patchDocument is null) return BadRequest();

            var courseToPatch = new CourseUpdateDto();
            patchDocument.ApplyTo(courseToPatch, ModelState);

            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var changedCourse = await _serviceManager.CourseService.UpdateAsync(id, patchDocument);

            return Ok(changedCourse);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            if (!await _serviceManager.CourseService.AnyAsync(id))
            {
                return NotFound($"There is no course with id: {id}");
            }

            await _serviceManager.CourseService.DeleteAsync(id);
            return NoContent();
        }

    }
}
