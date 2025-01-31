﻿using Domain.Contracts;
using Domain.Models.Entities;
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
            if (includeModules && includeDocuments) return base.FindAll(trackChanges).Include(m => m.Modules).Include(m => m.Documents);

            if (includeModules) return base.FindAll(trackChanges).Include(m => m.Modules);

            if (includeDocuments) return base.FindAll(trackChanges).Include(m => m.Documents);

            return base.FindAll(trackChanges);
        }

        public async Task<Course?> FindByIdAsync(int Id,bool includeModules = false, bool includeDocuments = false, bool trackChanges = false)
        {
            if (includeModules && includeDocuments) return await base.FindAll(trackChanges).Include(m => m.Modules).ThenInclude(a => a.Activities).Include(m => m.Documents).FirstOrDefaultAsync(c => c.Id == Id);

            if (includeModules) return await base.FindAll(trackChanges).Include(m => m.Modules).ThenInclude(a => a.Activities).FirstOrDefaultAsync(c => c.Id == Id);

            if (includeDocuments) return await base.FindAll(trackChanges).Include(m => m.Documents).FirstOrDefaultAsync(c => c.Id == Id);

            return await base.FindByIdAsync(Id);
        }
    }
}
