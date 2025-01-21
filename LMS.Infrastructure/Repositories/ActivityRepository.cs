using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
        public ActivityRepository(LmsContext context) : base(context)
        {
        }

        public IQueryable<Activity> FindAll(bool includeDocuments = false, bool trackChanges = false)
        {
            if(includeDocuments)return base.FindAll(trackChanges).Include(m => m.Documents);

            return base.FindAll(trackChanges);
        }

    }
}
