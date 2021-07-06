using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.RegistrationPage
{
    public class DetailsModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public DetailsModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public Registration Registration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Registration = await _context.Registration.FirstOrDefaultAsync(m => m.stu_id == id);

            if (Registration == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
