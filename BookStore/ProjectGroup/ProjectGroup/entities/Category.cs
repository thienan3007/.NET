using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGroup.entities
{
    public class Category
    {
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public List<Book> ListBook { get; set; }
    }
}
