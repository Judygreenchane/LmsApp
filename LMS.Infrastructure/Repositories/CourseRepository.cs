using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Module;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(LmsContext context) : base(context)
        {
        }
        public IQueryable<Course> FindAll(bool includeModules = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (includeModules && includeDocuments) return base.FindAll(trackChanges)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Name = c.Name,
                    Users = c.Users,
                    Documents = c.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),
                    Modules = c.Modules.Select(m => new Module
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description = m.Description,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        Course = c,
                        Activities = m.Activities.Select(a => new Activity
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            ActivityType = a.ActivityType,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Module = m,
                            Documents = c.Documents.Select(d => new Document
                            {
                                Id = d.Id,
                                Name = d.Name,
                                Description = d.Description,
                                UploadTime = d.UploadTime,
                            }).ToList(),

                        }).ToList(),
                        Documents = c.Documents.Select(d => new Document
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Description = d.Description,
                            UploadTime = d.UploadTime,
                        }).ToList(),
                    }
                    ).ToList()
                });

            if (includeModules) return base.FindAll(trackChanges)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Name = c.Name,
                    Users = c.Users,
                    Documents = c.Documents,
                    Modules = c.Modules.Select(m => new Module
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description = m.Description,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        Course = c,
                        Activities = m.Activities.Select(a => new Activity
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            ActivityType = a.ActivityType,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Module = m,
                            Documents = c.Documents.Select(d => new Document
                            {
                                Id = d.Id,
                                Name = d.Name,
                                Description = d.Description,
                                UploadTime = d.UploadTime,
                            }).ToList(),

                        }).ToList(),
                        Documents = c.Documents.Select(d => new Document
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Description = d.Description,
                            UploadTime = d.UploadTime,
                        }).ToList(),
                    }
                    ).ToList()
                });

            if (includeDocuments) return  base.FindAll(trackChanges)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Name = c.Name,
                    Users = c.Users,
                    Documents = c.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),
                    Modules = c.Modules
                });
            return base.FindAll();
        }

        public async Task<Course?> FindByIdAsync(int Id,bool includeModules = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (includeModules && includeDocuments) return await base.FindAll(trackChanges)
                .Where(c => c.Id == Id)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Name = c.Name,
                    Users = c.Users,
                    Documents = c.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),
                    Modules = c.Modules.Select(m => new Module
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description= m.Description,
                        StartDate= m.StartDate,
                        EndDate= m.EndDate,
                        Course = c,
                        Activities = m.Activities.Select(a => new Activity
                        {
                            Id= a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            ActivityType = a.ActivityType,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Module = m,
                            Documents = c.Documents.Select(d => new Document
                            {
                                Id = d.Id,
                                Name = d.Name,
                                Description = d.Description,
                                UploadTime = d.UploadTime,
                            }).ToList(),
                            
                        }).ToList(),
                        Documents = c.Documents.Select(d => new Document
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Description = d.Description,
                            UploadTime = d.UploadTime,
                        }).ToList(),
                    }
                    ).ToList()
                }).FirstOrDefaultAsync();
                
            if (includeModules) return await base.FindAll(trackChanges)
                .Where(c => c.Id == Id)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Name = c.Name,
                    Users = c.Users,
                    Documents = c.Documents,
                    Modules = c.Modules.Select(m => new Module
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description= m.Description,
                        StartDate= m.StartDate,
                        EndDate= m.EndDate,
                        Course = c,
                        Activities = m.Activities.Select(a => new Activity
                        {
                            Id= a.Id,
                            Name = a.Name,
                            Description = a.Description,
                            ActivityType = a.ActivityType,
                            StartTime = a.StartTime,
                            EndTime = a.EndTime,
                            Module = m,
                            Documents = c.Documents.Select(d => new Document
                            {
                                Id = d.Id,
                                Name = d.Name,
                                Description = d.Description,
                                UploadTime = d.UploadTime,
                            }).ToList(),
                            
                        }).ToList(),
                        Documents = c.Documents.Select(d => new Document
                        {
                            Id = d.Id,
                            Name = d.Name,
                            Description = d.Description,
                            UploadTime = d.UploadTime,
                        }).ToList(),
                    }
                    ).ToList()
                }).FirstOrDefaultAsync();

            if (includeDocuments) return await base.FindAll(trackChanges)
                .Where(c => c.Id == Id)
                .Select(c => new Course
                {
                    Id = c.Id,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Name = c.Name,
                    Users = c.Users,
                    Documents = c.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),
                    Modules = c.Modules
                }).FirstOrDefaultAsync();
            return await base.FindByIdAsync(Id);
        }
    }
}
