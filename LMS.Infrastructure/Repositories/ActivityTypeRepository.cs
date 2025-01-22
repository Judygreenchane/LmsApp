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
    public class ActivityTypeRepository : RepositoryBase<ActivityType>, IActivityTypeRepository
    {
        public ActivityTypeRepository(LmsContext context) : base(context)
        {
        }

        public IQueryable<ActivityType> FindAll(bool includeActivities = false, bool trackChanges = false)
        {
            if (includeActivities) return base.FindAll(trackChanges).Include(m => m.Activities);

            return base.FindAll(trackChanges);
        }
        public async Task<ActivityType?> FindByIdAsync(int Id,bool includeActivities = false, bool trackChanges = false)
        {
            if (includeActivities) return await FindAll(includeActivities ,trackChanges).FirstOrDefaultAsync(at => at.Id == Id);

            return await FindByIdAsync(Id);
        }
    }
}
