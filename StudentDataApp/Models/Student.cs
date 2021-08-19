using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataApp.Models
{
    public class Student
    {
        // This is the database generated ID for primary key. It is not the Student School ID.
        [Key]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Student School ID is required.")]
        [Display(Name = "Student School ID")]
        public int StudentSchoolID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [MaxLength(300, ErrorMessage = "Maximum number of characters that can be entered is 300")]
        public string Notes { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; }
        public ICollection<Scholarship> Scholarships { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Post_Registration> Post_Registrations { get; set; }
    }
}
