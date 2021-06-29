using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataApp.Models
{
    public class ContactInfo
    {
        [Key]
        public int ID { get; set; }

        public int StudentID { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
