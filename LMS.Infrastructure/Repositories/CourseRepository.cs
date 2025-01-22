using Domain.Contracts;
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
        //public async Task<List<Course>> GetAllCoursesAsync(bool trackChanges = false)
        //{
        //    return await FindAll(trackChanges)
        //        .Include(c => c.Modules)
        //         .ToListAsync();
        //}
        public async Task<Course?> getcoursebyidasync(int courseId, bool trackchanges = false)
        {
            return await
                FindByCondition(c => c.Id.Equals(courseId), trackChanges)
                .include(c => c.modules)
                .theninclude(m => m.activities)
                .theninclude(a => a.activitytype)
                .firstordefaultasync();


             public async Task<Course?> GetCourseByIdAsync(int courseId, bool trackChanges = false)
        {
            return await
               

                .FirstOrDefaultAsync();
        }
    }
    }
}
