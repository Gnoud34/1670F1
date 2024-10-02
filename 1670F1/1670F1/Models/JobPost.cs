using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _1670F1.Models
{
    public class JobPost
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public string RequireSkill { get; set; }
        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 99999999, ErrorMessage = "Salary must be a positive number and cannot exceed 8 digits.")]
        public int Salary { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
