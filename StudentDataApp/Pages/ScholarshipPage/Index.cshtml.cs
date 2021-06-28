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
    public class IndexModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public IndexModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public IList<Scholarship> Scholarship { get;set; }

        public async Task OnGetAsync()
        {
            Scholarship = await _context.Scholarship.ToListAsync();
        }
    }
}
