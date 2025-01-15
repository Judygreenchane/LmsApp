using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public DateTime UploadTime { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //  Course
        public int? CourseId { get; set; } // Nullable
        public Course? Course { get; set; } // Nullable

        // Module
        public int? ModuleId { get; set; } // Nullable
        public Module? Module { get; set; } // Nullable

        // Activity
        public Guid? ActivityId { get; set; } // Nullable 
        public Activity? Activity { get; set; } // Nullable 
    }
}