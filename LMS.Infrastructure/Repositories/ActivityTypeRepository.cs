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
    }
}
