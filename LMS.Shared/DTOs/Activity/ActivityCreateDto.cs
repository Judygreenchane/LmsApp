using LMS.Shared.DTOs.ActivityType;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Document;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Activity
{
    public record ActivityCreateDto : BaseCreateDto
    {
        [Required]
        public string Description { get; init; }

        [Required]
        public DateTime StartTime { get; init; }

        [Required]
        public DateTime EndTime { get; init; }
        [Required]
        public int ActivityTypeId { get; set; }
        public int? ModuleId { get; set; }
        public List<DocumentInCollectionDto>? Documents { get; init; }
    }
}
