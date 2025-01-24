using LMS.Shared.DTOs.Activity;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Module
{
    public record ModuleCreateDto : BaseCreateDto
    {
        [Required]
        public string Description { get; init; }

        [Required]
        public DateTime StartDate { get; init; }

        [Required]
        public DateTime EndDate { get; init; }
        public int? CourseId { get; set; }
        public List<ActivityCreateDto>? Activities { get; set; }
        public List<DocumentCreateDto>? Documents { get; set; }
    }
}
