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

        public IList<Student> Student { get; set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }

        [BindProperty]
        public IFormFile UnorganizedStudentData { get; set; }

        public async Task OnPostAsync(int? id)
        {
            if (id == null)
            {
                if (UnorganizedStudentData != null)
                {
                    await ProcessUnorganizedData();
                }
            }
            else
            {
                Response.Redirect("ContactInfoPage/Details/" + id.ToString(), true);
                await OnGetAsync();
            }
        } 

        public async Task ProcessUnorganizedData()
        {
            using (StreamReader reader = new(UnorganizedStudentData.OpenReadStream()))
            {
                List<ContactInfo> contactInfos = new();
                
                // Skip the first title line
                await reader.ReadLineAsync();

                Student newStudent = null;

                // Start reading the data lines
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    string[] values = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                    //Adding Student
                    if (values[0].Trim().Length != 0)
                    {
                        // 0: Student ID
                        // 1: Last Name
                        // 2: First Name
                        // 6: Email
                        // 7: Phone Number
                        // 8: Address
                        // 9: City
                        // 10: State
                        // 11: Zip
                        newStudent = new Student { 
                            StudentSchoolID = int.Parse(values[0].Trim()), 
                            LastName = values[1].Trim(), 
                            FirstName = values[2].Trim() 
                        };
                        _context.Student.Add(newStudent);
                        await _context.SaveChangesAsync();
                    }

                    // Adding Contact Info
                    if (newStudent != null)
                    {
                        string email = values[6].Trim();
                        string phoneNumber = values[7].Trim();
                        string address = values[8].Trim();
                        string city = values[9].Trim();
                        string state = values[10].Trim();
                        string zip = values[11].Trim();

                        if (email.Length != 0       || 
                            phoneNumber.Length != 0 || 
                            address.Length != 0     || 
                            city.Length != 0        || 
                            state.Length != 0       || 
                            zip.Length != 0)
                        {
                            ContactInfo newContactInfo = new ContactInfo
                            {
                                StudentID = newStudent.StudentID,
                                EmailAddress = email.Length != 0 ? email : null,
                                PhoneNumber = phoneNumber.Length != 0 ? phoneNumber : null,
                                StreetAddress = address.Length != 0 ? address : null,
                                City = city.Length != 0 ? city : null,
                                State = state.Length != 0 ? state : null,
                                Zip = zip.Length != 0 ? zip : null
                            };
                            _context.ContactInfo.Add(newContactInfo);
                        }
                        else if (values[0].Trim().Length == 0)
                        {
                            // If there's no Student ID, no email, no phone number,
                            // assume that the file ended and exit.
                            break;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                await OnGetAsync();
            };
        }
        
    }
}
