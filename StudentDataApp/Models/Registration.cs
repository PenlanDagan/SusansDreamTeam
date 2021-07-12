using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace StudentDataApp.Models
{
    public class Registration
    {    
        [Key]
        public int ID { get; set; }
       
        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student ID")]
    
        public int StudentID { get; set; }
        
        [Required(ErrorMessage = "Required sign_course")]
        [Display(Name = "Signed for Courses")]
        public bool signCourse { get; set; }

        [Required(ErrorMessage = "Required Prior Classes")]
        [Display(Name = "Prior Classes")]
        public bool priorClasses { get; set; }
        
        [Required(ErrorMessage = "Required trans sent status")]
        [Display(Name = "Transcript Sent")]
        public bool transSet { get; set; }
        
        [Required(ErrorMessage = "Required trans eval status")]
        [Display(Name = "Transcipt Evaluated")]
        public bool transEval { get; set; }
        
        [Required(ErrorMessage = "Required enrollment status")]
        [Display(Name = "Enrolled")]
        public bool enrolled { get; set; }
        
        [Required(ErrorMessage = "Required term creation status")]
        [Display(Name = "Term Status")]
        public bool termCreate { get; set; }
    }
}
