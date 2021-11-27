using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProjectGroup.entities
{
    public class Student
    {
        public int StudentID { get; set; }
        public String StudentName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }

        public StudentStatus Status { get; set; }

        //[DisplayName("Status")]
        public int StatusID { get; set; }
    }
}
