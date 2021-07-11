using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.ScholarshipPage
{
    public class DeleteModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public DeleteModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Scholarship Scholarship { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Scholarship = await _context.Scholarship.FirstOrDefaultAsync(m => m.ID == id);

            if (Scholarship == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Scholarship = await _context.Scholarship.FindAsync(id);

            if (Scholarship != null)
            {
                _context.Scholarship.Remove(Scholarship);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
