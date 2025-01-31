﻿using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Course
{
    public  record CourseCreateDto : BaseCreateDto
    {
        [Required]
        public string Description { get; init; }

        [Required]
        public DateTime StartDate { get; init; }

        [Required]
        public DateTime EndDate { get; init; }

       public List<ModuleCreateDto> Modules { get; init; }
    }
}
