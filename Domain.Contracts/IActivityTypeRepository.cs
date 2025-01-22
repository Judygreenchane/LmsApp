using Domain.Contracts;
using Domain.Models.Entities;

namespace LMS.Infrastructure.Repositories
{
    public interface IActivityTypeRepository : IRepositoryBase<ActivityType>
    {
        IQueryable<ActivityType> FindAll(bool includeActivities, bool trackChanges = false);
        Task<ActivityType?> FindByIdAsync(int Id,bool includeActivities, bool trackChanges = false);
    }
}