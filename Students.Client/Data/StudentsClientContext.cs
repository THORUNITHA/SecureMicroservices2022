using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Students.Client.Models;

namespace Students.Client.Data
{
    public class StudentsClientContext : DbContext
    {
        public StudentsClientContext (DbContextOptions<StudentsClientContext> options)
            : base(options)
        {
        }

        public DbSet<Students.Client.Models.Student> Student { get; set; }
    }
}
