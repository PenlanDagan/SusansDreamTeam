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
        public bool schedComp { get; set; }


        [Required(ErrorMessage = "missing start date")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "Not Entered")]

        public DateTime startDate { get; set; }


        [Required(ErrorMessage = "require projected graduation date")]
        [Display(Name = "Graduation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "Not Entered")]
        public DateTime gradDate { get; set; }


        [Required(ErrorMessage = "require emphasis")]
        [Display(Name = "Emphasis")]
        [DataType(DataType.Text)]
        public String emphasis { get; set; }
    }
}
