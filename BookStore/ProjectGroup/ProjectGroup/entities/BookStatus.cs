using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGroup.entities
{
    public class BookStatus
    {
        public string StatusName { get; set; }
        public int ID { get; set; }
        public List<Book> ListBook { get; set; }
    }
}
