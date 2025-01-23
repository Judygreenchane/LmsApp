using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Module
{
    public record ModuleDto : BaseDto
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CourseDto Course { get; set; }

        public List<ActivityInCollectionDto> Activities { get; set; }
        public List<DocumentInCollectionDto> Documents { get; init; }
    }
}
