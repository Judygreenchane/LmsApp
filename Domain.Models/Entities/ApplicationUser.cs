using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Domain.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // public int Id { get; set; }

        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the First Name is 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Last Name is 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Email is 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "IsActive status is required.")]
        public bool IsActive { get; set; }

        // Navigation Property
        public  ICollection<Document>? Documents { get; set; }
    }
}
