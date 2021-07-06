using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentDataApp.Models;

namespace StudentDataApp.Data
{
    public class StudentDataAppContext : DbContext
    {
        public StudentDataAppContext (DbContextOptions<StudentDataAppContext> options)
            : base(options)
        {
        }

        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Scholarship> Scholarship { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentDataApp.Models.Post_Registration> Post_Registration { get; set; }
        public DbSet<StudentDataApp.Models.Registration> Registration { get; set; }
    }
}
