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

        public bool StudentIsExist = false;

        public IndexModel(StudentDataApp.Data.StudentDataAppContext context)
        {
            _context = context;
            StudentIsExist = _context.Student.Any(s => s.StudentID == s.StudentID);
        }

        public IList<Student> Student { get; set; }

        public string InputStartTerm { get; set; }
        public string InputFirstName { get; set; }
        public string InputLastName { get; set; }

        public async Task OnGetAsync(string submitButton, string startTerm, string firstName, string lastName)
        {
            StudentIsExist = _context.Student.Any(s => s.StudentID == s.StudentID);

            if (submitButton == "Filter")
            {
                // GOD IS DEAD THE SPIRIT IN THE MACHINE HAS DEEMED THIS UNACCEPATABLE REPENT
                if (startTerm == null && firstName == null && lastName == null)
                {
                    return;
                }
                if (startTerm != null) startTerm = startTerm.Trim();
                if (firstName != null) firstName = firstName.Trim();
                if (lastName != null) lastName = lastName.Trim();

                Student = await _context.Student.Where(s => (
                    (startTerm == null || s.Post_Registrations.Any(p => p.StartTerm == startTerm.Trim())) &&
                    (firstName == null || firstName.Length == 0 || s.FirstName.ToUpper().Contains(firstName.ToUpper())) &&
                    (lastName == null || lastName.Length == 0 || s.LastName.ToUpper().Contains(lastName.ToUpper()))
                )).Include(s => s.Post_Registrations).ToListAsync();

            }
            else if(submitButton == "Get All")
            {
                Student = await _context.Student.ToListAsync();
            }
        }
        [BindProperty]
        public IFormFile RawStudentData { get; set; }

        public async Task OnPostAsync()
        {
            if (RawStudentData != null)
            {
                await ProcessRawData();
            }

            StudentIsExist = _context.Student.Any(s => s.StudentID == s.StudentID);
        } 

        public string UploadErrorMessage { get; set; }
        public async Task ProcessRawData()
        {
            //Getting old student list for comparision
            Student = await _context.Student.ToListAsync();
            try
            {
                using (StreamReader reader = new(RawStudentData.OpenReadStream()))
                {
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
                            //Check if student is already in the database.
                            //If student is in the database, then skip that student.
                            if (Student.Count > 0)
                            {
                                int StudentSchoolID = int.Parse(values[0].Trim());
                                if (Student.FirstOrDefault(s => s.StudentSchoolID == StudentSchoolID) != null)
                                {
                                    continue;
                                }
                            }

                            // 0: Student ID, 1: Last Name, 2: First Name, 5: Start Term
                            // 6: Email, 7: Phone Number, 8: Address, 9: City, 10: State, 11: Zip
                            newStudent = new Student
                            {
                                StudentSchoolID = int.Parse(values[0].Trim()),
                                LastName = values[1].Trim(),
                                FirstName = values[2].Trim()
                            };

                            _context.Student.Add(newStudent);
                            _context.SaveChanges();

                            Post_Registration postReg = new()
                            {
                                emphasis = "",
                                schedComp = false,
                                StudentID = newStudent.StudentID,
                                StartTerm = values[5].Trim()
                            };


                            Registration Reg = new()
                            {
                                StudentID = newStudent.StudentID,
                                signCourse = false,
                                priorClasses = false,
                                transSet =  false,
                                transEval = false,
                                enrolled = false,
                                termCreate = false
                            };
                            _context.Registration.Add(Reg);
                            _context.Post_Registration.Add(postReg);
                            _context.SaveChanges();


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

                            if (email.Length != 0 ||
                                phoneNumber.Length != 0 ||
                                address.Length != 0 ||
                                city.Length != 0 ||
                                state.Length != 0 ||
                                zip.Length != 0)
                            {
                                ContactInfo newContactInfo = new()
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
                                // If there's no Student ID, no Contact Infos,
                                // assume that the file ended and exit.
                                break;
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    Student = null;
                };
            } catch (Exception e)
            {
                UploadErrorMessage = "Something went wrong while uploading. System error: " + e.ToString();
            }
        }
        
    }
}
