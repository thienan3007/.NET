using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Oder Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double OrderAmount { get; set; }
        [Required]
        public string OrderAddress { get; set; }
        [Required]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Phone must have 10 digits and contains no characters!!")]
        public string OrderPhone { get; set; }
        [Required]
        [EmailAddress]
        public string OrderEmail { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
