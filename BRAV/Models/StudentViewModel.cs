using System;
using System.ComponentModel.DataAnnotations;

namespace BRAV.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "field must be atleast 3 characters")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "field must be atleast 3 characters")]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }        

        [DataType(DataType.EmailAddress)]
        [MaxLength(64)]
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "This field is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
