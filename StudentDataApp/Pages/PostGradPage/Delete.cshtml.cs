using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.PostGradPage
{
    public class DeleteModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public DeleteModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PostGrad PostGrad { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PostGrad = await _context.PostGrad
                .Include(e => e.Student).FirstOrDefaultAsync(m => m.ID == id);

            if (PostGrad == null)
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

            PostGrad = await _context.PostGrad.FindAsync(id);

            if (PostGrad != null)
            {
                _context.PostGrad.Remove(PostGrad);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
