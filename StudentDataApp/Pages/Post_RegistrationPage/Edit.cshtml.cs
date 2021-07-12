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

namespace StudentDataApp.Pages.Post_RegistrationPage
{
    public class EditModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public EditModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }
        public int StudentID { get; set; }

        [BindProperty]
        public Post_Registration Post_Registration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId, int? itemId)
        {
            if (studentId == null || itemId == null)
            {
                return NotFound();
            }
            StudentID = (int)studentId;

            Post_Registration = await _context.Post_Registration.FirstOrDefaultAsync(m => m.ID == itemId);

            if (Post_Registration == null)
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

            _context.Attach(Post_Registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Post_RegistrationExists(Post_Registration.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = Post_Registration.StudentID });
        }

        private bool Post_RegistrationExists(int id)
        {
            return _context.Post_Registration.Any(e => e.ID == id);
        }
    }
}
