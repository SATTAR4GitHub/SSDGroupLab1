using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace techloops.Models
{
    public class Client
    {
        [Key]
        [Display(Name = "Contact ID")]
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

        [RegularExpression(@"^[ A-Za-z0-9_@./#&+-]*$", ErrorMessage = "Your input is not valid, please try again."), MaxLength(500)]
        public string Description { get; set; }

    }
}
