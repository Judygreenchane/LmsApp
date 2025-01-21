using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Activity : BaseEntity
    {
        [Required(ErrorMessage = "Activity name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Activity Name is 100 characters.")]
        public override string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Activity type ID is a required field.")]
        public int ActivityTypeId { get; set; }

        [Required(ErrorMessage = "Start time is a required field.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is a required field.")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Module ID is a required field.")]
        public int? ModuleId { get; set; }

        // Navigation Properties
        [ForeignKey("ActivityTypeId")]
        public ActivityType? ActivityType { get; set; }
        [ForeignKey("ModuleId")]
        public  Module Module { get; set; } = null!;
        public ICollection<Document> Documents { get; set; }

    }
}