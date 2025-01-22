using LMS.Shared.DTOs.ActivityType;

namespace Services.Contracts
{
    public interface IActivityTypeService : IServiceBase<ActivityTypeDto, ActivityTypeCreateDto, ActivityTypeUpdateDto>
    {
        IEnumerable<ActivityTypeDto> FindAll(bool includeActivities = false, bool trackChanges = false);
        Task<ActivityTypeDto> FindByIdAsync(int Id, bool includeActivities = false, bool trackChanges = false);
    }
}