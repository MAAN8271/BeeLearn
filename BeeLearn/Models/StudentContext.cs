using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeeLearn.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext>options) : base(options)
        {

        }
        public DbSet<Student> StudentRegistration { get; set; }
        public DbSet<Department> Department { get; set; }
    } 
}
