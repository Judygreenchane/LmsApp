using Domain.Models.Entities;
using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Document
{
    public record DocumentDto : BaseDto
    {
        public string Description { get; set; }
        public DateTime UploadTime { get; set; }
        public ApplicationUser User { get; set; }
        public CourseDto? Course { get; set; }
        public ModuleDto? Module { get; set; }
        public ActivityDto? Activity { get; set; }
    }
}
