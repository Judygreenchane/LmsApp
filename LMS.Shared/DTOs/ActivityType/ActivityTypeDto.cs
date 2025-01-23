using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.BaseDtos;

namespace LMS.Shared.DTOs.ActivityType
{
    public record ActivityTypeDto : BaseDto
    {
        public ICollection<ActivityForActivityTypeDto>? Activities { get; set; }

    }
}
