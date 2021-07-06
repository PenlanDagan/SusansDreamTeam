using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentDataApp.Models
{
    public class Post_Registration
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student ID")]
        [Key]
        public int stu_id { get; set; }


        [Required(ErrorMessage = "missing schedule completion status")]
        [Display(Name = "Schedule Status")]
        public Boolean sched_comp { get; set; }


        [Required(ErrorMessage = "missing start date")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime start_date { get; set; }


        [Required(ErrorMessage = "require projected graduation date")]
        [Display(Name = "Graduation Date")]
        [DataType(DataType.Date)]
        public DateTime grad_date { get; set; }


        [Required(ErrorMessage = "require emphasis")]
        [Display(Name = "Emphasis")]
        [DataType(DataType.Text)]
        public String emphasis { get; set; }
    }
}
