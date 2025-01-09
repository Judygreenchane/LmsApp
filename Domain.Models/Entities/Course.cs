using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Course Name is 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is a required field.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is a required field.")]
        public DateTime EndDate { get; set; }



        // Navigation Properties
        public  ICollection<ApplicationUser>? Users { get; set; }
        public  ICollection<Module>? Modules { get; set; }
        public  ICollection<Document>? Documents { get; set; }
    }
}
