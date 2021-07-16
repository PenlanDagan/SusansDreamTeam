using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.EmploymentPage
{
    public class EditModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public EditModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employment Employment { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }


        public async Task<IActionResult> OnGetAsync(int? studentId, int? itemId)
        {
            if (studentId == null || itemId == null)
            {
                return NotFound();
            }

            StudentID = (int)studentId;
            Employment = await _context.Employment.FirstOrDefaultAsync(m => m.ID == itemId);

            if (Employment == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Employment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmploymentExists(Employment.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = Employment.StudentID });
        }

        private bool EmploymentExists(int id)
        {
            return _context.Employment.Any(e => e.ID == id);
        }
    }
}
