using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentDataApp.Pages.PostGradPage
{
    public class IndexModel : PageModel
    {
        private readonly StudentDataAppContext _context;

        public IndexModel(StudentDataAppContext context)
        {
            _context = context;
        }

        public IList<PostGrad> PostGrads { get;set; }

        public int NumberOfEmployed { get; set; }
        public int NumberOfEducation { get; set; }
        public int NumberOfMilitary { get; set; }
        public int NumberOfMainframe { get; set; }
        public int NumberOfWebDev { get; set; }
        public int NumberOfMobile { get; set; }
        public string AverageSalary { get; set; }
        public string PercentPlaced { get; set; }
  
        public string InputCompName { get; set; }
        public string InputStuFName { get; set; }
        public string InputStuLName { get; set; }
        public string InputGradTerm { get; set; }

        public async Task OnGetAsync(string compName = null, string stuFName = null, string gradTerm = null, string stuLName = null)
        {
            InputCompName = compName;
            InputStuFName = stuFName;
            InputStuLName = stuLName;
            InputGradTerm = gradTerm;


            PostGrads = await _context.PostGrad.Where(e => (
                        (compName == null || e.CompName.ToUpper().Trim() == compName.ToUpper().Trim()) && 
                        (stuFName == null || e.Student.FirstName.ToUpper().Trim() == stuFName.ToUpper().Trim()) &&
                        (gradTerm == null || e.GradTerm.ToUpper().Trim() == gradTerm.ToUpper().Trim()) &&
                        (stuLName == null || e.Student.LastName.ToUpper().Trim() == stuLName.ToUpper().Trim())

                    )
                ).Include(i => i.Student).ToListAsync();

            NumberOfEducation = 0;
            NumberOfEmployed = 0;
            NumberOfMilitary = 0;
            NumberOfMainframe = 0;
            NumberOfWebDev = 0;
            NumberOfMobile = 0;
            double totalSalary = 0;
            int numberOfUnemployed = 0;
            

            foreach(PostGrad pg in PostGrads)
            {
                switch (pg.GradCode.ToUpper())
                {
                    case "MILITARY":
                        NumberOfMilitary += 1;
                        break;
                    case "EDUCATION":
                        NumberOfEducation += 1;
                        break;
                    case "JOB":
                        NumberOfEmployed += 1;
                        break;
                    default:
                        break;
                }
                if (pg.IsMainframe)
                {
                    NumberOfMainframe += 1;
                }
                if (pg.IsWebDev)
                {
                    NumberOfWebDev += 1;
                }
                if (pg.IsMobile)
                {
                    NumberOfMobile += 1;
                }

                if (pg.Salary != null)
                {
                    totalSalary += (double)pg.Salary;
                }
                else
                {
                    numberOfUnemployed += 1;
                }
                
            }



            if(PostGrads.Count > 0)
            {
                AverageSalary = (totalSalary /(PostGrads.Count - numberOfUnemployed)).ToString("C2");
            }
            else
            {
                AverageSalary = 0.ToString("C2");
            }

            PercentPlaced = ((double)(NumberOfEmployed + NumberOfEducation + NumberOfMilitary) / (double)PostGrads.Count).ToString("P");
        }
    }
}
