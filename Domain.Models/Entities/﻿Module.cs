using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Module name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Module Name is 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is a required field.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is a required field.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Course ID is a required field.")]
        public int? CourseId { get; set; }

        // Navigation Properties
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
        public ICollection<Activity>? Activities { get; set; }
        public ICollection<Document>? Documents { get; set; }
    }
}