using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.API.Models;

namespace Students.API.Data
{
    public class StudentAPIContext : DbContext
    {
        public StudentAPIContext (DbContextOptions<StudentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Students.API.Models.Student> Student { get; set; }
    }
}
