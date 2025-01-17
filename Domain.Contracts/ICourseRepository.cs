using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public  interface ICourseRepository 
    {
        Task<List<Course>> GetAllCoursesAsync(bool trackChanges = false);
        void Create(Course entity);
        Task<Course?> GetCourseByIdAsync(int courseId, bool trackChanges = false);
        void Update(Course entity);
        void Delete(Course entity);
    }
}
