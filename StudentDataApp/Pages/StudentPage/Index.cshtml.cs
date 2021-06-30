using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Data;
using StudentDataApp.Models;

namespace StudentDataApp.Pages.StudentPage
{
    public class IndexModel : PageModel
    {
        private readonly StudentDataApp.Data.StudentDataAppContext _context;

        public IndexModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }

        [BindProperty]
        public IFormFile UnorganizedStudentData { get; set; }

        public async Task OnPostAsync()
        {
            if (UnorganizedStudentData != null)
            {
                await ProcessUnorganizedData();
            }

        } 

        public async Task ProcessUnorganizedData()
        {
            using (StreamReader reader = new(UnorganizedStudentData.OpenReadStream()))
            {
                List<ContactInfo> contactInfos = new();
                
                // Skip the first title line
                await reader.ReadLineAsync();
                
                // Start reading the data lines
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    string[] values = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    Console.WriteLine(values[0]);
                    if (values[0].Trim().Length != 0)
                    {
                        // 0: Student ID
                        // 1: Last Name
                        // 2: First Name
                        // 6: Email
                        // 7: Phone Number
                        Student newStudent = new Student { StudentSchoolID = int.Parse(values[0].Trim()), LastName = values[1].Trim(), FirstName = values[2].Trim() };
                        await _context.Student.AddAsync(newStudent);
                        await _context.SaveChangesAsync();
                        ContactInfo newContactInfo = new ContactInfo { StudentID = newStudent.StudentID, EmailAddress = values[6], PhoneNumber = values[7] };
                        await _context.ContactInfo.AddAsync(newContactInfo);
                        await _context.SaveChangesAsync();
                    }
                }
                await OnGetAsync();
            };
        }
    }
}
