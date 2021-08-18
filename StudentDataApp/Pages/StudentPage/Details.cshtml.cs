using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.StudentPage
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
        public ContactInfo ContactInfo { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
        public Post_Registration post_Registration { get; set; }
        public Scholarship scholarship { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }
            StudentID = (int)StudentID;
            Student = await _context.Student.FirstOrDefaultAsync(m => m.StudentID == studentId);
            ContactInfo = await _context.ContactInfo.Where(m => m.StudentID == studentId).FirstOrDefaultAsync();
            post_Registration = await _context.Post_Registration.Where(m => m.StudentID == studentId).FirstOrDefaultAsync();
            scholarship = await _context.Scholarship.Where(m => m.StudentID == studentId).FirstOrDefaultAsync();

            return Page();
        }


    }
}