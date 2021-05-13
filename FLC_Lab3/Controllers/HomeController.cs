using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FLC_Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace FLC_Lab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            foreach(Enrollment e in _context.Enrollments)
            {
                e.Course = _context.Courses.Find(e.CourseID);
            }
            foreach (Enrollment s in _context.Enrollments)
            {
                s.Student = _context.Students.Find(s.StudentID);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EditPlus(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var enrollPlus = await _context.Students.FindAsync(id);
            if(enrollPlus ==null)
            {
                return NotFound();
            }
            EditPlus e = new EditPlus { student = enrollPlus};
            return View(e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlus(int id,[Bind("student.StudentID,student.EnrolledCourses")] EditPlus plus)
        {
            //This is returns null for some reason and I cant figure it out.  this code to me looks to be fine but i am not so sure so Ill be handing it in hoping for feedback
            if(id !=plus.student.StudentNoID)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    for(int i=0; i<plus.student.EnrolledCourses.Count();i++)
                    {
                        if(plus.Checked[i]==false)
                        {
                            var enrollment = await _context.Enrollments.FindAsync(plus.student.EnrolledCourses[i].EnrollmentID);
                            _context.Enrollments.Remove(enrollment);
                            plus.student.EnrolledCourses[i] = null;
                        }
                    }
                    Student s = plus.student;
                    _context.Update(s);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plus);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
