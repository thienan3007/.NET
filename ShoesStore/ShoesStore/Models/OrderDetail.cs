using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int OrderId { get; set; }

        public Order Order { get; set; }
        
        [Range(1, Int32.MaxValue)]
        [Required]
        public int ShoeId { get; set; }

        [NotMapped]
        public Shoe Shoe { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required]
        public int Quantity { get; set; }
    }
}
