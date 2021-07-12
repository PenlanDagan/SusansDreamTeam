using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StudentDataApp.Models
{
    public class Internship
    {
        [Key]
        public int ID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        [Display(Name = "Internship Company Name")]
        public string IntCompName { get; set; }

        [Display(Name = "Internship Location")]
        public string IntCompLocation { get; set; }

        [Display(Name = "Internship Wage")]
        [DataType(DataType.Currency)]
        public double IntWage { get; set; }


    }
}
