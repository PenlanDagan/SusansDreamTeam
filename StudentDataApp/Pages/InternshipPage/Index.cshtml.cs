using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.InternshipPage
{
    public class IndexModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public IndexModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public IList<Internship> Internship { get; set; }

        public string InputIntCompName { get; set; }
        public string InputIntStuFName { get; set; }
        public string IntAvgWage { get; set; }

        public async Task OnGetAsync(string compIntName = null, string stuIntFName = null)
        {
            InputIntCompName = compIntName;
            InputIntStuFName = stuIntFName;


            Internship = await _context.Internship.Where(e => (
                        (compIntName == null || e.IntCompName.ToUpper().Trim() == compIntName.ToUpper().Trim()) &&
                        (stuIntFName == null || e.Student.FirstName.ToUpper().Trim() == stuIntFName.ToUpper().Trim())
                    )
                ).Include(i => i.Student).ToListAsync();


            double totalWage = 0;



            foreach (Internship i in Internship)
            {
                if (i.IntWage != null)
                {
                    totalWage += (double)i.IntWage;
                }
            }
            if (Internship.Count > 0)
            {
                IntAvgWage = (totalWage / Internship.Count).ToString("C2");
            }
            else
            {
                IntAvgWage = 0.ToString("C2");
            }

        }
    }
}
