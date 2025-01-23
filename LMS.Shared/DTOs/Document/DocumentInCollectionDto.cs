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
    public record DocumentInCollectionDto : BaseDto
    {
        public string Description { get; set; }
        public DateTime UploadTime { get; set; }
        //public int ApplicationUserId { get; set; }
    }
}
