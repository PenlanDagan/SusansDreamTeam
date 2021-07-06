using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentDataApp.Models
{
    public class Registration
    {
        public int ID { get; set; }
       
        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student ID")]
        [Key]
        public int stu_id { get; set; }
        
        [Required(ErrorMessage = "Required sign_course")]
        [Display(Name = "Signed for Courses")]
        public Boolean sign_course { get; set; }

        [Required(ErrorMessage = "Required Prior Classes")]
        [Display(Name = "Prior Classes")]
        public Boolean prior_classes { get; set; }
        
        [Required(ErrorMessage = "Required trans sent status")]
        [Display(Name = "Transcript Sent")]
        public Boolean trans_set { get; set; }
        
        [Required(ErrorMessage = "Required trans eval status")]
        [Display(Name = "Transcipt Evaluated")]
        public Boolean trans_eval { get; set; }
        
        [Required(ErrorMessage = "Required enrollment status")]
        [Display(Name = "Enrolled")]
        public Boolean enrolled { get; set; }
        
        [Required(ErrorMessage = "Required term creation status")]
        [Display(Name = "Term Status")]
        public Boolean term_create { get; set; }


    }
}
