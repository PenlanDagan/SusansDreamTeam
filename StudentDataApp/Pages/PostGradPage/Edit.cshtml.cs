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

namespace StudentDataApp.Pages.PostGradPage
{
    public class EditModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public EditModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PostGrad PostGrad { get; set; }
        public Student Student { get; set; }
        public readonly List<SelectListItem> GradCodes = PostGradCodeSelectList.getItems();
        public readonly List<SelectListItem> EmpList = PostGradEmpList.getItems();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

 
            PostGrad = await _context.PostGrad.FirstOrDefaultAsync(m => m.ID == id);

            if (PostGrad == null)
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

            _context.Attach(PostGrad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostGradExists(PostGrad.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { studentId = PostGrad.StudentID });
        }

        private bool PostGradExists(int id)
        {
            return _context.PostGrad.Any(e => e.ID == id);
        }
    }
}
