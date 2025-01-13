using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Document name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Document Name is 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Document type is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Document Type is 50 characters.")]
        public string DocumentType { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "File path is a required field.")]
        [MaxLength(255, ErrorMessage = "Maximum length for the File Path is 255 characters.")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "Upload date is a required field.")]
        public DateTime UploadedDate { get; set; }

        [Required(ErrorMessage = "Uploaded by user is a required field.")]
        public string UploadedByUserId { get; set; } // Foreign Key

        public int? ActivityId { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }

        // Navigation Properties
        [ForeignKey("UploadedByUserId")]
        public ApplicationUser? UploadedByUser { get; set; } // Navigation property for User

        [ForeignKey("ActivityId")]
        public Activity? Activity { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        [ForeignKey("ModuleId")]
        public Module? Module { get; set; }
    }
}
