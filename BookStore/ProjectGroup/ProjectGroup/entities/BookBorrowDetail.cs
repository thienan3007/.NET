using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProjectGroup.entities
{
    public class BookBorrowDetail
    {
        public int Id { get; set; }
        public int BookBorrowID { get; set; }
        public Book Book { get; set; }
        [DisplayName("Book")]
        public int BookId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
    }
}
