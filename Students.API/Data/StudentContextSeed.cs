using Students.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Students.API.Data
{
    public class StudentContextSeed
    {
        public static void SeedAsync(StudentAPIContext studentContext)
        {
     
            if (!studentContext.Student.Any())
            {
                var students = new List<Student>
                {
            new Student
            {
                Id = 1,
                Name = "Thorunitha",
                Department = "MTech",
                Address = "Vellore",
                courses=2,
                Cgpa=8.78,
                ImageUrl = "images/src",        
                Owner = "alice"
            },
            new Student
            {
                Id=2,
                Name="Aswini",
                Department="CSE",
                Address="Chennai",
                courses=4,
                 Cgpa=7.3,
                ImageUrl="images/src",
                Owner="alice"
            },
             new Student
            {
                Id=3,
                Name="Janani",
                Department="ECE",
                Address="Canada",
                courses=6,
                 Cgpa=9.0,
                ImageUrl="images/src",
                Owner="bob"
            }
            };
                studentContext.Student.AddRange(students);
                studentContext.SaveChanges();
            }
        }
    }
}
