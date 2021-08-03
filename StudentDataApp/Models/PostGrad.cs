using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentDataApp.Models
{
    public class PostGrad
    {
        [Key]
        public int ID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        [Display(Name = "Graduation Code")]
        public string GradCode { get; set; }

        [Display(Name = "Company/Institution Name")]
        public string CompName { get; set; }

        [Display(Name = "Company/Institution Location")]
        public string CompLocation { get; set; }

        [Display(Name = "Job Salary")]
        [DataType(DataType.Currency)]
        public double? Salary { get; set; }

        [Display(Name = "Graduation Term")]
        public string GradTerm { get; set; }

    }
}
