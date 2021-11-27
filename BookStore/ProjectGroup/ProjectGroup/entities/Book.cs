using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectGroup.entities
{
    public class Book
    {
        
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Language")]
        public string Language { get; set; }

        public int NoOfPages { get; set; }


        public Category Category { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        public Author Author { get; set; }

        [DisplayName("Author")]
        public int AuthorID { get; set; }

        public string Description { get; set; }
        public int QuantityLeft { get; set; }

        public BookStatus Status { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }

        
    }
}
