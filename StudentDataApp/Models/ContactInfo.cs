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
        public Student Student { get; set; }

        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        
        public string State { get; set; }
        
        public string City { get; set; }
        
        public string Zip { get; set; }


    }
}
