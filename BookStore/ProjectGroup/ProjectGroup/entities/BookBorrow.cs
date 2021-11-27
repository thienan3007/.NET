using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProjectGroup.entities
{
    public class BookBorrow
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public List<BookBorrowDetail> ListBookBorrowDetail { get; set; }
        public double TotalPrice { get; set; }
        public Student Student { get; set; }
        [DisplayName("Student")]
        public int StudentID { get; set; }
        
        public Librarian Librarian { get; set; }
        [DisplayName("Librarian")]
        public int LibrarianID { get; set; }
        public bool Status { get; set; }
    }
}
