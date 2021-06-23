using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataApp.Models
{
    public class Scholarship
    {
        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student ID")]
        [MaxLength(8)]
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Scholarship amount is required")]
        [Display(Name = "Scholarship Amount")]
        [DataType(DataType.Currency)]
        public double ScholarshipAmount { get; set; }

        [Display(Name = "Date Offered")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateOffered { get; set; }

        [Display(Name = "Date Awarded")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateAwarded { get; set; }

        [Display(Name = "Date Accepted")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateAccepted { get; set; }
    }
}
