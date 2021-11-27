using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProjectGroup.entities
{
    public class Author
    {
        
        public int AuthorID { get; set; }
        public String AuthorName { get; set; }
        public String Email { get; set; }
        [DisplayName("Phone test")]
        public String Phone { get; set; }
    }
}
