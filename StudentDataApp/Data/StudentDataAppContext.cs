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
        public DbSet<StudentDataApp.Models.Internship> Internship { get; set; }
        public DbSet<StudentDataApp.Models.PostGrad> PostGrad { get; set; }
    }
}
