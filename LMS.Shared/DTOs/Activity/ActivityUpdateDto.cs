using LMS.Shared.DTOs.ActivityType;
using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Activity
{
    public record ActivityUpdateDto : BaseUpdateDto
    {
        [Required]
        public string Description { get; init; }

        [Required]
        public DateTime StartTime { get; init; }

        [Required]
        public DateTime EndTime { get; init; }
        public ActivityTypeDto ActivityType { get; set; }
        public ModuleDto Module { get; set; }
    }
}
