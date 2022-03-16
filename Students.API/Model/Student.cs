using System;

namespace Students.API.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public int courses { get; set; }
        public double Cgpa { get; set; }
        public string ImageUrl { get; set; }
        public string Owner { get; set; }
    }
}
