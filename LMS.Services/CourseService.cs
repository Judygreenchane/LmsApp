using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Course;
using Microsoft.AspNetCore.JsonPatch;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public IEnumerable<CourseDto> GetAllCourses()
        {
            var courses = _uow.CourseRepository.FindAll();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
        public async Task<CourseDto> GetCourseByIdAsync(int courseId)
        {
             Course? course = await _uow.CourseRepository.FindByIdAsync(courseId);
            //Course? course = await _uow.CourseRepository.FindByCondition()
            if (course == null)
            {
                //Todo
            }
            return _mapper.Map<CourseDto>(course);
        }
        public async Task<CourseDto> CreateCourseAsync(CourseCreateDto dto)
        {
            Course course = _mapper.Map<Course>(dto);

            _uow.CourseRepository.Create(course);

            await _uow.CompleteAsync();

            return _mapper.Map<CourseDto>(course);
        }
        public async Task<CourseDto> UpdateCourseAsync(int id, JsonPatchDocument<CourseUpdateDto> patchDocument)
        {
            var courseToPatch = await _uow.CourseRepository.FindByIdAsync(id);
            if (courseToPatch == null) throw new KeyNotFoundException($"{id} not found.");

            var course = _mapper.Map<CourseUpdateDto>(courseToPatch);
            patchDocument.ApplyTo(course);

            _mapper.Map(course, courseToPatch);
            await _uow.CompleteAsync();

            return _mapper.Map<CourseDto>(courseToPatch);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var courseToDelete = await _uow.CourseRepository.FindByIdAsync(id);
            if (courseToDelete == null) throw new KeyNotFoundException($"{id} not found.");
            _uow.CourseRepository.Delete(courseToDelete);

            await _uow.CompleteAsync();
        }
    }
}
