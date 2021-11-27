using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProjectGroup.entities
{
    public class BookReturn
    {
        public int ID { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        public Student Student { get; set; }
        [DisplayName("Student")]
        public int StudentID { get; set; }
        public Librarian Librarian { get; set; }
        [DisplayName("Librarian")]
        public int LibrarianID { get; set; }
        public List<BookReturnDetail> BookReturnDetails { get; set; }
    }
}
