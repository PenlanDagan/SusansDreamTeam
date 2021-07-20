using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.EmploymentPage
{
    public class DetailsModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public DetailsModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public List<PostGrad> Employments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }
            StudentID = (int)studentId;
            Employments = await _context.Employment.Where(m => m.StudentID == studentId).ToListAsync();
            Student = await _context.Student.FirstOrDefaultAsync(s => s.StudentID == studentId);
        
            return Page();
        }
    }
}
