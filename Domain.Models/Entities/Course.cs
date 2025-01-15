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
        public int Id { get; set; } 

        [Required]
        [MaxLength(255)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public ICollection<Module> Modules { get; set; } 
        public ICollection<Document> Documents { get; set; } 
    }

}