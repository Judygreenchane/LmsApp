using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class ActivityType
    {
        [Key]
        public int ActivityTypeId { get; set; }

        [Required(ErrorMessage = "Activity type name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Activity Type Name is 50 characters.")]
        public string Name { get; set; }

        // Navigation Property
        public ICollection<Activity>? Activities { get; set; }
    }
}
