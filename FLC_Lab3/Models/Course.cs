using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLC_Lab3.Models
{
    public class Course
    {
        [Key]
        public int CourseCodeID { get; set; }

        [Required(ErrorMessage = "Cant be Null")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Please put a numeric value in here")]
        [RegularExpression("^[1-9]*$", ErrorMessage = "Please put a numeric value bigger than zero in here")]
        public int NumberOfCredits { get; set; }

        public List<Enrollment> EnrolledCourses { get; set; }

    }
}
