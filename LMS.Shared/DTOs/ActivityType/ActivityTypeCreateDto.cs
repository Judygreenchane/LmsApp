﻿using LMS.Shared.DTOs.BaseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.ActivityType
{
    public record ActivityTypeCreateDto : BaseCreateDto
    {
        [Required]
        public string Description { get; init; }

        [Required]
        public DateTime StartTime { get; init; }

        [Required]
        public DateTime EndTime { get; init; }
    }
}
