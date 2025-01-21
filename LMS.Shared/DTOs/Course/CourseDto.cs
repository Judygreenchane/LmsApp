﻿using LMS.Shared.DTOs.BaseDtos;
using LMS.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.Course
{
    public record CourseDto : BaseDto
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DocumentDto> Documents { get; init; }
        public List<ModuleDto>? Modules { get; set; }
    }
}
