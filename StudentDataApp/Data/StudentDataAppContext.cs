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

        public DbSet<StudentDataApp.Models.ContactInfo> ContactInfo { get; set; }

        public DbSet<StudentDataApp.Models.Scholarship> Scholarship { get; set; }
    }
}
