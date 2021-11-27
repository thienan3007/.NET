using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGroup.entities
{
    public class Librarian
    {
        public int LibrarianID { get; set; }
        public String Password { get; set; }      
        public String LibrarianName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public int StatusID { get; set; }
        public LibrarianStatus Status { get; set; }
    }
}
