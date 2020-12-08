using System;
using System.ComponentModel.DataAnnotations;
using techloops.Common;

namespace techloops.Models
{
    public class Freelancer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(150)]
        public string Skills { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Date of Availability")]
        [DateRange]
        public DateTime? DateApplied { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(50)]
        public string Country { get; set; }

    }
}
