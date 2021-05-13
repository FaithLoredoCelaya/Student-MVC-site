using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLC_Lab3.Models
{
    public class Student
    {
        [Key]
        public int StudentNoID { get; set; }

        [Required(ErrorMessage = "Please Enter your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter your Last Name")]
        public string LastName { get; set; }

        public List<Enrollment> EnrolledCourses { get; set; }
    }
}
