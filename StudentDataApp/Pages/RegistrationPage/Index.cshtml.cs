using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.RegistrationPage
{
    public class IndexModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public IndexModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public IList<Registration> Registration { get;set; }

        public async Task OnGetAsync()
        {
            Registration = await _context.Registration.ToListAsync();
        }
    }
}
