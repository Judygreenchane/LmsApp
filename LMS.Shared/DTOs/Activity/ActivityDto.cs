using LMS.Shared.DTOs.ActivityType;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Activity
{
    public record ActivityDto : BaseDto
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<DocumentDto> Documents { get; init; }

        public  ActivityTypeDto ActivityType { get; set; }
    }
}
