using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.ContactInfoPage
{
    public class CreateModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public CreateModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public int StudentID { get; set; }

        public IActionResult OnGet(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }
            StudentID = (int)studentId;

            return Page();
        }

        [BindProperty]
        public ContactInfo ContactInfo { get; set; }
        public readonly List<SelectListItem> States = StateSelectList.getItems();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ContactInfo.Add(ContactInfo);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Details", new { studentId = ContactInfo.StudentID });
        }
    }
}
