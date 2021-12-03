using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Publisher's name must not be emptied!!!")]
        public string Name { get; set; }
        public List<Shoe> Shoes { get; set; }
    }
}
