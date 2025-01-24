using Domain.Models.Entities;
using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Course;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Document
{
    public record DocumentCreateDto : BaseCreateDto
    {
        public string Description { get; set; }
        [Required]
        public DateTime UploadTime { get; set; }
        public string? FilePath { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
    }
}
