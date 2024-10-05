using System.ComponentModel.DataAnnotations;

namespace _1670F1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        [Range(18, 100, ErrorMessage = "Age must be between 16 and 100.")]
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and contain exactly 10 digits.")]
        [StringLength(10, ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }
        public string Skill { get; set; }
        public string Image { get; set; }

    }
}
