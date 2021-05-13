using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLC_Lab3.Models
{
    public class EditPlus
    {
        public List<Course> course { get; set; }
        public Student student { get; set; }
        public List<Enrollment> enrollment { get; set; }

        public List<bool> Checked { get; set; }
    }
}
