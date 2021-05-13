using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FLC_Lab3.Models;

namespace FLC_Lab3.Component
{
    public class SelectedStudent : ViewComponent
    {
        public string Invoke(Student s)
        {
            return "Student: "+s.StudentNoID+" - "+s.FirstName+" "+s.LastName+" - "+s.EnrolledCourses; 
        }
    }
}
