using LMS.Shared.DTOs.ActivityType;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Document;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Activity
{
    public record ActivityInCollectionDto : BaseDto
    {
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ActivityTypeDto ActivityType { get; set; }
        public List<DocumentInCollectionDto> Documents { get; init; }
    }
}
