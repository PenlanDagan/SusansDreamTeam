using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentDataApp.Models
{
    public class Post_Registration
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }


        [Required(ErrorMessage = "Missing schedule completion status")]
        [Display(Name = "Schedule Status")]
        public bool schedComp { get; set; } = false;

        [DataType(DataType.Text)]
        [Display(Name = "Starting Term")]
        [DisplayFormat( NullDisplayText = "Not Enrolled")]
        public string StartTerm { get; set; }


        [Required(ErrorMessage = "require emphasis")]
        [Display(Name = "Emphasis")]
        [DataType(DataType.Text)]
        [DisplayFormat(NullDisplayText = "undetermined")]
        public String emphasis { get; set; }

    }
}