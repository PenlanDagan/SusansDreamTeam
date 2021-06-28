using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.ScholarshipPage
{
    public class CreateModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public CreateModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Scholarship Scholarship { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Scholarship.Add(Scholarship);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
