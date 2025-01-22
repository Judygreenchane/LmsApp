using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        //IQueryable<Course> FindAll(bool includeModules = false, bool includeDocuments = false, bool trackChanges = false);
    }
}
