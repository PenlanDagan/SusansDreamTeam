using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.Post_RegistrationPage
{
    public class DeleteModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public DeleteModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post_Registration Post_Registration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post_Registration = await _context.Post_Registration.FirstOrDefaultAsync(m => m.stu_id == id);

            if (Post_Registration == null)
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

            Post_Registration = await _context.Post_Registration.FindAsync(id);

            if (Post_Registration != null)
            {
                _context.Post_Registration.Remove(Post_Registration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
