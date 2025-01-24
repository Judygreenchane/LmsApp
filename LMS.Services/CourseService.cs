using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Course;
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
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<bool> AnyAsync()
        {
            return await uow.CourseRepository.AnyAsync();
        }

        public async Task<bool> AnyAsync(int Id)
        {
            return await uow.CourseRepository.AnyAsync(Id);
        }

        public async Task<CourseDto> CreateAsync(CourseCreateDto dto)
        {
            Course course = mapper.Map<Course>(dto);

            uow.CourseRepository.Create(course);
            await uow.CompleteAsync();

            return mapper.Map<CourseDto>(course);
        }
        public async Task<CourseDto> GetCourseByUserIdAsync(string userId)
        {
            var course = await uow.CourseRepository.GetCourseByUserIdAsync(userId);
            if (course == null)
            {
                throw new KeyNotFoundException($"not found");
            }
            return mapper.Map<CourseDto>(course);
        }
        public async Task DeleteAsync(int id)
        {
            Course? courseToDelete = await uow.CourseRepository.FindByIdAsync(id)
                ?? throw new KeyNotFoundException($"Course with id: {id} not found.");
            uow.CourseRepository.Delete(courseToDelete);

            await uow.CompleteAsync();
        }

        public IEnumerable<CourseDto> FindAll(bool includecourses = false, bool includeDocuments = false, bool trackChanges = false)
        {
            var courses = uow.CourseRepository.FindAll(includecourses, includeDocuments, trackChanges);
            return mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> FindByIdAsync(int Id)
        {
            Course? course = await uow.CourseRepository.FindByIdAsync(Id);
            return course == null
                ? throw new KeyNotFoundException($"course with id: {Id} not found") : mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> FindByIdAsync(int Id, bool includecourses = false, bool includeDocuments = false, bool trackChanges = false)
        {
            Course? course = await uow.CourseRepository.FindByIdAsync(Id, includecourses, includeDocuments, trackChanges);
            return course == null
                ? throw new KeyNotFoundException($"course with id: {Id} not found") : mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> UpdateAsync(int id, JsonPatchDocument<CourseUpdateDto> patchDocument)
        {
            var courseToPatch = await uow.CourseRepository.FindByIdAsync(id)
                ?? throw new KeyNotFoundException($"{id} not found.");
            var course = mapper.Map<CourseUpdateDto>(courseToPatch);
            patchDocument.ApplyTo(course);

            mapper.Map(course, courseToPatch);
            await uow.CompleteAsync();

            return mapper.Map<CourseDto>(courseToPatch);
        }

        public async Task<CourseDto> PutAsync(int id, CourseUpdateDto dto)
        {
            Course? courseToPut = await uow.CourseRepository.FindByIdAsync(id) ?? throw new NullReferenceException("Course not found");

            mapper.Map(dto, courseToPut);

            await uow.CompleteAsync();

            return mapper.Map<CourseDto>(courseToPut);
        }
    }
}
