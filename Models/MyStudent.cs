using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeClientApp.Models
{
    public class MyStudent
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
    }
}
