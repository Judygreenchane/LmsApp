using Domain.Contracts;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMS.Infrastructure.Repositories
{
    public class ModuleRepository : RepositoryBase<Module>, IModuleRepository
    {
        public ModuleRepository(LmsContext context) : base(context)
        {
        }

        public IQueryable<Module> FindAll(bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (includeActivities && includeDocuments) return base.FindAll(trackChanges).Select(m => new Module
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Course = m.Course,
                Activities = m.Activities.Select(a => new Activity
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ActivityType = a.ActivityType,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Module = m,
                    Documents = a.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),

                }).ToList(),
                Documents = m.Documents.Select(d => new Document
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    UploadTime = d.UploadTime,
                }).ToList(),
            });

            if (includeActivities) return base.FindAll(trackChanges).Select(m => new Module
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Course = m.Course,
                Activities = m.Activities.Select(a => new Activity
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ActivityType = a.ActivityType,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Module = m,
                    Documents = a.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),

                }).ToList(),
                Documents = m.Documents,
            });

            if (includeDocuments) return base.FindAll(trackChanges).Select(m => new Module
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Course = m.Course,
                Activities = m.Activities,
                Documents = m.Documents.Select(d => new Document
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    UploadTime = d.UploadTime,
                }).ToList(),
            });

            return base.FindAll(trackChanges);
        }

        public async Task<Module?> FindByIdAsync(int Id, bool includeActivities = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (includeActivities && includeDocuments) return await base.FindAll(trackChanges).Where(m => m.Id == Id).Select(m => new Module
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Course = m.Course,
                Activities = m.Activities.Select(a => new Activity
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ActivityType = a.ActivityType,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Module = m,
                    Documents = a.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),

                }).ToList(),
                Documents = m.Documents.Select(d => new Document
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    UploadTime = d.UploadTime,
                }).ToList(),
            }).FirstOrDefaultAsync();

            if (includeActivities) return await base.FindAll(trackChanges).Where(m => m.Id == Id).Select(m => new Module
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Course = m.Course,
                Activities = m.Activities.Select(a => new Activity
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ActivityType = a.ActivityType,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    Module = m,
                    Documents = a.Documents.Select(d => new Document
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        UploadTime = d.UploadTime,
                    }).ToList(),

                }).ToList(),
                Documents = m.Documents,
            }).FirstOrDefaultAsync();

            if (includeDocuments) return await base.FindAll(trackChanges).Where(m => m.Id == Id).Select(m => new Module
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Course = m.Course,
                Activities = m.Activities,
                Documents = m.Documents.Select(d => new Document
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    UploadTime = d.UploadTime,
                }).ToList(),
            }).FirstOrDefaultAsync();
            
            return await base.FindByIdAsync(Id);
        }
    }
}
