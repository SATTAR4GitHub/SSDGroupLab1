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
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string Skills { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Date of Availability")]
        [DateRange]
        public DateTime? DateApplied { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

    }
}
