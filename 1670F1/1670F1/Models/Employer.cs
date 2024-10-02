using System;
using System.ComponentModel.DataAnnotations;

namespace _1670F1.Models
{
    public class Employer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name must contain only letters and spaces.")]
        public string FullName { get; set; }
        public DateTime DateTime { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be either 'Male', 'Female', or 'Other'.")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 100, ErrorMessage = "Age must be between 16 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[\w-\.]+@gmail\.com$", ErrorMessage = "Only Gmail addresses are allowed.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and contain exactly 10 digits.")]
        [StringLength(10, ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        [RegularExpression(@"(https?:\/\/.*\.(?:png|jpg|jpeg))", ErrorMessage = "URL must point to a valid image (jpg, jpeg, png).")]
        public string Image { get; set; }
    }
}
