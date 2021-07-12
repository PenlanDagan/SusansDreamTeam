using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.InternshipPage
{
    public class CreateModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public CreateModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StudentID = (int)id;

            return Page();
        }

        [BindProperty]
        public Internship Internship { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Internship.Add(Internship);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Internship.StudentID });
        }
    }
}
