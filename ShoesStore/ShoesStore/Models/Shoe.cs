using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models
{
    public class Shoe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Shoe's name must not be emptied!!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price must not be emptied!!!")]
        [Range(0, Double.MaxValue)]
        public double Price { get; set; }
        [Required(ErrorMessage = "Quantity must not be emptied!!!")]
        [Range(0, Int32.MaxValue)]
        public int Quantity { get; set; }
        public string ShortDes { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Shoe status must not be emptied!!!")]
        [DisplayName("Shoe Status")]
        public bool ShoeLive { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Publisher Publisher { get; set; }
        public Category Category { get; set; }
    }
}
